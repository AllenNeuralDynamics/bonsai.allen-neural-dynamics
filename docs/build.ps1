$baseDir = (Get-Item -Path "..\src" -Verbose).FullName
$folderPaths = Get-ChildItem -Path $baseDir -Directory
$sufix = "bin\Release\net472"
$packages = @()
foreach ($folderPath in $folderPaths) {
    $snlPath = Join-Path -Path $folderPath.FullName -ChildPath ($folderPath.Name + ".sln")
    if (Test-Path -Path $snlPath) {
        $libPath = Join-Path -Path $folderPath.FullName -ChildPath $sufix
        $packages += $libPath
    }
}
Write-Host ("Found the following packages: " + $packages)
.\bonsai\modules\Export-Image.ps1 $packages
dotnet docfx @args