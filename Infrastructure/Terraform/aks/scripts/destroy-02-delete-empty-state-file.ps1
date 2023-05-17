param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version
)

$TerraformStateStorageAccountName = "thinkexception"
$TerraformStateBlobContainerName = "terraformstate"
$TerraformStateKey = "${Environment}.ratemybeer.cluster.${Version}.tfstate"

az storage blob delete --account-name $TerraformStateStorageAccountName --container-name $TerraformStateBlobContainerName --name $TerraformStateKey