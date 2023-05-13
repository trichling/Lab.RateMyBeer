$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker buildx build -f Dockerfile -t thinkexception.azurecr.io/checkins:dev --build-context framework=../../../Framework/src --build-context frontend=../../../Frontend/src ./..
docker push thinkexception.azurecr.io/checkins:dev

$prevPwd | Set-Location