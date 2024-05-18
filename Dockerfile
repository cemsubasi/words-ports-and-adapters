# Derleme için kullanılacak .NET SDK imajı
FROM mcr.microsoft.com/dotnet/sdk:8.0.203-alpine3.18-amd64 AS build
WORKDIR /source

# Proje dosyalarını kopyala
COPY *.csproj .
# RUN dotnet restore .

# Tüm proje dosyalarını kopyala ve build et
COPY . .
RUN dotnet publish -c Release -o /app

# Çalışma zamanı için hafif bir imaj kullan
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-alpine3.18-amd64
WORKDIR /app
COPY --from=build /app .

# Uygulamayı çalıştır
ENTRYPOINT ["dotnet", "Infra.dll"]
