# ===============================
# Stage 1: Build and publish
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# Restore toàn bộ solution
RUN dotnet restore FUNewsManagementSystemASS2.sln

# Publish dự án WebApp cụ thể (chỉ định rõ đường dẫn)
RUN dotnet publish FUNewsManagementSystemASS2/FUNewsManagementSystem.WebApp.csproj -c Release -o /app --no-restore

# ===============================
# Stage 2: Runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "FUNewsManagementSystem.WebApp.dll"]
