# ChallengeTecnicoMeli-MatiasTomaino

# Technologies 
1. .Net 6
2. Redis
3. Docker
4. Docker-compose
5. Unitest

# Requirements
1. Docker (https://docs.docker.com/engine/install/)
2. Postman (https://www.postman.com/)

# APIs available
- TraceIp (Ex: https://localhost:5000/Ip/traceIp?ip=128.201.132.0)
    - Get country information from Ip.
- GetFurthestDistance (Ex: https://localhost:5000/Ip/GetFurthestDistance)
    - Get the furthest distance from all request
- GetClosestDistance (Ex: https://localhost:5000/Ip/GetClosestDistance)
    - Get the closest distance from all request
- GetAverageDistance (Ex: https://localhost:5000/Ip/GetAverageDistance)
    - Get average distance from all requests.

# Turn on ther server 

First, Download the repository from github

### For Linux / Mac:

-  Move to path => Backend/TraceIp
-  Run command => ./trace_ip_linux.sh 

Note: if command doesnt work, run chmod +x trace_ip_linux.sh

### For Windows:

- Move to path => Backend/TraceIp
- Run command => ./trace_ip_windows.ps1

# How Test Api?
1. Postman
2. Curl
3. Index.html

# Documentation
View Documentation.pdf file in repository.
