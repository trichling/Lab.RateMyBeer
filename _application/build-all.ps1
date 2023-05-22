$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

./checkins/src/build-all.ps1
./comments/src/build-all.ps1
./ratings/src/build-all.ps1
./frontend/src/build-all.ps1

$prevPwd | Set-Location