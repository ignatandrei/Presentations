cls
$folderResultsTests = "./TestResults"

if (Test-Path $folderResultsTests)
{
    Remove-Item -Path $folderResultsTests -Recurse
}

dotnet tool restore

dotnet test --collect:"XPlat Code Coverage"   --results-directory:$folderResultsTests

$xmlFile = Get-ChildItem $folderResultsTests -Recurse | Where-Object { $_.Extension -eq ".xml" } | Select-Object -First 1

Write-Host " find coverage" 
Write-Host  $xmlFile.FullName

reportgenerator -reports:"$xmlFile" -targetdir:"$folderResultsTests" -reporttypes:Html

$htmlFile = Get-ChildItem $folderResultsTests -Recurse | Where-Object { $_.Extension -eq ".html" } | Select-Object -First 1

Write-Host " please open " 
Write-Host  $htmlFile.FullName

Start-Process -FilePath "iexplore.exe" -ArgumentList $htmlFile


