$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker buildx build -f Dockerfile -t thinkexception.azurecr.io/comments:dev --build-context framework=../../../Framework/src --build-context frontend=../../../Frontend/src .\..
docker push thinkexception.azurecr.io/comments:dev

$prevPwd | Set-Location