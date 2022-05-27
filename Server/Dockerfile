#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server/afterimage.Server.csproj", "Server/"]
COPY ["Client/afterimage.Client.csproj", "Client/"]
COPY ["Shared/afterImage.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/afterimage.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "afterimage.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "afterimage.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
