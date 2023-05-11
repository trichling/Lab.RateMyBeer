param (
    [Parameter(Mandatory)] [string] $Environment
)

terraform init -backend-config="$Environment/backend.tfvars" `
               -reconfigure

# 
# To use this follow the instructions here: https://github.com/hieven/terraform-visual
#
terraform  destroy `
                 -var-file="$Environment/env.tfvars" `
                 -var-file="$Environment/secrets.tfvars" 
                 
