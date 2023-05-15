$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

./Lab.RateMyBeer.Frontend/build.ps1
./Lab.RateMyBeer.Frontend.Api/build.ps1

$prevPwd | Set-Location