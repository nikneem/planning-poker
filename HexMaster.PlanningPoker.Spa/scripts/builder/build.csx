Execute(() => {
    Cmd($"dotnet restore {pathToSolution}/server");
    // Cmd($"dotnet test {pathToApiTests}");
    Cmd($"dotnet publish {pathToApiApp} -o {pathToApiOut} -c Release");
}, "Build ASP.NET Core Api", "build-api");

Execute(() => {
    Cmd($"yarn install --production=false", pathToWebApp);
    Cmd($"ng build --app planningpoker --prod", pathToWebApp);
}, "Build Angular App", "build-web");