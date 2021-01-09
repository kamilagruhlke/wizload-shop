FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/Services/Notifications/Notifications.Api/Notifications.Api.csproj src/Services/Notifications/Notifications.Api/
RUN dotnet restore "src/Services/Notifications/Notifications.Api/Notifications.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Notifications/Notifications.Api"
RUN dotnet build "Notifications.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notifications.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.Api.dll"]
