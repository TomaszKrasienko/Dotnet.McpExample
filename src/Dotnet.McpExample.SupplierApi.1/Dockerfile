FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Dotnet.McpExample.SupplierApi.1/Dotnet.McpExample.SupplierApi.1.csproj", "Dotnet.McpExample.SupplierApi.1/"]
COPY ["Dotnet.McpExample.ProductSeed/Dotnet.McpExample.ProductSeed.csproj", "Dotnet.McpExample.ProductSeed/"]
RUN dotnet restore "Dotnet.McpExample.SupplierApi.1/Dotnet.McpExample.SupplierApi.1.csproj"
COPY . .
WORKDIR "/src/Dotnet.McpExample.SupplierApi.1"
RUN dotnet build "Dotnet.McpExample.SupplierApi.1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.McpExample.SupplierApi.1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.McpExample.SupplierApi.1.dll"]
