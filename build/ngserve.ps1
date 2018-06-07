pushd .\src\Callisto.Web.App\ClientApp
#ng serve --extract-css --serve-path /app --watch --progress
ng serve --serve-path /app  --extract-css   --build-optimizer=false  --sourcemaps=false  --live-reload=false  --watch --progress
popd