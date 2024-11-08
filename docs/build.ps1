# Build device tables
$files = Get-ChildItem .\harp_devices_src\harp.device.*\software\bonsai\device.yml
$files_root = Get-ChildItem .\harp_devices_src\harp.device.*\device.yml
$files += $files_root

foreach ($file in $files)
{
    Write-Output "Generating schema tables for $file..."
    $readmePath = (Get-Item $file).Directory.Parent.Parent.FullName + "\README.md"
    if (-Not (Test-Path $readmePath)) {
        $readmePath = (Get-Item $file).Directory.FullName + "\README.md"
    }
    $readmePath = ("." + (Resolve-Path -Relative $readmePath))
    $readmePath = $readmePath.Replace("\", "/")
    dotnet run --project .\harp_devices_src\harp.schemaprocessor $file .\harp_devices_spec $readmePath
}

# Find assemblies to build workflows
$baseDir = (Get-Item -Path "..\src" -Verbose).FullName
$folderPaths = Get-ChildItem -Path $baseDir -Directory
$suffix = "bin\Release\net*"
$packages = @()

# find package assemblies
foreach ($folderPath in $folderPaths) {
    $slnPath = Join-Path -Path $folderPath.FullName -ChildPath ($folderPath.Name + ".sln")
    if (Test-Path -Path $slnPath) {
        $libPath = Join-Path -Path $folderPath.FullName -ChildPath $suffix
        $matchingFolders = Get-ChildItem -Path $libPath -Directory | Where-Object { $_.Name -match "^net\d+" }
        if ($matchingFolders) {
            $packages += $matchingFolders.FullName
        }
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