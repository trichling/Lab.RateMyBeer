param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version
)

$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

$Application = "ratemybeer"
$REPLICA_COUNT = 1
$RESSOURCE_GROUP = "$Environment-$Application"
$CLUSTER_NAME = "$Environment-$Application-$Version"
$INFRASTRUCTURE_RESOURCE_GROUP = "MC_" + $RESSOURCE_GROUP + "_" + $CLUSTER_NAME + "_westeurope"

az aks get-credentials -g $RESSOURCE_GROUP -n $CLUSTER_NAME --overwrite-existing

helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo add jetstack https://charts.jetstack.io

helm repo update

### NGINX ###
# This creates the namespace if it does not exists and wont complain otherwise (https://stackoverflow.com/questions/63135361/how-to-create-kubernetes-namespace-if-it-does-not-exist)
kubectl create namespace ingress-nginx --dry-run=client -o yaml | kubectl apply -f -

helm upgrade ingress-nginx ingress-nginx/ingress-nginx --install `
    --namespace ingress-nginx `
    --set controller.replicaCount=$REPLICA_COUNT `
    --set controller.service.externalTrafficPolicy=Local `
    --set controller.nodeSelector."kubernetes\.io/os"=linux `
    --set defaultBackend.nodeSelector."kubernetes\.io/os"=linux `
    --set controller.admissionWebhooks.patch.nodeSelector."kubernetes\.io/os"=linux `

### Cert Manager ###
# This creates the namespace if it does not exists and wont complain otherwise (https://stackoverflow.com/questions/63135361/how-to-create-kubernetes-namespace-if-it-does-not-exist)
kubectl create namespace cert-manager --dry-run=client -o yaml | kubectl apply -f -

helm upgrade cert-manager jetstack/cert-manager --install `
    --namespace cert-manager `
    --set installCRDs=true

$prevPwd | Set-Location