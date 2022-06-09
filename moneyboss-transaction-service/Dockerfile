#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["moneyboss-transaction-service/moneyboss-transaction-service.csproj", "moneyboss-transaction-service/"]
RUN dotnet restore "moneyboss-transaction-service/moneyboss-transaction-service.csproj"
COPY . .
WORKDIR "/src/moneyboss-transaction-service"
RUN dotnet build "moneyboss-transaction-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "moneyboss-transaction-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "moneyboss-transaction-service.dll"]