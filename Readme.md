# docker compose

- Execute the createCert.bat in .certificate
- Import the ratemybeer.pfx to Tursted Root CA store
- Restart the Browser

docker compose up --build

|System         | Url                               | Username / Pwd    |
|---------------|-----------------------------------|-------------------|
|UI             | http://localhost:4000             |                   |
|Application API| https://localhost:6001/swagger    |                   |
|Checkins API   | https://localhost:6003/swagger    |                   |
|RabbitMq       | http://localhost:15672/#/         | guest / guest     |
|SqlServer      | localhost,1533                    | sa/1stChangeIt!   |

# Kubernetes

## Prerequisits

- [Azure Subscription](https://my.visualstudio.com/)
  * Make sure to select the correct Abonement (Visual Studio Professional)
  * Make sure to activate the one with the monthly credit (not "Pay as you go").

- [Chocolaty](https://chocolatey.org/install)
  > Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

- [Windows Terminal](https://docs.microsoft.com/en-us/windows/terminal/get-started)
  * It is optional but useful to handle different shells in one terminal

- [WSL2](https://docs.microsoft.com/en-us/windows/wsl/install-win10)
  * Check Requirements
    * systeminfo | findstr /B /C:"Betriebssystemname" /C:"Betriebssystemversion"
    * Version 1903 or higher is required     
  * Enable system features
    * dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart
    * dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart
  * Update WSL
    * https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi
    * wsl --set-default-version 2
  * Download Distro
    * https://aka.ms/wslstore

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
  * Make sure that it starts correctly  

- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-powershell)
  > Invoke-WebRequest -Uri https://aka.ms/installazurecliwindows -OutFile .\AzureCLI.msi; Start-Process msiexec.exe -Wait -ArgumentList '/I AzureCLI.msi /quiet'; rm .\AzureCLI.msi
  
  > az version
    &rarr; 2.24.2

  > az login
  
  > az account set --subscripiton "Visual Studio Professional"

- [Kubernetes CLI](https://kubernetes.io/docs/tasks/tools/install-kubectl-windows/)
  > choco install kubernetes-cli

  > kubectl version --client 
    &rarr; v1.21.1

- [Kubernetes Helm](https://helm.sh/docs/intro/install/)
  > choco install kubernetes-helm

  > helm version
    &rarr; v3.6.0

- [Kubernetes Tools for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-kubernetes-tools.vscode-kubernetes-tools)

- [Azure CLI Tools for VS Code]

- [Lab.RateMyBeer Demoanwendung](https://github.com/trichling/Lab.RateMyBeer)
  > git clone https://github.com/trichling/Lab.RateMyBeer.git
  
## High level overview

<style>  
.rotate90 {  
  -webkit-transform: rotate(90deg) ;  
  -moz-transform: rotate(90deg) translate(100px;0px);  
  -ms-transform: rotate(90deg) translate(100px;0px);  
  -o-transform: rotate(90deg) translate(100px;0px);  
  transform: rotate(90deg) translate(100px;0px);  
}  

</style>  
<img src="./Kubernetes Architecture High Level.svg" class="rotate90 translate100">

## Basic API Objects
<img src="./Kubernetes Basic Api Objects.svg" class="rotate90">

