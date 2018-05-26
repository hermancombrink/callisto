Param
(
 [switch]$submitResults,
 [Parameter(Mandatory=$false)]
 [string]$verbosity="q",
 [Parameter(Mandatory=$false)]
 [string]$fxversion="2.0.0"
)

function getTool([string]$name, [string]$tool) {
	$packageDir = Join-Path -Path $env:USERPROFILE -ChildPath "\.nuget\packages\$name"
	$latestTool = Join-Path -Path (Get-ChildItem -Path $packageDir | Sort-Object Fullname -Descending)[0].FullName -ChildPath "tools\$tool.exe"
	return $latestTool
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

cleanResults

$openCover = getTool -name "OpenCover" -tool "OpenCover.Console"
$cobertura = getTool -name "OpenCoverToCoberturaConverter" -tool "OpenCoverToCoberturaConverter"
$reportGen = getTool -name "reportgenerator" -tool "ReportGenerator"
$codecov   = getTool -name "codecov" -tool "codecov"

foreach($testProject in $testProjects)
{
    $t = $testProject.Directory.Parent.FullName

	Write-Host "testing $($testProject)..." -ForegroundColor Green

    $dotnetArguments = "xunit" `
	, "--fx-version $fxversion" `
	, "-msbuildverbosity $verbosity" `
    , "-xml `"$t\Results\$($testProject.BaseName).testresults`"" `
    , "-configuration Debug" 

    Write-Host "with args $($dotnetArguments)..." -ForegroundColor Gray

     & $openCover `
        -register:user `
        -target:dotnet.exe `
        -targetdir:$t\$($testProject.Directory.BaseName) `
        -targetargs:"$dotnetArguments" `
        -returntargetcode `
        -output:"$t\Results\OpenCover.coverageresults" `
        -mergeoutput `
        -oldStyle `
        -excludebyattribute:System.CodeDom.Compiler.GeneratedCodeAttribute `
		-filter:"+[Callisto.*]* -[*Tests]*"

		if ($LASTEXITCODE -gt 0)
		{
			exit $LASTEXITCODE
		}
}

Write-Host "converting coverage to Cobertura..." -ForegroundColor Green
& $cobertura `
-input:"test\Results\OpenCover.coverageresults" `
-output:"test\Results\Cobertura.coverageresults" `
-sources:"test\Results"

	if ($LASTEXITCODE -gt 0)
		{
			exit $LASTEXITCODE
		}

Write-Host "generating html output..." -ForegroundColor Green
& $reportGen `
-targetdir:"test\Results\Coverage" `
-reports:"test\Results\Cobertura.coverageresults" `
-reporttypes:"Html;HtmlChart;HtmlSummary" 

	if ($LASTEXITCODE -gt 0)
		{
			exit $LASTEXITCODE
		}

if($submitResults)
{
	Write-Host "submitting coverage to github..." -ForegroundColor Green
	& $codecov `
	-f "test\Results\OpenCover.coverageresults" `
	-t "cdf4eeb7-be3f-4879-8ec1-620cdeb9529a"

		if ($LASTEXITCODE -gt 0)
		{
			exit $LASTEXITCODE
		}
}
else
{
	Write-Host "Skipping coverage to github..."  -ForegroundColor Gray
}
