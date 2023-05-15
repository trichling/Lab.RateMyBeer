$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

kubectl kustomize ./overlays/feature
kubectl apply -k ./overlays/feature

kubectl rollout restart deployment/ratemybeer-frontend

$prevPwd | Set-Location