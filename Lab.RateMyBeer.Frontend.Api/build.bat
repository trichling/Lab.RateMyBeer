az acr login --name ratemybeercontainer
docker build -f Dockerfile -t ratemybeercontainer.azurecr.io/frontendapi:dev .\..
docker push ratemybeercontainer.azurecr.io/frontendapi:dev

kubectl rollout restart deployment/lab-ratemybeer-frontend-api