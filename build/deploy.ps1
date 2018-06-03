Param
(
 [string]$dest
)

$proj = (
	"Callisto.Web.App",
	"Callisto.Web.Api", 
	"Callisto.Web.Landing",
	"Callisto.Worker.Service"
)


Copy-Item .\docker-compose.yml -Destination $dest -Recurse -Force
Copy-Item .\docker-compose.azure.yml -Destination $dest -Recurse -Force

Copy-Item -Path .\docker -Destination $dest -Recurse -Force

foreach( $p in $proj)
{
	New-Item -ItemType Directory "$dest\src\$p\" -Force -ErrorAction Continue
	Copy-Item -Path .\src\$p\dockerfile -Destination "$dest\src\$p\" -Force -Container
}

