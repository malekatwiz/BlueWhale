FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src

COPY . .
RUN dotnet restore BlueWhale.Exchange.csproj
COPY . .
WORKDIR /src
RUN dotnet build BlueWhale.Exchange.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BlueWhale.Exchange.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlueWhale.Exchange.dll"]