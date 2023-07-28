FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
ENV ASPNETCORE_ENVIRONMENT Development
WORKDIR /
COPY . .
RUN dotnet restore
RUN dotnet publish -c Debug -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/FluxoCaixa.Projeto.API.dll" ]