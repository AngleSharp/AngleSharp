using Microsoft.Build.Exceptions;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Utilities.Collections;
using Octokit;
using Octokit.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuke.Common.Tooling;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;
using Project = Nuke.Common.ProjectModel.Project;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.RunUnitTests);

    [Nuke.Common.Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Nuke.Common.Parameter("ReleaseNotesFilePath - To determine the SemanticVersion")]
    readonly AbsolutePath ReleaseNotesFilePath = RootDirectory / "CHANGELOG.md";

    [Solution]
    readonly Solution Solution;

    string TargetProjectName => "AngleSharp";

    string TargetLibName => $"{TargetProjectName}.Core";

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath BuildDirectory => SourceDirectory / TargetProjectName / "bin" / Configuration;

    AbsolutePath ResultDirectory => RootDirectory / "bin" / Version;

    AbsolutePath NugetDirectory => ResultDirectory / "nuget";

    GitHubActions GitHubActions => GitHubActions.Instance;

    Project TargetProject { get; set; }

    // Note: The ChangeLogTasks from Nuke itself look buggy. So using the Cake source code.
    IReadOnlyList<ReleaseNotes> ChangeLog { get; set; }

    ReleaseNotes LatestReleaseNotes { get; set; }

    SemVersion SemVersion { get; set; }

    string Version { get; set; }

    IReadOnlyCollection<string> TargetFrameworks { get; set; }

    protected override void OnBuildInitialized()
    {
        var parser = new ReleaseNotesParser();

        Log.Debug("Reading ChangeLog {FilePath}...", ReleaseNotesFilePath);
        ChangeLog = parser.Parse(File.ReadAllText(ReleaseNotesFilePath));
        ChangeLog.NotNull("ChangeLog / ReleaseNotes could not be read!");

        LatestReleaseNotes = ChangeLog.First();
        LatestReleaseNotes.NotNull("LatestVersion could not be read!");

        Log.Debug("Using LastestVersion from ChangeLog: {LatestVersion}", LatestReleaseNotes.Version);
        SemVersion = LatestReleaseNotes.SemVersion;
        Version = LatestReleaseNotes.Version.ToString();

        if (GitHubActions != null)
        {
            Log.Debug("Add Version Postfix if under CI - GithubAction(s)...");

            var buildNumber = GitHubActions.RunNumber;

            if (ScheduledTargets.Contains(Default))
            {
                Version = $"{Version}-ci.{buildNumber}";
            }
            else if (ScheduledTargets.Contains(PrePublish))
            {
                Version = $"{Version}-beta.{buildNumber}";
            }
        }

        Log.Information("Building version: {Version}", Version);

        TargetProject = Solution.GetProject(TargetLibName);
        TargetProject.NotNull("TargetProject could not be loaded!");

        TargetFrameworks = TargetProject.GetTargetFrameworks();
        TargetFrameworks.NotNull("No TargetFramework(s) found to build for!");

        Log.Information("Target Framework(s): {Frameworks}", TargetFrameworks);
    }

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(x => x.DeleteDirectory());
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetContinuousIntegrationBuild(IsServerBuild)
                .EnableNoRestore());
        });

    Target RunUnitTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetProcessEnvironmentVariable("prefetched", "false")
                .When(GitHubActions.Instance is not null, x => x.SetLoggers("GitHubActions"))
            );

            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetProcessEnvironmentVariable("prefetched", "true")
                .When(GitHubActions.Instance is not null, x => x.SetLoggers("GitHubActions"))
            );
        });

    Target CopyFiles => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            foreach (var item in TargetFrameworks)
            {
                var targetDir = NugetDirectory / "lib" / item;
                var srcDir = BuildDirectory / item;

                CopyFile(srcDir / $"{TargetProjectName}.dll", targetDir / $"{TargetProjectName}.dll", FileExistsPolicy.OverwriteIfNewer);
                CopyFile(srcDir / $"{TargetProjectName}.pdb", targetDir / $"{TargetProjectName}.pdb", FileExistsPolicy.OverwriteIfNewer);
                CopyFile(srcDir / $"{TargetProjectName}.xml", targetDir / $"{TargetProjectName}.xml", FileExistsPolicy.OverwriteIfNewer);
            }

            CopyFile(SourceDirectory / $"{TargetProjectName}.nuspec", NugetDirectory / $"{TargetProjectName}.nuspec", FileExistsPolicy.OverwriteIfNewer);
            CopyFile(RootDirectory / "logo.png", NugetDirectory / "logo.png", FileExistsPolicy.OverwriteIfNewer);
            CopyFile(RootDirectory / "README.md", NugetDirectory / "README.md", FileExistsPolicy.OverwriteIfNewer);
        });

    Target CreatePackage => _ => _
        .DependsOn(CopyFiles)
        .Executes(() =>
        {
            var nuspec = NugetDirectory / $"{TargetProjectName}.nuspec";

            NuGetPack(_ => _
                .SetTargetPath(nuspec)
                .SetVersion(Version)
                .SetOutputDirectory(NugetDirectory)
                .EnableSymbols()
                .SetSymbolPackageFormat(NuGetSymbolPackageFormat.snupkg)
                .SetConfiguration(Configuration)
            );
        });

    Target PublishPackage => _ => _
        .DependsOn(CreatePackage)
        .DependsOn(RunUnitTests)
        .Executes(() =>
        {
            var apiKey = Environment.GetEnvironmentVariable("NUGET_API_KEY");


            if (apiKey.IsNullOrEmpty())
            {
                throw new BuildAbortedException("Could not resolve the NuGet API key.");
            }

            foreach (var nupkg in NugetDirectory.GlobFiles("*.nupkg"))
            {
                NuGetPush(s => s
                    .SetTargetPath(nupkg)
                    .SetSource("https://api.nuget.org/v3/index.json")
                    .SetApiKey(apiKey));
            }
        });

    Target PublishPreRelease => _ => _
        .DependsOn(PublishPackage)
        .Executes(() =>
        {
            string gitHubToken;

            if (GitHubActions != null)
            {
                gitHubToken = GitHubActions.Token;
            }
            else
            {
                gitHubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            }

            if (gitHubToken.IsNullOrEmpty())
            {
                throw new BuildAbortedException("Could not resolve GitHub token.");
            }

            var credentials = new Credentials(gitHubToken);

            GitHubTasks.GitHubClient = new GitHubClient(
                new ProductHeaderValue(nameof(NukeBuild)),
                new InMemoryCredentialStore(credentials));

            GitHubTasks.GitHubClient.Repository.Release
                .Create("AngleSharp", TargetProjectName, new NewRelease(Version)
                {
                    Name = Version,
                    Body = String.Join(Environment.NewLine, LatestReleaseNotes.Notes),
                    Prerelease = true,
                    TargetCommitish = "devel",
                });
        });

    Target PublishRelease => _ => _
        .DependsOn(PublishPackage)
        .Executes(() =>
        {
            string gitHubToken;

            if (GitHubActions != null)
            {
                gitHubToken = GitHubActions.Token;
            }
            else
            {
                gitHubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            }

            if (gitHubToken.IsNullOrEmpty())
            {
                throw new BuildAbortedException("Could not resolve GitHub token.");
            }

            var credentials = new Credentials(gitHubToken);

            GitHubTasks.GitHubClient = new GitHubClient(
                new ProductHeaderValue(nameof(NukeBuild)),
                new InMemoryCredentialStore(credentials));

            GitHubTasks.GitHubClient.Repository.Release
                .Create("AngleSharp", TargetProjectName, new NewRelease(Version)
                {
                    Name = Version,
                    Body = String.Join(Environment.NewLine, LatestReleaseNotes.Notes),
                    Prerelease = false,
                    TargetCommitish = "main",
                });
        });

    Target Package => _ => _
        .DependsOn(RunUnitTests)
        .DependsOn(CreatePackage);

    Target Default => _ => _
        .DependsOn(Package);

    Target Publish => _ => _
        .DependsOn(PublishRelease);

    Target PrePublish => _ => _
        .DependsOn(PublishPreRelease);
}
