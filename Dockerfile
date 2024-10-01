# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos .csproj e restaurar as dependências
COPY CareGuide.API/CareGuide.API.csproj CareGuide.API/
COPY CareGuide.Core/CareGuide.Core.csproj CareGuide.Core/
COPY CareGuide.Data/CareGuide.Data.csproj CareGuide.Data/
COPY CareGuide.Infra/CareGuide.Infra.csproj CareGuide.Infra/
COPY CareGuide.Models/CareGuide.Models.csproj CareGuide.Models/
COPY CareGuide.Security/CareGuide.Security.csproj CareGuide.Security/

RUN dotnet restore CareGuide.API/CareGuide.API.csproj

# Copiar todo o código para o container e compilar
COPY . .
WORKDIR /app/CareGuide.API

# Publicar o projeto
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Executar a aplicação usando a imagem Runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados da etapa de build
COPY --from=build /app/out .

# Expor porta
EXPOSE 8080     

# Definir o ponto de entrada para rodar a aplicação
ENTRYPOINT ["dotnet", "CareGuide.API.dll"]
