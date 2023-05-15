$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

./Checkins/src/build-all.ps1
./Comments/src/build-all.ps1
./Ratings/src/build-all.ps1
./Frontend/src/build-all.ps1

$prevPwd | Set-Location