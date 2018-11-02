FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 57557
EXPOSE 44322

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.PlanningPoker.Api/HexMaster.PlanningPoker.Api.csproj", "HexMaster.PlanningPoker.Api/"]
RUN dotnet restore "HexMaster.PlanningPoker.Api/HexMaster.PlanningPoker.Api.csproj"
COPY . .
WORKDIR "/src/HexMaster.PlanningPoker.Api"
RUN dotnet build "HexMaster.PlanningPoker.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.PlanningPoker.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.PlanningPoker.Api.dll"]