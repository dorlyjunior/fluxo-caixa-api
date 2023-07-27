using FluxoCaixa.Projeto.API.Filters;
using FluxoCaixa.Projeto.API.Middlewares;
using FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Profiles;
using FluxoCaixa.Projeto.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Reflection;
using System.Text;

namespace FluxoCaixa.Projeto.API
{
    public class Startup
    {
        readonly string CorsPolicy = "_corsPolicy";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ambiente = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment ambiente;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.AddService<UnitOfWorkActionFilterAsync>();
            })
            .AddJsonOptions(
                opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null
            );

            // Automapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ContasProfile)));

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: CorsPolicy,
                        builder =>
                            {
                                builder.AllowAnyOrigin();
                                builder.AllowAnyMethod();
                                builder.AllowAnyHeader();
                            }
                );
            });

            // JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            throw new UnauthorizedAccessException(context.ErrorDescription);
                        }
                    };
                });

            // Config Swagger
            string nomeAmbiente = ambiente.EnvironmentName;
            string versao = Configuration.GetSection("Aplicacao:Versao").Value;
            string informacoes = "Versão: " + versao;

            if (nomeAmbiente != "Producao")
            {
                string host = Configuration.GetConnectionString("DefaultConnection").Split(";")[2];
                string banco = Configuration.GetConnectionString("DefaultConnection").Split(";")[4];
                string schema = Configuration.GetSection("Schema").Value;
                informacoes += @$"<h3 style='font-size:16px'>Informações da Conexão: </h3>
                        <h5>[{host}] | [{banco}] | [Schema={schema}]</h5>";
            }

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
                c.SwaggerDoc("seguranca", new OpenApiInfo { Title = "FluxoCaixa.Projeto.Seguranca", Version = "v1", Description = informacoes });
                c.SwaggerDoc("caixa", new OpenApiInfo { Title = "FluxoCaixa.Projeto.Caixa", Version = "v1", Description = informacoes });
            });

            // Injeção
            services.AddScoped<UnitOfWorkActionFilterAsync, UnitOfWorkActionFilterAsync>();
            NativeInjectorBootStrapper.Registrar(services, Configuration);

            // Tratamento para erros de model invalid
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string basePath = Configuration.GetSection("Aplicacao:BasePath").Value;

            if(!string.IsNullOrWhiteSpace(basePath))
            {
                app.UsePathBase(new PathString(basePath));

                app.Use(async(context, next) => 
                {
                    context.Request.PathBase = basePath;
                    await next.Invoke();
                });
            }

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseHttpsRedirection();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(basePath+"/swagger/seguranca/swagger.json", "FluxoCaixa.Projeto.Seguranca");
                c.SwaggerEndpoint(basePath+"/swagger/caixa/swagger.json", "FluxoCaixa.Projeto.Caixa");
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
            });


            // Port foward para Nginx
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Tratamento de erros global
            app.UseMiddleware(typeof(TratamentoDeErrosMiddleware));

            //CORS
            app.UseCors(CorsPolicy);

            app.UseRouting();

            // Autenticação
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
