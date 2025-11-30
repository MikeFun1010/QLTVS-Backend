

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QLTVS.API/QLTVS.API.csproj", "QLTVS.API/"]
COPY ["QLTVS.BUS/QLTVS.BUS.csproj", "QLTVS.BUS/"]
COPY ["QLTVS.DAO/QLTVS.DAO.csproj", "QLTVS.DAO/"]
COPY ["QLTVS.DTO/QLTVS.DTO.csproj", "QLTVS.DTO/"]
RUN dotnet restore "./QLTVS.API/./QLTVS.API.csproj"
COPY . .
WORKDIR "/src/QLTVS.API"
RUN dotnet build "./QLTVS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QLTVS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QLTVS.API.dll"]