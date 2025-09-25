FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/billing.api/billing.api.csproj", "services/billing.api/"]
COPY ["building-blocks/Shared.Kernel/Shared.Kernel.csproj", "building-blocks/Shared.Kernel/"]
COPY ["building-blocks/Shared.Messaging/Shared.Messaging.csproj", "building-blocks/Shared.Messaging/"]
RUN dotnet restore "services/billing.api/billing.api.csproj"
COPY . .
WORKDIR "/src/services/billing.api"
RUN dotnet build "./billing.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./billing.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "billing.api.dll"]
