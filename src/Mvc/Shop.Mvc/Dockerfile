FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY src/Mvc/Shop.Mvc/Shop.Mvc.csproj src/Mvc/Shop.Mvc/
RUN dotnet restore "src/Mvc/Shop.Mvc/Shop.Mvc.csproj"
COPY . .
WORKDIR "/src/src/Mvc/Shop.Mvc"
RUN dotnet build "Shop.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Mvc.dll"]
