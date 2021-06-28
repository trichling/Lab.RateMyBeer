cd %~dp0

docker build -f Dockerfile -t ratemybeercontainer.azurecr.io/checkinsapi:dev .\..
docker push ratemybeercontainer.azurecr.io/checkinsapi:dev

REM kubectl rollout restart deployment/lab-ratemybeer-checkins-api