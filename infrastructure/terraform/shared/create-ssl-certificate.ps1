$thumbprint = $(New-SelfSignedCertificate `
  -certstorelocation cert:\localmachine\my `
  -dnsname dev.thinkex.net
).Thumbprint

$pwd = ConvertTo-SecureString -String Abcd1234! -Force -AsPlainText
Export-PfxCertificate `
-cert cert:\localMachine\my\$thumbprint `
-FilePath appgwcert.pfx `
-Password $pwd

az keyvault certificate import `
    --vault-name thinkexception `
    --name appgwcert `
    --file appgwcert.pfx `
    --password $pwd