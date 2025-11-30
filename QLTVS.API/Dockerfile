# Giai đoạn 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file csproj của TẤT CẢ các tầng
COPY ["QLTVS.API/QLTVS.API.csproj", "QLTVS.API/"]
COPY ["QLTVS.BUS/QLTVS.BUS.csproj", "QLTVS.BUS/"]
COPY ["QLTVS.DAO/QLTVS.DAO.csproj", "QLTVS.DAO/"]
COPY ["QLTVS.DTO/QLTVS.DTO.csproj", "QLTVS.DTO/"]

# Restore
RUN dotnet restore "QLTVS.API/QLTVS.API.csproj"

# Copy toàn bộ code
COPY . .

# Build
WORKDIR "/src/QLTVS.API"
RUN dotnet build "QLTVS.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "QLTVS.API.csproj" -c Release -o /app/publish

# Giai đoạn 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QLTVS.API.dll"]