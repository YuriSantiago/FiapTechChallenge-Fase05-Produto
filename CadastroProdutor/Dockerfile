FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Core ./Core
COPY Infrastructure ./Infrastructure
COPY CadastroProdutor ./CadastroProdutor

WORKDIR /src/CadastroProdutor

RUN dotnet restore CadastroProdutor.csproj
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "CadastroProdutor.dll"]