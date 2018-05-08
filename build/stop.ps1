docker-compose rm -s -v -f
docker volume rm $(docker volume ls -qf dangling=true)