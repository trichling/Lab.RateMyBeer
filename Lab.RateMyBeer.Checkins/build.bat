REM az acr login --name ratemybeercontainer
docker build -f Dockerfile -t ratemybeercontainer.azurecr.io/checkinsapi:dev .\..
docker push ratemybeercontainer.azurecr.io/checkinsapi:dev