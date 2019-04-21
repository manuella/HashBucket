FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY HashBucket.csproj ./HashBucket/
RUN dotnet restore "HashBucket/HashBucket.csproj"
COPY . .
#WORKDIR "/src/HashBucket"
RUN dotnet build "HashBucket.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HashBucket.csproj" -c Release -o /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS https://*:5000
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:5000"]