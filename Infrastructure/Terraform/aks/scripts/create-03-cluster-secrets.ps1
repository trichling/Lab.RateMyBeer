param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version
)

$Application = "ratemybeer"
$RESSOURCE_GROUP = "$Environment-$Application"
$CLUSTER_NAME = "$Environment-$Application-$Version"
$INFRASTRUCTURE_RESOURCE_GROUP = "MC_" + $RESSOURCE_GROUP + "_" + $CLUSTER_NAME + "_westeurope"
$KEY_VAULT_NAME = $(az keyvault list --out tsv --query "[?starts_with(name, '$Environment-$Application')] | [0].name")

az aks get-credentials -g $RESSOURCE_GROUP -n $CLUSTER_NAME --overwrite-existing

$NServiceBusTransportConnectionString = $(az keyvault secret show --vault-name $KEY_VAULT_NAME --name "servicebusconnectionstring-$Version" --out tsv --query "value")
$DatabaseConnectionString = $(az keyvault secret show --vault-name $KEY_VAULT_NAME --name "connectionstringtemplate" --out tsv --query "value")

kubectl create secret generic connectionstrings `
   --from-literal=DatabaseConnectionString=$DatabaseConnectionString `
   --from-literal=NServiceBusTransportConnectionString=$NServiceBusTransportConnectionString `
   --dry-run=client -o yaml | kubectl apply -f -