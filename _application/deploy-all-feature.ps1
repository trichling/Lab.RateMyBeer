$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

# Checkins - API
./checkins/kubernetes/api/deploy-feature.ps1

# Checkins - NServiceBus Endpoint
./checkins/kubernetes/nsb/deploy-feature.ps1

# Comments - API
./comments/kubernetes/api/deploy-feature.ps1

# Comments - NServiceBus Endpoint
./comments/kubernetes/nsb/deploy-feature.ps1

# Ratings - API
./ratings/kubernetes/api/deploy-feature.ps1

# Ratings - NServiceBus Endpoint
./ratings/kubernetes/nsb/deploy-feature.ps1

# Frontend - API
./frontend/kubernetes/api/deploy-feature.ps1

# Frontend - Client
./frontend/kubernetes/client/deploy-feature.ps1

$prevPwd | Set-Location