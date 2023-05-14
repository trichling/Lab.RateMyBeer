param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version
)

$Application = "ratemybeer"
$RESSOURCE_GROUP = "$Environment-$Application"
$CLUSTER_NAME = "$Environment-$Application-$Version"
$INFRASTRUCTURE_RESOURCE_GROUP = "MC_" + $RESSOURCE_GROUP + "_" + $CLUSTER_NAME + "_westeurope"

az aks get-credentials -g $RESSOURCE_GROUP -n $CLUSTER_NAME --overwrite-existing

# Im Azure-Portal in der MC_ Ressource Gruppe nach der öffentlichen IP des Ingress-Controller suchen
# unter Konfiguration einen DNS-Namen vergeben <meinname>.westeurope.cloudapp.azure.com
$INGRESS_IP_NAME=$(az network public-ip list -g  $INFRASTRUCTURE_RESOURCE_GROUP  --query "[?tags.\""k8s-azure-service\""=='ingress-nginx/ingress-nginx-controller'].name | [0]")
az network public-ip update -g $INFRASTRUCTURE_RESOURCE_GROUP -n $INGRESS_IP_NAME --dns-name $DNS_LABEL
