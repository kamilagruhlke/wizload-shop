FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY src/Services/Products/Products.Api/Products.Api.csproj src/Services/Products/Products.Api/
COPY src/Services/Products/Products.Domain/Products.Domain.csproj src/Services/Products/Products.Domain/
COPY src/Services/Products/Products.Infrastructure/Products.Infrastructure.csproj src/Services/Products/Products.Infrastructure/
RUN dotnet restore "src/Services/Products/Products.Api/Products.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Products/Products.Api"
RUN dotnet build "Products.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.Api.dll"]
