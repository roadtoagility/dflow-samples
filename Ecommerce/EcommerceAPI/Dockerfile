FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EcommerceAPI/EcommerceAPI.csproj", "EcommerceAPI/"]
RUN dotnet restore "EcommerceAPI/EcommerceAPI.csproj"
COPY . .
WORKDIR "/src/EcommerceAPI"
RUN dotnet build "EcommerceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcommerceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceAPI.dll"]
