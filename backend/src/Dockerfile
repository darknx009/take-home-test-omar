FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /Fundo.Domain
COPY "Fundo.Domain/Fundo.Domain.csproj" .
RUN dotnet restore
COPY "Fundo.Domain" .
RUN dotnet publish -c release -o /app

WORKDIR /Fundo.Infrastructure
COPY "Fundo.Infrastructure/Fundo.Infrastructure.csproj" .
RUN dotnet restore
COPY "Fundo.Infrastructure" .
RUN dotnet publish -c release -o /app

WORKDIR /src
COPY "Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj" .
RUN dotnet restore
COPY "Fundo.Applications.WebApi" .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Fundo.Applications.WebApi.dll"]