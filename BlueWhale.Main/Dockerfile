FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src

COPY . .
RUN dotnet restore BlueWhale.Main/BlueWhale.Main.csproj
COPY . .
WORKDIR /src/BlueWhale.Main
RUN dotnet build BlueWhale.Main.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BlueWhale.Main.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlueWhale.Main.dll"]
