#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Kudos.Api/Kudos.Api.csproj", "src/Kudos.Api/"]
COPY ["src/Kudos.Data/Kudos.Data.csproj", "src/Kudos.Data/"]
COPY ["src/Kudos.Domain/Kudos.Domain.csproj", "src/Kudos.Domain/"]
COPY ["src/Kudos.Services/Kudos.Services.csproj", "src/Kudos.Services/"]
RUN dotnet restore "src/Kudos.Api/Kudos.Api.csproj"
COPY . .
WORKDIR "/src/src/Kudos.Api"
RUN dotnet build "Kudos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kudos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kudos.Api.dll"]