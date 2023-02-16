$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker build -f Dockerfile -t thinkexception.azurecr.io/commentsapi:dev .\..
docker push thinkexception.azurecr.io/commentsapi:dev

$prevPwd | Set-Location