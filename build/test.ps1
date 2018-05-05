

function openCover() {
	$nugetOpenCoverPackage = Join-Path -Path $env:USERPROFILE -ChildPath "\.nuget\packages\OpenCover"
	$latestOpenCover = Join-Path -Path ((Get-ChildItem -Path $nugetOpenCoverPackage | Sort-Object Fullname -Descending)[0].FullName) -ChildPath "tools\OpenCover.Console.exe"
	return $latestOpenCover
}


function cobertura() {
	$nugetCoberturaConverterPackage = Join-Path -Path $env:USERPROFILE -ChildPath "\.nuget\packages\OpenCoverToCoberturaConverter"
	$latestCoberturaConverter = Join-Path -Path (Get-ChildItem -Path $nugetCoberturaConverterPackage | Sort-Object Fullname -Descending)[0].FullName -ChildPath "tools\OpenCoverToCoberturaConverter.exe"
	return $latestCoberturaConverter
}

function cleanResults()
{ 
	Write-Host "Cleaning results..."  -ForegroundColor Gray
	if(!(Test-Path "test\Results"))
	{
		New-Item -ItemType Directory -Force -Path "test\Results"
	}

	if(Test-Path "test\Results\*")
	{
		Remove-Item -Recurse "test\Results\*"
	}
}

$testProjects = get-childitem .\test -Depth 2 | where { $_.extension -eq ".csproj" }

echo $testProjects

cleanResults

$openCover = openCover
$cobertura = cobertura
 $t = ""
foreach($testProject in $testProjects)
{
    $t = $testProject.Directory.Parent.FullName

	Write-Host "testing $($testProject)..." -ForegroundColor Green

    $dotnetArguments = "xunit" `
    ,"--fx-version 2.0.0" `
    , "-xml `"$t\Results\$($testProject.BaseName).testresults`"" `
    , "-configuration Debug" `

    Write-Host "with args $($dotnetArguments)..." -ForegroundColor Gray

     & $openCover `
        -register:user `
        -target:dotnet.exe `
        -targetdir:$t\$($testProject.Directory.BaseName) `
        "-targetargs:$dotnetArguments" `
        -returntargetcode `
        -output:"$t\Results\OpenCover.coverageresults" `
        -mergeoutput `
        -oldStyle `
        -excludebyattribute:System.CodeDom.Compiler.GeneratedCodeAttribute `
		-filter:"+[Callisto.*]* -[*Tests]*"

	& $cobertura `
    -input:"$t\Results\OpenCover.coverageresults" `
    -output:"$t\Results\Cobertura.coverageresults" `
    "-sources:$t\Results"
}



