$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

# Checkins - API
./Checkins/kubernetes/api/deploy-feature.ps1

# Checkins - NServiceBus Endpoint
./Checkins/kubernetes/nsb/deploy-feature.ps1

# Comments - API
./Comments/kubernetes/api/deploy-feature.ps1

# Comments - NServiceBus Endpoint
./Comments/kubernetes/nsb/deploy-feature.ps1

# Ratings - API
./Ratings/kubernetes/api/deploy-feature.ps1

# Ratings - NServiceBus Endpoint
./Ratings/kubernetes/nsb/deploy-feature.ps1

# Frontend - API
./Frontend/kubernetes/api/deploy-feature.ps1

# Frontend - Client
./Frontend/kubernetes/client/deploy-feature.ps1

$prevPwd | Set-Location