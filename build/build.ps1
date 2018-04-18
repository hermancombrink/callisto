Param
(
 [switch]$ignoreNpmRestore,
 [switch]$dockerCompose
)

$proj = (
	"Callisto.Web.Landing"
)

function getNpmInstallDir($project, $packageDir, $framework = "netcoreapp2.0"){
	return ".\src\$project\bin\Release\$framework\publish\$packageDir";
}

Write-Host "dotnet clean..."
dotnet clean -v q

Write-Host "dotnet restore..."
dotnet restore -nowarn:msb3202,nu1503 -v q

foreach( $p in $proj)
{
	Write-Host "dotnet publish..."
	dotnet publish ".\src\$p" -c Release --no-restore -v q

	if($ignoreNpmRestore -eq $false)
	{
		Write-Host "npm install..."
		$t = getNpmInstallDir $p -packageDir "wwwroot" 
		npm --prefix $t install $t
	}
	else
	{
		Write-Host "Skipping npm install..."
	}
}

if($dockerCompose)
{
	Write-Host "docker compose..."
	docker-compose build
}
else
	{
		Write-Host "Skipping docker compose..."
	}