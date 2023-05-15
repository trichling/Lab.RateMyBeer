param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version,
    [Parameter(Mandatory)] [string] $ClusterSubnetAddressSpace
)

./Infrastructure/Terraform/aks/create.ps1 $Environment $Version $ClusterSubnetAddressSpace

./Infrastructure/Terraform/aks/scripts/create-01-install-helm-charts.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-02-install-basic-infrastructure.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-03-set-dns-label-for-publicIp.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-04-cluster-secrets.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-05-allow-cluster-in-sqlserver-firewall.ps1 $Environment $Version

./prepare-feature-branch.ps1 $Version latest
./Application/deploy-all-feature.ps1
./prepare-feature-branch.ps1 $Version "feature$Version"