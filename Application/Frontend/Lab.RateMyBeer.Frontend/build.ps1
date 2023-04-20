$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

((Get-Content entrypoint.sh) -join "`n") + "`n" | Set-Content -NoNewline entrypoint.sh

docker build -f Dockerfile -t thinkexception.azurecr.io/frontend:dev .\..
docker push thinkexception.azurecr.io/frontend:dev

$prevPwd | Set-Location