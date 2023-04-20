$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker build -f Dockerfile -t thinkexception.azurecr.io/ratings:dev .\..
docker push thinkexception.azurecr.io/ratings:dev

$prevPwd | Set-Location