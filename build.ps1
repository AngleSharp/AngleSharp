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
$TOOLS_DIR = Join-Path $PSScriptRoot "tools"
$NUGET_EXE = Join-Path $TOOLS_DIR "nuget.exe"
$CAKE_EXE = Join-Path $TOOLS_DIR "Cake/Cake.exe"
$NUGET_URL = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

# Should we use experimental build of Roslyn?
$UseExperimental = "";
if($Experimental.IsPresent) {
    $UseExperimental = "-experimental"
}

# Is this a dry run?
$UseDryRun = "";
if($WhatIf.IsPresent) {
    $UseDryRun = "-dryrun"
}

# Should we use mono?
$UseMono = "";
if($Mono.IsPresent) {
    $UseMono = "-mono"
}

# Try download NuGet.exe if do not exist.
if (!(Test-Path $NUGET_EXE)) {
    (New-Object System.Net.WebClient).DownloadFile($NUGET_URL, $NUGET_EXE)
}

# Make sure NuGet exists where we expect it.
if (!(Test-Path $NUGET_EXE)) {
    Throw "Could not find NuGet.exe"
}

# Restore tools from NuGet?
if(-Not $SkipToolPackageRestore.IsPresent)
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
if (!(Test-Path $CAKE_EXE)) {
    Throw "Could not find Cake.exe"
}

# Start Cake
Invoke-Expression "$CAKE_EXE `"$Script`" -target=`"$Target`" -configuration=`"$Configuration`" -verbosity=`"$Verbosity`" $UseMono $UseDryRun $UseExperimental $ScriptArgs"
exit $LASTEXITCODE