$files = Get-ChildItem .\src\HarpDevices\harp.device.*\software\bonsai\device.yml
foreach ($file in $files)
{
    Write-Output "Generating schema tables for $file..."
    dotnet run --project .\src\harp.schemaprocessor $file .\docs\harp_devices
}