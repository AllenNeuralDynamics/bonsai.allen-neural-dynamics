# Build Bonsai packages
$baseDir = (Get-Item -Path "..\src" -Verbose).FullName
$folderPaths = Get-ChildItem -Path $baseDir -Directory
foreach ($folderPath in $folderPaths) {
    $slnPath = Join-Path -Path $folderPath.FullName -ChildPath ($folderPath.Name + ".sln")
    if (Test-Path -Path $slnPath) {
        Write-Host ("Building: " + $slnPath)
        $command = ("msbuild -t:restore " + $slnPath)
        Invoke-Expression $command
        $command = ("msbuild " + $slnPath + " /p:Configuration=Release")
        Invoke-Expression $command
    }
}

# Build Harp Interfaces
$harp_solutions = Get-ChildItem "..\docs\harp_devices_src\harp.device.*\software\bonsai\Interface\*.sln"
foreach ($slnPath in $harp_solutions) {
    Write-Host ("Building: " + $slnPath)
    $command = ("msbuild -t:restore " + $slnPath)
    Invoke-Expression $command
    $command = ("msbuild " + $slnPath + " /p:Configuration=Release")
    Invoke-Expression $command
}
