FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 57411
EXPOSE 44398

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.ScrumPoker.Gateway/HexMaster.ScrumPoker.Gateway.csproj", "HexMaster.ScrumPoker.Gateway/"]
RUN dotnet restore "HexMaster.ScrumPoker.Gateway/HexMaster.ScrumPoker.Gateway.csproj"
COPY . .
WORKDIR "/src/HexMaster.ScrumPoker.Gateway"
RUN dotnet build "HexMaster.ScrumPoker.Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.ScrumPoker.Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.ScrumPoker.Gateway.dll"]