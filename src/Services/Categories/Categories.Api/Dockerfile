FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY src/Services/Categories/Categories.Api/Categories.Api.csproj src/Services/Categories/Categories.Api/
COPY src/Services/Categories/Categories.Domain/Categories.Domain.csproj src/Services/Categories/Categories.Domain/
COPY src/Services/Categories/Categories.Infrastructure/Categories.Infrastructure.csproj src/Services/Categories/Categories.Infrastructure/
RUN dotnet restore "src/Services/Categories/Categories.Api/Categories.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Categories/Categories.Api"
RUN dotnet build "Categories.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Categories.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Categories.Api.dll"]
