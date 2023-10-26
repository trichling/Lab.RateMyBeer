param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version
)

$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

$Application = "ratemybeer"
$RESSOURCE_GROUP = "$Environment-$Application"
$CLUSTER_NAME = "$Environment-$Application-$Version"
$INFRASTRUCTURE_RESOURCE_GROUP = "MC_" + $RESSOURCE_GROUP + "_" + $CLUSTER_NAME + "_westeurope"


az aks get-credentials -g $RESSOURCE_GROUP -n $CLUSTER_NAME --overwrite-existing

# Im Azure-Portal in der MC_ Ressource Gruppe nach der öffentlichen IP des Ingress-Controller suchen
# unter Konfiguration einen DNS-Namen vergeben <meinname>.westeurope.cloudapp.azure.com
$OUTBOUND_IP=$(az network public-ip list -g  $INFRASTRUCTURE_RESOURCE_GROUP  --query "[?tags.\""aks-managed-type\""=='aks-slb-managed-outbound-ip'].ipAddress | [0]")
az sql server firewall-rule create -g $RESSOURCE_GROUP -s "$Environment-$Application" -n "Allow$Environment-$Application-$Version" --start-ip-address $OUTBOUND_IP --end-ip-address $OUTBOUND_IP

$prevPwd | Set-Location