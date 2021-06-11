docker build -f Dockerfile -t ratemybeercontainer.azurecr.io/frontend:dev .\..
docker push ratemybeercontainer.azurecr.io/frontend:dev

REM kubectl rollout restart deployment/lab-ratemybeer-frontend