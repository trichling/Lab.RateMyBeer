param (
)

$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

terraform init -backend-config="backend.tfvars" `
               -reconfigure

# 
# To use this follow the instructions here: https://github.com/hieven/terraform-visual
#
terraform  plan -out="plan.out" `
                 -var-file="env.tfvars" 

terraform show -json plan.out > plan.json   # Read plan file and output it in JSON format
terraform-visual --plan plan.json
./terraform-visual-report/index.html

$decision = $Host.UI.PromptForChoice("Confirmation required", "Are you sure you want to proceed with the above changes?", ("&Yes", "&No"), 1)

if ($decision -eq 0) {
    terraform apply plan.out
}

$prevPwd | Set-Location