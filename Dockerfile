
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the full source code and publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Production image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose default HTTP port
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "MyWebApi.dll"]
