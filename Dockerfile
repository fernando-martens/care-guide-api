
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
COPY ["src/CareGuide.API/CareGuide.API.csproj", "src/CareGuide.API/"]
COPY ["src/CareGuide.Core/CareGuide.Core.csproj", "src/CareGuide.Core/"]
COPY ["src/CareGuide.Data/CareGuide.Data.csproj", "src/CareGuide.Data/"]
COPY ["src/CareGuide.Models/CareGuide.Models.csproj", "src/CareGuide.Models/"]
COPY ["src/CareGuide.Security/CareGuide.Security.csproj", "src/CareGuide.Security/"]
COPY ["src/CareGuide.Infra/CareGuide.Infra.csproj", "src/CareGuide.Infra/"]
RUN dotnet restore "./src/CareGuide.API/CareGuide.API.csproj"
COPY . .
WORKDIR "/src/src/CareGuide.API"
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
