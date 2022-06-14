# ChallengeTecnicoMeli-MatiasTomaino

# Technologies 
1. .Net 6
2. Redis
3. Docker
4. Docker-compose
5. Unitest

# Requirements
1. Docker (https://docs.docker.com/engine/install/) -V 20.10.16
2. Docker Compose -V 1.29.2
3. Postman (https://www.postman.com/)

# APIs available
- TraceIp (Ex: http://localhost:5000/Ip/traceIp?ip=128.201.132.0)
    - Get country information from Ip.
- GetFurthestDistance (Ex: http://localhost:5000/Ip/GetFurthestDistance)
    - Get the furthest distance from all request
- GetClosestDistance (Ex: http://localhost:5000/Ip/GetClosestDistance)
    - Get the closest distance from all request
- GetAverageDistance (Ex: http://localhost:5000/Ip/GetAverageDistance)
    - Get average distance from all requests.

# Turn on the server 

First, Download the repository from github

### Linux / Mac:

-  Move to path => Backend/TraceIp
-  Run command => ./trace_ip_linux.sh 

Note: if command doesnt work, run chmod +x trace_ip_linux.sh

### Windows:

- Move to path => Backend/TraceIp
- Run command => ./trace_ip_windows.ps1

# How to test the Api?
1. Postman
2. Curl
    
    curl --location --request GET 'http://localhost:5000/Ip/traceip?ip=128.201.132.0'
    
    curl --location --request GET 'http://localhost:5000/Ip/GetFurthestDistance'
    
    curl --location --request GET 'http://localhost:5000/Ip/GetClosestDistance'
    
    curl --location --request GET 'http://localhost:5000/Ip/GetAverageDistance'
    
3. Swagger (http://localhost:5000/swagger/index.html)
4. Html (Frontend/index.html)

For more details view documentation

# Documentation
View Documentation.pdf file in repository.
