param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version,
    [Parameter(Mandatory)] [string] $ClusterSubnetAddressSpace
)

$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

./Infrastructure/Terraform/aks/scripts/destroy-01-remove-cluster-from-sqlserver-firewall.ps1 $Environment $Version

./Infrastructure/Terraform/aks/destroy.ps1 $Environment $Version $ClusterSubnetAddressSpace

$prevPwd | Set-Location