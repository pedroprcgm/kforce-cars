FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KForceCars/KForceCars.csproj", "KForceCars/"]
RUN dotnet restore "KForceCars/KForceCars.csproj"
COPY . .
WORKDIR "/src/KForceCars"
RUN dotnet build "KForceCars.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KForceCars.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KForceCars.dll"]
