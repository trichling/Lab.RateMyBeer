terraform init
terraform destroy -var-file="dev/env.tfvars" -var-file="dev/secrets.tfvars"