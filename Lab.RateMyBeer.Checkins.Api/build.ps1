$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker build -f Dockerfile -t thinkexception.azurecr.io/checkinsapi:dev .\..
docker push thinkexception.azurecr.io/checkinsapi:dev

$prevPwd | Set-Location