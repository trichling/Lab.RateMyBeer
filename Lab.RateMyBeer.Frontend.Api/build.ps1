$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker build -f Dockerfile -t thinkexception.azurecr.io/frontendapi:dev .\..
docker push thinkexception.azurecr.io/frontendapi:dev

$prevPwd | Set-Location