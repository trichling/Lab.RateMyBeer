$thumbprint = $(New-SelfSignedCertificate `
  -certstorelocation cert:\localmachine\my `
  -dnsname dev.thinkex.net
).Thumbprint

$pwd = ConvertTo-SecureString -String Abcd1234! -Force -AsPlainText
Export-PfxCertificate `
-cert cert:\localMachine\my\$thumbprint `
-FilePath appgwcert.pfx `
-Password $pwd