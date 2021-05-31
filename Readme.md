- Execute the createCert.bat in .certificate
- Import the ratemybeer.pfx to Tursted Root CA store
- Restart the Browser

docker compose up --build

UI: http://localhost:4000
Application API: https://localhost:6001/swagger
Checkins API: https://localhost:6003/swagger

RabbitMq: http://localhost:15672/#/ (guest/guest)
SqlServer: localhost,1533 (sa/1stChangeIt!)
