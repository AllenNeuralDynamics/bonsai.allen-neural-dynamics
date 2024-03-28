# Build device tables
$files = Get-ChildItem .\harp_devices_src\harp.device.*\software\bonsai\device.yml
foreach ($file in $files)
{
    Write-Output "Generating schema tables for $file..."
    $readmePath =  (get-item $file ).Directory.Parent.Parent.FullName + "\README.md"
    $readmePath = ("." + (Resolve-Path -Relative $readmePath))
    $readmePath = $readmePath.Replace("\", "/")
    dotnet run --project .\harp_devices_src\harp.schemaprocessor $file .\harp_devices_spec $readmePath
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

$harp_solutions = Get-ChildItem .\harp_devices_src\harp.device.*\software\bonsai\Interface\**\bin\Release\net4* -Directory
foreach ($libPath in $harp_solutions) {
    $packages += $(Resolve-Path($libPath) -Relative)
}

Write-Host ("Found the following packages: " + $packages)
.\bonsai\modules\Export-Image.ps1 $packages

# Build documentation
dotnet docfx @args