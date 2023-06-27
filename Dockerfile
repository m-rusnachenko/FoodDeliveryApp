# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0-focal AS build
WORKDIR /source
COPY . .

# Restore
RUN dotnet restore "./FoodDeliveryApi/FoodDeliveryApi.csproj" --disable-parallel
RUN dotnet publish "./FoodDeliveryApi/FoodDeliveryApi.csproj" -c Release -o /app --no-restore

# Service stage
FROM mcr.microsoft.com/aspnet:7.0-focal AS service
WORKDIR /app

# Copy build artifacts
COPY --from=build /app ./

# Run
EXPOSE 5000
ENTRYPOINT ["dotnet", "FoodDeliveryApi.dll"]
