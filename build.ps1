Param(
    [string]$Script = "build.cake",
    [string]$Target = "Default",
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    [ValidateSet("Quiet", "Minimal", "Normal", "Verbose", "Diagnostic")]
    [string]$Verbosity = "Verbose",
    [switch]$Experimental,
    [switch]$WhatIf,
    [switch]$Mono,
    [switch]$SkipToolPackageRestore,
    [Parameter(Position=0,Mandatory=$false,ValueFromRemainingArguments=$true)]
    [string[]]$ScriptArgs
)

$PSScriptRoot = split-path -parent $MyInvocation.MyCommand.Definition;
$UseDryRun = "";
$UseMono = "";
$TOOLS_DIR = Join-Path $PSScriptRoot "tools"
$NUGET_EXE = Join-Path $TOOLS_DIR "nuget.exe"
$NUGET_OLD_EXE = Join-Path $TOOLS_DIR "nuget_old.exe"
$CAKE_DLL = Join-Path $TOOLS_DIR "Cake.Tool/tools/net6.0/any/Cake.dll"
$NUGET_URL = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$NUGET_OLD_URL = "https://dist.nuget.org/win-x86-commandline/v3.5.0/nuget.exe"

# Should we use experimental build of Roslyn?
$UseExperimental = "";
if ($Experimental.IsPresent) {
    $UseExperimental = "--experimental"
}

# Is this a dry run?
if ($WhatIf.IsPresent) {
    $UseDryRun = "--dryrun"
}

# Should we use mono?
if ($Mono.IsPresent) {
    $UseMono = "--mono"
}

# Try download NuGet.exe if do not exist.
if (!(Test-Path $NUGET_EXE)) {
    (New-Object System.Net.WebClient).DownloadFile($NUGET_URL, $NUGET_EXE)
}

# Try download NuGet.exe if do not exist.
if (!(Test-Path $NUGET_OLD_URL)) {
    (New-Object System.Net.WebClient).DownloadFile($NUGET_OLD_URL, $NUGET_OLD_EXE)
}

# Make sure NuGet (latest) exists where we expect it.
if (!(Test-Path $NUGET_EXE)) {
    Throw "Could not find nuget.exe"
}

# Make sure NuGet (v3.5.0) exists where we expect it.
if (!(Test-Path $NUGET_OLD_EXE)) {
    Throw "Could not find nuget_old.exe"
}

# Restore tools from NuGet?
if (-Not $SkipToolPackageRestore.IsPresent)
{
    Push-Location
    Set-Location $TOOLS_DIR
    Invoke-Expression "$NUGET_EXE install -ExcludeVersion"
    Pop-Location
    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }
}

# Make sure that Cake has been installed.
if (!(Test-Path $CAKE_DLL)) {
    Throw "Could not find Cake.exe"
}

# Start Cake
Invoke-Expression "dotnet $CAKE_DLL `"$Script`" --target=`"$Target`" --configuration=`"$Configuration`" --verbosity=`"$Verbosity`" $UseMono $UseDryRun $UseExperimental $ScriptArgs"
exit $LASTEXITCODE