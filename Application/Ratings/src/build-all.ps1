$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

./Lab.RateMyBeer.Ratings/build.ps1
./Lab.RateMyBeer.Ratings.Api/build.ps1

$prevPwd | Set-Location