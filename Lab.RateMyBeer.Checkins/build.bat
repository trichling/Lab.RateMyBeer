cd %~dp0

docker build -f Dockerfile -t ratemybeercontainers7428.azurecr.io/checkinsapi:dev .\..
docker push ratemybeercontainers7428.azurecr.io/checkinsapi:dev

REM kubectl rollout restart deployment/lab-ratemybeer-checkins-api