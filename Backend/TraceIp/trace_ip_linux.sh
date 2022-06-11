docker-compose down
docker rmi -f traceip-backend:latest
docker images -aq -f dangling=true | xargs docker rmi -f
docker-compose up -d