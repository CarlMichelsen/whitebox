FROM node AS frontend

WORKDIR /src

COPY ["Frontend", "."]

RUN npm install

RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["whitebox.sln", "./"]

COPY --from=frontend ["src/dist/", "Api/wwwroot/"]

COPY . .

RUN dotnet restore

RUN dotnet test

RUN dotnet build "./Api" -c Release --output /app/build

FROM build AS publish

RUN dotnet publish "./Api" -c Release --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "./Api.dll"]