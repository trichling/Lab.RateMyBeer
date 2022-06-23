$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

docker build -f Dockerfile -t thinkexception.azurecr.io/checkins:dev .\..
docker push thinkexception.azurecr.io/checkins:dev

$prevPwd | Set-Location