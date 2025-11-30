# Giai đoạn 1: Base (Môi trường chạy)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Giai đoạn 2: Build code (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy file .csproj của TẤT CẢ các tầng
COPY ["QLTVS.API/QLTVS.API.csproj", "QLTVS.API/"]
COPY ["QLTVS.BUS/QLTVS.BUS.csproj", "QLTVS.BUS/"]
COPY ["QLTVS.DAO/QLTVS.DAO.csproj", "QLTVS.DAO/"]
COPY ["QLTVS.DTO/QLTVS.DTO.csproj", "QLTVS.DTO/"]

# 2. Restore thư viện (Đã loại bỏ lỗi đường dẫn thừa)
RUN dotnet restore "QLTVS.API/QLTVS.API.csproj"

# 3. Copy toàn bộ source code còn lại vào
COPY . .

# 4. Build dự án
WORKDIR "/src/QLTVS.API"
RUN dotnet build "QLTVS.API.csproj" -c Release -o /app/build

# Giai đoạn 3: Publish ra file chạy (.dll)
FROM build AS publish
RUN dotnet publish "QLTVS.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Giai đoạn 4: Chạy thật (Final)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QLTVS.API.dll"]