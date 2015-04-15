$spec = "Nuget\AngleSharp.nuspec"

Function Update-Content($target, $source) {
    $dest = "Nuget\lib\$target"
    New-Item $dest -type directory -Force
    Copy-Item "$source\bin\Release\AngleSharp.dll" $dest
    Copy-Item "$source\bin\Release\AngleSharp.xml" $dest
}

Function Update-Version($file) {
    $ver = (Get-Item $file).VersionInfo.FileVersion
    $repl = "<version>$ver</version>"
    (Get-Content $spec) | 
        Foreach-Object { $_ -replace "<version>(.*)</version>", $repl } | 
        Set-Content $spec
    return $ver
}

Function Publish-Package($ver) {
    $file = "Nuget\AngleSharp.$ver.nupkg"
    nuget pack $spec -OutputDirectory "Nuget"
    nuget push $file
    return $LastExitCode
}

Update-Content "net40" "AngleSharp.Legacy"
Update-Content "sl50" "AngleSharp.Silverlight"
Update-Content "portable-windows8+net45+windowsphone8+wpa" "AngleSharp"

$version = Update-Version "Nuget\lib\net40\AngleSharp.dll"
$success = Publish-Package $version