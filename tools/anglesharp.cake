#addin "Cake.FileHelpers"
#addin "Octokit"
using Octokit;

var configuration = Argument("configuration", "Release");
var isRunningOnUnix = IsRunningOnUnix();
var isRunningOnWindows = IsRunningOnWindows();
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;
var buildNumber = AppVeyor.Environment.Build.Number;
var releaseNotes = ParseReleaseNotes("./CHANGELOG.md");
var version = releaseNotes.Version.ToString();
var buildDir = Directory($"./src/{projectName}/bin") + Directory(configuration);
var buildResultDir = Directory("./bin") + Directory(version);
var nugetRoot = buildResultDir + Directory("nuget");

if (target == "PrePublish")
{
    version = $"{version}-alpha-{buildNumber}";
}

if (!isRunningOnWindows)
{
    frameworks.Remove("net46");
}

// Initialization
// ----------------------------------------

Setup(_ =>
{
    Information($"Building version {version} of {projectName}.");
    Information("For the publish target the following environment variables need to be set:");
    Information("- NUGET_API_KEY");
    Information("- GITHUB_API_TOKEN");
});

// Tasks
// ----------------------------------------

Task("Clean")
    .Does(() =>
    {
        CleanDirectories(new DirectoryPath[] { buildDir, buildResultDir, nugetRoot });
    });

Task("Restore-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        NuGetRestore($"./src/{solutionName}.sln", new NuGetRestoreSettings
        {
            ToolPath = "tools/nuget.exe",
        });
    });

Task("Build")
    .IsDependentOn("Restore-Packages")
    .Does(() =>
    {
        ReplaceRegexInFiles("./src/Directory.Build.props", "(?<=<Version>)(.+?)(?=</Version>)", version);
        DotNetCoreBuild($"./src/{solutionName}.sln", new DotNetCoreBuildSettings
        {
           Configuration = configuration,
        });
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
        };

        if (isRunningOnAppVeyor)
        {
            settings.TestAdapterPath = Directory(".");
            settings.Logger = "Appveyor";
            // TODO Finds a way to exclude tests not allowed to run on appveyor
            // Not used in current code
            //settings.Where = "cat != ExcludeFromAppVeyor";
        }

        DotNetCoreTest($"./src/{solutionName}.Tests/", settings);
    });

Task("Copy-Files")
    .IsDependentOn("Build")
    .Does(() =>
    {
        foreach (var item in frameworks)
        {
            var targetDir = nugetRoot + Directory("lib") + Directory(item.Key);
            CreateDirectory(targetDir);
            CopyFiles(new FilePath[]
            {
                buildDir + Directory(item.Value) + File($"{projectName}.dll"),
                buildDir + Directory(item.Value) + File($"{projectName}.xml"),
            }, targetDir);
        }

        CopyFiles(new FilePath[] { $"src/{projectName}.nuspec" }, nugetRoot);
    });

Task("Create-Package")
    .IsDependentOn("Copy-Files")
    .Does(() =>
    {
        var nugetExe = GetFiles("./tools/**/nuget.exe").FirstOrDefault()
            ?? (isRunningOnAppVeyor ? GetFiles("C:\\Tools\\NuGet3\\nuget.exe").FirstOrDefault() : null)
            ?? throw new InvalidOperationException("Could not find nuget.exe.");

        var nuspec = nugetRoot + File($"{projectName}.nuspec");

        NuGetPack(nuspec, new NuGetPackSettings
        {
            Version = version,
            OutputDirectory = nugetRoot,
            Symbols = false,
            Properties = new Dictionary<String, String>
            {
                { "Configuration", configuration },
            },
        });
    });

Task("Publish-Package")
    .IsDependentOn("Create-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        var apiKey = EnvironmentVariable("NUGET_API_KEY");

        if (String.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("Could not resolve the NuGet API key.");
        }

        foreach (var nupkg in GetFiles(nugetRoot.Path.FullPath + "/*.nupkg"))
        {
            NuGetPush(nupkg, new NuGetPushSettings
            {
                Source = "https://nuget.org/api/v2/package",
                ApiKey = apiKey,
            });
        }
    });

Task("Publish-Release")
    .IsDependentOn("Publish-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        var githubToken = EnvironmentVariable("GITHUB_API_TOKEN");

        if (String.IsNullOrEmpty(githubToken))
        {
            throw new InvalidOperationException("Could not resolve GitHub token.");
        }

        var github = new GitHubClient(new ProductHeaderValue("AngleSharpCakeBuild"))
        {
            Credentials = new Credentials(githubToken),
        };

        var newRelease = github.Repository.Release;
        newRelease.Create("AngleSharp", projectName, new NewRelease("v" + version)
        {
            Name = version,
            Body = String.Join(Environment.NewLine, releaseNotes.Notes),
            Prerelease = false,
            TargetCommitish = "master",
        }).Wait();
    });

Task("Update-AppVeyor-Build-Number")
    .WithCriteria(() => isRunningOnAppVeyor)
    .Does(() =>
    {
        var num = AppVeyor.Environment.Build.Number;
        AppVeyor.UpdateBuildVersion($"{version}-{num}");
    });

// Targets
// ----------------------------------------

Task("Package")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Create-Package");

Task("Default")
    .IsDependentOn("Package");

Task("Publish")
    .IsDependentOn("Publish-Package")
    .IsDependentOn("Publish-Release");

Task("PrePublish")
    .IsDependentOn("Publish-Package");

Task("AppVeyor")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Update-AppVeyor-Build-Number");
