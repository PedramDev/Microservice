#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog/API/Catalog.API.csproj", "Services/Catalog/API/"]
COPY ["Services/Catalog/Repository/Catalog.Repository/Catalog.Repository.csproj", "Services/Catalog/Repository/Catalog.Repository/"]
COPY ["Services/Catalog/Data/Catalog.Data/Catalog.Data.csproj", "Services/Catalog/Data/Catalog.Data/"]
COPY ["Services/Catalog/Core/Catalog.Core.csproj", "Services/Catalog/Core/"]
COPY ["Services/Catalog/Infrastructure/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "Services/Catalog/Infrastructure/Catalog.Infrastructure/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
RUN dotnet restore "Services/Catalog/API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
