Param
(
 [switch]$dockerCompose,
 [switch]$ignoreDotNetBuild,
 [switch]$ignoreSqlBuild,
 [Parameter(Mandatory=$false)]
 [string]$verbosity="q"
)

$proj = (
	"Callisto.Web.App",
	"Callisto.Web.Api", 
	"Callisto.Web.Landing",
	"Callisto.Worker.Service"
)

function getPublishDir($project, $framework = "netcoreapp2.1"){
	return ".\src\$($project)\bin\Release\$framework";
}

if($ignoreDotNetBuild)
{
	Write-Host "Skipping dotnet build..."  -ForegroundColor Gray
}
else
{
	Write-Host "msbuild restore..." -ForegroundColor Cyan
	msbuild /t:restore callisto.sln /verbosity:$verbosity
	Write-Host "msbuild restore done" -ForegroundColor Cyan

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

		dotnet publish ".\src\$p" -c Release --no-restore -v $verbosity -nowarn:MSB3277

		if ($LASTEXITCODE -gt 0)
		{
			exit $LASTEXITCODE
		}

		Write-Host "dotnet publish done" -ForegroundColor Cyan
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

		msbuild $p\Callisto.Database.sqlproj /p:Configuration=Release /verbosity:$verbosity

		if ($LASTEXITCODE -gt 0)
		{
			exit $LASTEXITCODE
		}

	Move-Item -Path "$p\bin\Release\Callisto_Create.sql" -Destination .\docker\sql\ -Force

	Write-Host "sql build done" -ForegroundColor Cyan
}


if($dockerCompose)
{
	Write-Host "docker compose..."  -ForegroundColor Cyan
		
		docker-compose build
	
		if ($LASTEXITCODE -gt 0)
		{
			exit $LASTEXITCODE
		}

	Write-Host "docker compose done"
}
else
{
	Write-Host "Skipping docker compose..."  -ForegroundColor Gray
}