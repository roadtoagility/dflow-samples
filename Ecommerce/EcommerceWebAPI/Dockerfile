FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8880
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TodoManagerWebAPI/TodoManagerWebAPI.csproj", "TodoManagerWebAPI/"]
RUN dotnet restore "TodoManagerWebAPI/TodoManagerWebAPI.csproj"
COPY . .
WORKDIR "/src/TodoManagerWebAPI"
RUN dotnet build "TodoManagerWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoManagerWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoManagerWebAPI.dll"]
