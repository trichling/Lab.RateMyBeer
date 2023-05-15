param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version
)

$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

$Application = "ratemybeer"
$RESSOURCE_GROUP = "$Environment-$Application"
$CLUSTER_NAME = "$Environment-$Application-$Version"

az aks get-credentials -g $RESSOURCE_GROUP -n $CLUSTER_NAME --overwrite-existing

# Im Azure-Portal in der MC_ Ressource Gruppe nach der öffentlichen IP des Ingress-Controller suchen
# unter Konfiguration einen DNS-Namen vergeben <meinname>.westeurope.cloudapp.azure.com
az sql server firewall-rule delete -g $RESSOURCE_GROUP -s "$Environment-$Application" -n "Allow$Environment-$Application-$Version"

$prevPwd | Set-Location