$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

./Lab.RateMyBeer.Comments/build.ps1
./Lab.RateMyBeer.Comments.Api/build.ps1

$prevPwd | Set-Location