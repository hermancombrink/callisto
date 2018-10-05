
function cleanResults()
{ 
	Write-Host "Cleaning results..."  -ForegroundColor Gray
    $infofiles = get-childitem .\test | Where-Object { $_.extension -eq ".info" }

    foreach($f in $infofiles)
    {
      Write-Host "removing $($f)..." -ForegroundColor Gray
      Remove-Item -Recurse ".\test\$f"
    }
}

$testProjects = get-childitem .\test -Depth 2 | Where-Object { $_.extension -eq ".csproj" -and $_.Name.Contains("Tests") }

cleanResults


foreach($testProject in $testProjects)
{
    $t = $testProject.Directory.Parent.FullName

	Write-Host "testing $($testProject)..." -ForegroundColor Green
	$proj = "$t\$($testProject.BaseName)\$testProject"
    $outfile = "$($testProject.BaseName).info"
	Write-Host  $proj
	#dotnet build $proj --configuration Debug
    dotnet test $proj /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../$outfile

}
