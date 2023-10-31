
terraform init -backend-config="backend.tfvars" `
               -reconfigure

# 
# To use this follow the instructions here: https://github.com/hieven/terraform-visual
#
terraform  destroy `
                 -var-file="env.tfvars" `
                 
