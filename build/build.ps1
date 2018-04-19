Param
(
 [switch]$dockerCompose
)

$proj = (
	"Callisto.Web.Landing"
)

function getPublishDir($project, $framework = "netcoreapp2.0"){
	return ".\src\$project\bin\Release\$framework";
}

Write-Host "dotnet restore..." -ForegroundColor Cyan
dotnet restore -nowarn:msb3202,nu1503 -v q 

foreach( $p in $proj)
{
	Write-Host "building $p..." -ForegroundColor Green
	Write-Host "dotnet publish..." -ForegroundColor Cyan
	$t = getPublishDir $p
	Remove-Item -Path $t -Force -Recurse

	if(Test-Path -Path ".\src\$p\bower.json")
	{
		Write-Host "bower install..."  -ForegroundColor Cyan
		pushd ".\src\$p"
		bower install 
		popd
	}

	dotnet publish ".\src\$p" -c Release --no-restore -v q

	
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