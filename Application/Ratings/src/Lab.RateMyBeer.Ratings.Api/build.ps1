$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker build -f Dockerfile -t thinkexception.azurecr.io/ratingsapi:dev .\..
docker push thinkexception.azurecr.io/ratingsapi:dev

$prevPwd | Set-Location