FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/staff.api/staff.api.csproj", "services/staff.api/"]
COPY ["building-blocks/Shared.Messaging/Shared.Messaging.csproj", "building-blocks/Shared.Messaging/"]
COPY ["building-blocks/Shared.Kernel/Shared.Kernel.csproj", "building-blocks/Shared.Kernel/"]
RUN dotnet restore "services/staff.api/staff.api.csproj"
COPY . .
WORKDIR "/src/services/staff.api"
RUN dotnet build "./staff.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./staff.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "staff.api.dll"]
