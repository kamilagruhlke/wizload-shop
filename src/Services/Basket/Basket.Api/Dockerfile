FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/Services/Basket/Basket.Api/Basket.Api.csproj src/Services/Basket/Basket.Api/
COPY src/Services/Basket/Basket.Domain/Basket.Domain.csproj src/Services/Basket/Basket.Domain/
COPY src/Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj src/Services/Basket/Basket.Infrastructure/
RUN dotnet restore "src/Services/Basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
