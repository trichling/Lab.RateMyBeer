cd %~dp0

docker build -f Dockerfile -t ratemybeercontainer.azurecr.io/frontendapi:dev .\..
docker push ratemybeercontainer.azurecr.io/frontendapi:dev

REM kubectl rollout restart deployment/lab-ratemybeer-frontend-api