#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MSC.API/MSC.API.csproj", "MSC.API/"]
COPY ["MSC.Core/MSC.Core.csproj", "MSC.Core/"]
COPY ["MSC.Widget/MSC.Widget.csproj", "MSC.Widget/"]
COPY ["MSC.Domain/MSC.Domain.csproj", "MSC.Domain/"]
COPY ["MSC.Data/MSC.Data.csproj", "MSC.Data/"]
RUN dotnet restore "MSC.API/MSC.API.csproj"
COPY . .
WORKDIR "/src/MSC.API"
RUN dotnet build "MSC.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSC.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSC.API.dll"]
