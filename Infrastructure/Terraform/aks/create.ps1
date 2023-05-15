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
terraform  plan -out="plan.out" `
                 -var-file="$Environment/env.tfvars" `
                 -var="cluster_subnet_address_space=$ClusterSubnetAddressSpace" `
                 -var="version_number=$Version"

terraform show -json plan.out > plan.json   # Read plan file and output it in JSON format
terraform-visual --plan plan.json
./terraform-visual-report/index.html

$decision = $Host.UI.PromptForChoice("Confirmation required", "Are you sure you want to proceed with the above changes?", ("&Yes", "&No"), 1)

if ($decision -eq 0) {
    terraform apply plan.out
}

$prevPwd | Set-Location