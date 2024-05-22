# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./

ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ENV ASPNETCORE_ENVIRONMENT=Development

RUN dotnet restore 

# Copy everything else and build
RUN dotnet publish -c Release -r osx-arm64 -o /app/publish

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "Infra.dll"]
