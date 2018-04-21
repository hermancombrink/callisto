Param
(
 [switch]$dockerCompose,
 [switch]$ignoreDotNetBuild,
 [switch]$ignoreSqlBuild,
 [Parameter(Mandatory=$false)]
 [string]$verbosity="q",
 [Parameter(Mandatory=$false)]
 [string]$msbuild="C:\'Program Files (x86)'\'Microsoft Visual Studio'\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"
)

$proj = (
	"Callisto.Web.Landing",
	"Callisto.Web.Api", 
	"Callisto.Web.App"
)

function getPublishDir($project, $framework = "netcoreapp2.0"){
	return ".\src\$($project)\bin\Release\$framework";
}

if($ignoreDotNetBuild)
{
	Write-Host "Skipping dotnet build..."  -ForegroundColor Gray
}
else
{
	Write-Host "dotnet restore..." -ForegroundColor Cyan
	Write-Host $msbuild
	$restore = $msbuild + " /t:restore callisto.sln  /verbosity:$verbosity"
	Invoke-Expression $restore
	#dotnet restore callisto.sln -nowarn:msb3202,nu1503 -v $verbosity

	foreach( $p in $proj)
	{
		Write-Host "building $p..." -ForegroundColor Green
		Write-Host "dotnet publish..." -ForegroundColor Cyan
		$t = getPublishDir $p
		if(Test-Path -Path $t)
		{
			Write-Host "cleaning publish..."  -ForegroundColor Gray
			Remove-Item -Path $t -Force -Recurse
		}

		if(Test-Path -Path ".\src\$p\bower.json")
		{
			Write-Host "bower install..."  -ForegroundColor Cyan
			pushd ".\src\$p"
			bower install 
			popd
		}

		if(Test-Path -Path ".\src\$p\package.json")
		{
			Write-Host "npm install..."  -ForegroundColor Cyan
			pushd ".\src\$p"
			npm install 
			popd
		}

		dotnet publish ".\src\$p" -c Release --no-restore -v $verbosity
	}
}

if($ignoreSqlBuild)
{
	Write-Host "Skipping sql build..."  -ForegroundColor Gray
}
else
{
	Write-Host "sql build..." -ForegroundColor Cyan
	$p = ".\src\Callisto.Database\"
	$t = getPublishDir "Callisto.Database" -framework ''
	Write-Host $t
		if(Test-Path -Path $t)
		{
			Write-Host "cleaning publish..."  -ForegroundColor Gray
			Remove-Item -Path $t -Force -Recurse
		}
	$build = $msbuild + " $p\Callisto.Database.sqlproj /p:Configuration=Release /verbosity:$verbosity"
	Invoke-Expression $build
	Move-Item -Path "$p\bin\Release\Callisto_Create.sql" -Destination .\docker\sql\ -Force
}


if($dockerCompose)
{
	Write-Host "docker compose..."  -ForegroundColor Cyan
	docker-compose build
}
else
{
	Write-Host "Skipping docker compose..."  -ForegroundColor Gray
}