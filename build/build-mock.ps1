Copy-Item -Path .\src\Callisto.Web.Client\App\mocks\* -Destination .\docker\mock-api\ -Force
docker build .\docker\mock-api\ -t mock_api

Copy-Item -Path .\src\Callisto.Web.Client\App\dist\* -Destination .\docker\mock-app\ -Force
docker build .\docker\mock-app\ -t mock_app