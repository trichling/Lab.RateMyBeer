param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Version,
    [Parameter(Mandatory)] [string] $ClusterSubnetAddressSpace
)

# Terraform init / plan / apply
./Infrastructure/Terraform/aks/create.ps1 $Environment $Version $ClusterSubnetAddressSpace

# Ingress, Cert Manager, Ip-DNS, Secrets und co.
./Infrastructure/Terraform/aks/scripts/create-01-install-helm-charts.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-02-install-basic-infrastructure.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-03-set-dns-label-for-publicIp.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-04-cluster-secrets.ps1 $Environment $Version
./Infrastructure/Terraform/aks/scripts/create-05-allow-cluster-in-sqlserver-firewall.ps1 $Environment $Version

# Deploy Application
./prepare-feature-branch.ps1 $Version latest
# Deploy all image
./Application/deploy-all-feature.ps1

./prepare-feature-branch.ps1 $Version "feature$Version"