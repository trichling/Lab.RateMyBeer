cd %~dp0

docker build -f Dockerfile -t ratemybeercontainers.azurecr.io/checkinsapi:dev .\..
docker push ratemybeercontainers.azurecr.io/checkinsapi:dev

REM kubectl rollout restart deployment/lab-ratemybeer-checkins-api