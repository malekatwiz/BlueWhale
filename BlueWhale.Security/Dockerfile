FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src

COPY . .
RUN dotnet restore BlueWhale.Security.csproj
COPY . .
WORKDIR /src
RUN dotnet build BlueWhale.Security.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BlueWhale.Security.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlueWhale.Security.dll"]
