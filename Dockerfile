
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CareGuide.API/CareGuide.API.csproj CareGuide.API/
COPY CareGuide.Core/CareGuide.Core.csproj CareGuide.Core/
COPY CareGuide.Data/CareGuide.Data.csproj CareGuide.Data/
COPY CareGuide.Infra/CareGuide.Infra.csproj CareGuide.Infra/
COPY CareGuide.Models/CareGuide.Models.csproj CareGuide.Models/
COPY CareGuide.Security/CareGuide.Security.csproj CareGuide.Security/

RUN dotnet restore CareGuide.API/CareGuide.API.csproj

COPY . .
WORKDIR /src/CareGuide.API

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "CareGuide.API.dll"]
