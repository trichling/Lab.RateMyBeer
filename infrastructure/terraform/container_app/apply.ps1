terraform init -reconfigure -upgrade
terraform apply -var-file="dev/env.tfvars" -var-file="dev/secrets.tfvars"