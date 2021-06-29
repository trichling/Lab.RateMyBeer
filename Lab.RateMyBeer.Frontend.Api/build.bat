cd %~dp0

docker build -f Dockerfile -t ratemybeercontainers.azurecr.io/frontendapi:dev .\..
docker push ratemybeercontainers.azurecr.io/frontendapi:dev

REM kubectl rollout restart deployment/lab-ratemybeer-frontend-api