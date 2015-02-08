$dest = "Nuget\lib\portable-windows8+net45+windowsphone8+wpa"
$spec = "Nuget\AngleSharp.nuspec"
New-Item $dest -type directory -Force
Copy-Item "AngleSharp\bin\Release\AngleSharp.dll" $dest
Copy-Item "AngleSharp\bin\Release\AngleSharp.xml" $dest
$file = $dest + "\AngleSharp.dll"
$ver = (Get-Item $file).VersionInfo.FileVersion
$file = "Nuget\AngleSharp." + $ver + ".nupkg"
$repl = '<version>' + $ver + '</version>'
(Get-Content $spec) | 
    Foreach-Object { $_ -replace "<version>([0-9\.]+)</version>", $repl } | 
    Set-Content $spec
nuget pack $spec -OutputDirectory "Nuget"
nuget push $file