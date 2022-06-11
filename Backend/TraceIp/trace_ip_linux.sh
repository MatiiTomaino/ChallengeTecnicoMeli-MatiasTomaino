docker-compose down
docker rmi -f traceipbackend:latest
docker images -aq -f dangling=true | xargs docker rmi -f
docker-compose up -d