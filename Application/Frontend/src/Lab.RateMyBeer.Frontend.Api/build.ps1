$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker buildx build -f Dockerfile -t thinkexception.azurecr.io/frontendapi:dev --build-context framework=../../../Framework/src --build-context checkins=../../../Checkins/src --build-context comments=../../../Comments/src --build-context ratings=../../../Ratings/src   .\..
docker push thinkexception.azurecr.io/frontendapi:dev

$prevPwd | Set-Location