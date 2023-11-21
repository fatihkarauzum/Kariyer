# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kariyer.Api/Kariyer.Api.csproj", "Kariyer.Api/"]
COPY ["Kariyer.Business/Kariyer.Business.csproj", "Kariyer.Business/"]
COPY ["Kariyer.Core/Kariyer.Core.csproj", "Kariyer.Core/"]
COPY ["Kariyer.Common/Kariyer.Common.csproj", "Kariyer.Common/"]
COPY ["Kariyer.Data/Kariyer.Data.csproj", "Kariyer.Data/"]
COPY ["Kariyer.Model/Kariyer.Model.csproj", "Kariyer.Model/"]
COPY ["Kariyer.Schedule/Kariyer.Schedule.csproj", "Kariyer.Schedule/"]
RUN dotnet restore "Kariyer.Api/Kariyer.Api.csproj"
COPY . .
WORKDIR "/src/Kariyer.Api"
RUN dotnet build "Kariyer.Api.csproj" -c Release -o /app/build

# Yayımlama aşaması
FROM build AS publish
RUN dotnet publish "Kariyer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Son aşama
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kariyer.Api.dll"]