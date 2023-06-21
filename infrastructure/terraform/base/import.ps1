param (
    [Parameter(Mandatory)] [string] $Environment,
    [Parameter(Mandatory)] [string] $Adress,
    [Parameter(Mandatory)] [string] $Id
)

terraform init -backend-config="$Environment/backend.tfvars" `
               -reconfigure

# 
# To use this follow the instructions here: https://github.com/hieven/terraform-visual
#
terraform  import  `
                 -var-file="$Environment/env.tfvars" `
                 -var-file="$Environment/secrets.tfvars" `
                 $Adress $Id




