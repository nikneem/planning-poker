FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51175
EXPOSE 44371

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.ScrumPoker.Api/HexMaster.ScrumPoker.Api.csproj", "HexMaster.ScrumPoker.Api/"]
RUN dotnet restore "HexMaster.ScrumPoker.Api/HexMaster.ScrumPoker.Api.csproj"
COPY . .
WORKDIR "/src/HexMaster.ScrumPoker.Api"
RUN dotnet build "HexMaster.ScrumPoker.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.ScrumPoker.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.ScrumPoker.Api.dll"]