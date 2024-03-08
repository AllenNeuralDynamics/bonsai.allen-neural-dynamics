$baseDir = (Get-Item -Path "..\src" -Verbose).FullName
$folderPaths = Get-ChildItem -Path $baseDir -Directory
foreach ($folderPath in $folderPaths) {
    $snlPath = Join-Path -Path $folderPath.FullName -ChildPath ($folderPath.Name + ".sln")
    if (Test-Path -Path $snlPath) {
        Write-Host ("Building: " + $snlPath)
        $command = ("msbuild -t:restore " + $snlPath)
        Invoke-Expression $command
        $command = ("msbuild" + $snlPath + " /p:Configuration=Release")
        Invoke-Expression $command
    }
}
