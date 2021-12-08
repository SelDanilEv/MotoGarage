FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM node:14-alpine as build-node
WORKDIR /ClientApp
COPY MotoGarage/ClientApp/package.json .
COPY MotoGarage/ClientApp/package-lock.json .
RUN npm install
COPY MotoGarage/ClientApp/ . 
RUN npm run build  
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV BuildingDocker true
WORKDIR /src
COPY ["MotoGarage/MotoGarage.csproj", "MotoGarage/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "MotoGarage/MotoGarage.csproj"
COPY . .
WORKDIR "/src/MotoGarage"
RUN dotnet build "MotoGarage.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MotoGarage.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=build-node /ClientApp/build ./ClientApp/build
ENTRYPOINT ["dotnet", "MotoGarage.dll"]