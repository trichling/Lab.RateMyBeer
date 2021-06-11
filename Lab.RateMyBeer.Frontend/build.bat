az acr login --name ratemybeercontainer
docker build -f Dockerfile -t ratemybeercontainer.azurecr.io/frontend:dev .\..
docker push ratemybeercontainer.azurecr.io/frontend:dev

kubectl rollout restart deployment/lab-ratemybeer-frontend