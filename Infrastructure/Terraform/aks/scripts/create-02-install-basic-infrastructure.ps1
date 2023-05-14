param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version
)

$Application = "ratemybeer"
$REPLICA_COUNT = 1
$RESSOURCE_GROUP = "$Environment-$Application"
$CLUSTER_NAME = "$Environment-$Application-$Version"
$INFRASTRUCTURE_RESOURCE_GROUP = "MC_" + $RESSOURCE_GROUP + "_" + $CLUSTER_NAME + "_westeurope"

az aks get-credentials -g $RESSOURCE_GROUP -n $CLUSTER_NAME --overwrite-existing

kubectl apply -f ./Infrastructure/Kubernetes/Infrastructure/StorageClass/storageclass-managed-standard.yaml
kubectl apply -f ./Infrastructure/Kubernetes/Infrastructure/CertManager/letsencrypt-prod-http-issuer.yaml
kubectl apply -f ./Infrastructure/Kubernetes/Infrastructure/CertManager/letsencrypt-staging-http-issuer.yaml
kubectl apply -f ./Infrastructure/Kubernetes/Infrastructure/Ingress/configmap-ingress-nginx-headers.yaml
kubectl apply -f ./Infrastructure/Kubernetes/Infrastructure/Ingress/ingress-class.yaml
