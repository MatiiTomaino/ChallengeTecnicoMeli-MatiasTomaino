docker-compose down
docker rmi -f traceip:latest
docker rmi -f @(docker images -aq -f dangling=true)
docker-compose up -d