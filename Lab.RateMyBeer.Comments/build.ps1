$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker build -f Dockerfile -t thinkexception.azurecr.io/comments:dev .\..
docker push thinkexception.azurecr.io/comments:dev

$prevPwd | Set-Location