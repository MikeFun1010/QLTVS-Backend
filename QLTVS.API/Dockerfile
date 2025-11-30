#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Giai đoạn 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- QUAN TRỌNG: Copy file .csproj của TẤT CẢ các tầng ---
COPY ["QLTVS.API/QLTVS.API.csproj", "QLTVS.API/"]
COPY ["QLTVS.BUS/QLTVS.BUS.csproj", "QLTVS.BUS/"]
COPY ["QLTVS.DAO/QLTVS.DAO.csproj", "QLTVS.DAO/"]
COPY ["QLTVS.DTO/QLTVS.DTO.csproj", "QLTVS.DTO/"]

# Restore các thư viện (Dựa trên API là thằng chính)
RUN dotnet restore "QLTVS.API/QLTVS.API.csproj"

# Copy toàn bộ source code của cả 4 thư mục vào
COPY . .

# Chuyển vào thư mục API để build
WORKDIR "/src/QLTVS.API"
RUN dotnet build "QLTVS.API.csproj" -c Release -o /app/build

# Publish ra file chạy (.dll)
FROM build AS publish
RUN dotnet publish "QLTVS.API.csproj" -c Release -o /app/publish

# Giai đoạn 2: Run (Tạo môi trường chạy thật)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QLTVS.API.dll"]