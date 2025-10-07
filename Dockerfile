FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY CareGuide.API/CareGuide.API.csproj CareGuide.API/
COPY CareGuide.Core/CareGuide.Core.csproj CareGuide.Core/
COPY CareGuide.Data/CareGuide.Data.csproj CareGuide.Data/
COPY CareGuide.Models/CareGuide.Models.csproj CareGuide.Models/
COPY CareGuide.Security/CareGuide.Security.csproj CareGuide.Security/
COPY CareGuide.Infra/CareGuide.Infra.csproj CareGuide.Infra/

RUN dotnet restore "CareGuide.API/CareGuide.API.csproj"

COPY . .

WORKDIR /src/CareGuide.API
RUN dotnet build "CareGuide.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CareGuide.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CareGuide.API.dll"]
