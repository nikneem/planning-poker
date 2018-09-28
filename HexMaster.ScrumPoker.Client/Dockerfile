FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51293
EXPOSE 44352

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.ScrumPoker.Client/HexMaster.ScrumPoker.Client.csproj", "HexMaster.ScrumPoker.Client/"]
RUN dotnet restore "HexMaster.ScrumPoker.Client/HexMaster.ScrumPoker.Client.csproj"
COPY . .
WORKDIR "/src/HexMaster.ScrumPoker.Client"
RUN dotnet build "HexMaster.ScrumPoker.Client.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.ScrumPoker.Client.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.ScrumPoker.Client.dll"]