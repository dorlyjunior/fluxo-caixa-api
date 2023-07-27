using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using FilterDescriptor = Microsoft.AspNetCore.Mvc.Filters.FilterDescriptor;

namespace FluxoCaixa.Projeto.API.Filters
{
    public class ParametroAuthSwaggerFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            IList<FilterDescriptor> filterDescriptors = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            bool isAuthorized = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            bool allowAnonymous = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

            if (isAuthorized && !allowAnonymous)
            {
                operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Token de Autorização",
                    Required = true,
                });

                operation.Responses.Add("401", new OpenApiResponse() { Description = "Usuário não está autenticado" });
                operation.Responses.Add("403", new OpenApiResponse() { Description = "Usuário não tem permissão para acessar a tela" });
            }

            operation.Responses.Add("500", new OpenApiResponse() { Description = "Erro interno no servidor" });
            operation.Responses.Add("400", new OpenApiResponse() { Description = "Erro de validação. Alguma regra de negócio foi violada." });
        }
    }
}
