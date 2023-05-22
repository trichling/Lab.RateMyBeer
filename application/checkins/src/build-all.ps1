$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

./Lab.RateMyBeer.Checkins/build.ps1
./Lab.RateMyBeer.Checkins.Api/build.ps1

$prevPwd | Set-Location