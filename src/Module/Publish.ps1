
$publishApiKey = Read-Host -Prompt "Nuget API Key for PowerShell Gallery"

$tempFolder = New-TemporaryFile | %{ rm $_; mkdir $_ }
$publishFolder = mkdir $tempFolder\Arvan


Push-Location

cd $PSScriptRoot\..\..
dotnet build -c Release
Copy-Item src\Module\bin\Release\netstandard2.0\*.* $publishFolder -Verbose

cd $publishFolder
Publish-Module -Path $publishFolder -NuGetApiKey $publishApiKey -Verbose

Pop-Location
