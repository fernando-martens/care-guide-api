
# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CareGuide.API/CareGuide.API.csproj", "CareGuide.API/"]
COPY ["CareGuide.Core/CareGuide.Core.csproj", "CareGuide.Core/"]
COPY ["CareGuide.Data/CareGuide.Data.csproj", "CareGuide.Data/"]
COPY ["CareGuide.Models/CareGuide.Models.csproj", "CareGuide.Models/"]
COPY ["CareGuide.Security/CareGuide.Security.csproj", "CareGuide.Security/"]
COPY ["CareGuide.Infra/CareGuide.Infra.csproj", "CareGuide.Infra/"]
RUN dotnet restore "./CareGuide.API/CareGuide.API.csproj"
COPY . .
WORKDIR "/src/CareGuide.API"
RUN dotnet build "./CareGuide.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CareGuide.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CareGuide.API.dll"]
