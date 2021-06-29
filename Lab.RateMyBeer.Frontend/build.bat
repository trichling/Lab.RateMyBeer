cd %~dp0

docker build -f Dockerfile -t ratemybeercontainers.azurecr.io/frontend:dev .\..
docker push ratemybeercontainers.azurecr.io/frontend:dev

REM kubectl rollout restart deployment/lab-ratemybeer-frontend