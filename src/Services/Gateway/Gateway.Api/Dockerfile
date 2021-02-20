FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY src/Services/Gateway/Gateway.Api/Gateway.Api.csproj src/Services/Gateway/Gateway.Api/
RUN dotnet restore "src/Services/Gateway/Gateway.Api/Gateway.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Gateway/Gateway.Api"
RUN dotnet build "Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Api.dll"]
