param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version,
    [Parameter(Mandatory)] [string] $ClusterSubnetAddressSpace
)

$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

terraform init -backend-config="$Environment/backend.tfvars" `
               -backend-config="key=$Environment.ratemybeer.cluster.$Version.tfstate" `
               -reconfigure

# 
# To use this follow the instructions here: https://github.com/hieven/terraform-visual
#
terraform  destroy `
                 -var-file="$Environment/env.tfvars" `
                 -var="cluster_subnet_address_space=$ClusterSubnetAddressSpace" `
                 -var="version_number=$Version"


$prevPwd | Set-Location