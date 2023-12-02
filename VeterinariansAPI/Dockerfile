#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VeterinariansAPI/VeterinariansAPI.csproj", "VeterinariansAPI/"]
COPY ["301290835(Maria&Josue)3013473439_Project/301290835(Maria&Josue)3013473439_Project.csproj", "301290835(Maria&Josue)3013473439_Project/"]
RUN dotnet restore "VeterinariansAPI/VeterinariansAPI.csproj"
COPY . .
WORKDIR "/src/VeterinariansAPI"
RUN dotnet build "VeterinariansAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeterinariansAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeterinariansAPI.dll"]