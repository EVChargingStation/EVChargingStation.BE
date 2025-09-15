# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["EVChargingStation.API/EVChargingStation.API.csproj", "EVChargingStation.API/"]
COPY ["EVChargingStation.Application/EVChargingStation.Application.csproj", "EVChargingStation.Application/"]
COPY ["EVChargingStation.Infrastructure/EVChargingStation.Infrastructure.csproj", "EVChargingStation.Infrastructure/"]
COPY ["EVChargingStation.Domain/EVChargingStation.Domain.csproj", "EVChargingStation.Domain/"]
RUN dotnet restore "./EVChargingStation.API/EVChargingStation.API.csproj"
COPY . .
WORKDIR "/src/EVChargingStation.API"
RUN dotnet build "./EVChargingStation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# PUBLISH stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EVChargingStation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# FINAL image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EVChargingStation.API.dll"]