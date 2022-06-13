console.log('hello world')

function callApi() {
    const ip = document.getElementById('ip').value;
    console.log('Click on button with ip:', ip)

    var url = "http://192.168.0.2:5000/Ip/traceip?ip="+ip; 
    console.log('url');
    fetch(url)
    .then(function(response) {
          if(response.ok) {
              response.json().then(function(data) { 
                    var str = JSON.stringify(data);
                    console.log(data);
                    console.log(data.name);
                    console.log(str);
                    // display pretty printed object in text area:
                    document.getElementById('countryNameId').innerHTML = data.name;
                    document.getElementById('isoCodeId').innerHTML = data.isoCode;
                    
                    var language = '';
                    var list = data.languages;
                    list.forEach(element => {
                        language += element.name+", ";
                    });
                    document.getElementById('languagesId').innerHTML = language;
                    document.getElementById('currencyId').innerHTML = data.currencyAndQuotation;
                    document.getElementById('hourId').innerHTML = data.actualTime;
                    document.getElementById('distanceBuenosAiresId').innerHTML = data.estimatedDistance + ' KM';
              });
          } else{ // Response wasn't ok. Check dev tools
              console.log("response failed?");
              document.getElementById('countryNameId').innerHTML = '';
              document.getElementById('isoCodeId').innerHTML = '';
              document.getElementById('languagesId').innerHTML = '';
              document.getElementById('currencyId').innerHTML = '';
              document.getElementById('hourId').innerHTML = '';
              document.getElementById('distanceBuenosAiresId').innerHTML = '';
              document.getElementById('serverResponseId').innerHTML = 'Fail request.';           
          }
    });
} 

function updateStats() {
    var url = "http://192.168.0.2:5000/Ip/GetAverageDistance"; 
    console.log('url');
    fetch(url)
    .then(function(response) {
          if(response.ok) {
              response.json().then(function(data) { 
                    var str = JSON.stringify(data);
                    console.log(data);
                    console.log(str);
                    // display pretty printed object in text area:
                    document.getElementById('averageDistanceId').innerHTML = data + " KM";
                  });
          } else{ // Response wasn't ok. Check dev tools
              console.log("response failed?");          
          }
    });

    var url = "http://192.168.0.2:5000/Ip/GetFurthestDistance"; 
    console.log('url');
    fetch(url)
    .then(function(response) {
          if(response.ok) {
              response.json().then(function(data) { 
                    var str = JSON.stringify(data);
                    console.log(data);
                    console.log(str);
                    // display pretty printed object in text area:
                    document.getElementById('furthestDistanceId').innerHTML = data.value + " KM " + "(" + data.countryName + ")";
                  });
          } else{ // Response wasn't ok. Check dev tools
              console.log("response failed?");          
          }
    });

    var url = "http://192.168.0.2:5000/Ip/GetClosestDistance"; 
    console.log('url');
    fetch(url)
    .then(function(response) {
          if(response.ok) {
              response.json().then(function(data) { 
                    var str = JSON.stringify(data);
                    console.log(data);
                    console.log(str);
                    // display pretty printed object in text area:
                    document.getElementById('closestDistanceId').innerHTML = data.value + " KM " + "(" + data.countryName + ")";
                  });
          } else{ // Response wasn't ok. Check dev tools
              console.log("response failed?");          
          }
    });
} 