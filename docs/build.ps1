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
$harp_solutions = Get-ChildItem .\harp_devices_src\harp.device.*\software\bonsai\Interface\*.sln
foreach ($slnPath in $harp_solutions) {
    Write-Host ("Building: " + $slnPath)
    $command = ("msbuild -t:restore " + $slnPath)
    Invoke-Expression $command
    $command = ("msbuild " + $slnPath + " /p:Configuration=Release")
    Invoke-Expression $command
}

# Build device tables
$files = Get-ChildItem .\harp_devices_src\harp.device.*\software\bonsai\device.yml
foreach ($file in $files)
{
    Write-Output "Generating schema tables for $file..."
    dotnet run --project .\harp_devices_src\harp.schemaprocessor $file .\harp_devices
}


# Find assemblies to build workflows
$baseDir = (Get-Item -Path "..\src" -Verbose).FullName
$folderPaths = Get-ChildItem -Path $baseDir -Directory
$sufix = "bin\Release\net472"
$packages = @()


# find package assemblies
foreach ($folderPath in $folderPaths) {
    $snlPath = Join-Path -Path $folderPath.FullName -ChildPath ($folderPath.Name + ".sln")
    if (Test-Path -Path $snlPath) {
        $libPath = Join-Path -Path $folderPath.FullName -ChildPath $sufix
        $packages += $libPath
    }
}

# find device assemblies
$sufix = "bin\Release\netstandard2.0"

$harp_solutions = Get-ChildItem .\docs\harp_devices_src\harp.device.*\software\bonsai\Interface\*.sln
foreach ($solution in $harp_solutions) {
    $parent = Split-Path -Path $solution -Parent
    $solution_folder = Get-ChildItem -Path $parent -Directory
    $libPath = Join-Path -Path (Join-Path -Path $parent  -ChildPath $solution_folder[0]) -ChildPath $sufix # grab the lib
    $packages += $libPath
}


Write-Host ("Found the following packages: " + $packages)
.\bonsai\modules\Export-Image.ps1 $packages

# Build documentation
dotnet docfx @args