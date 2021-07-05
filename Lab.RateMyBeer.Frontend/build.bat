cd %~dp0

docker build -f Dockerfile -t ratemybeercontainers7428.azurecr.io/frontend:dev .\..
docker push ratemybeercontainers7428.azurecr.io/frontend:dev

REM kubectl rollout restart deployment/lab-ratemybeer-frontend