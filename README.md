# LGDShop
An ASP.NET CORE 2.2 webapi + identity server 4 project

## Development Environment
- Visual Studio 2019 Community
- .NET Core SDK 2.2.104 
- IdentityServer4 2.4.0

## Setup

**1.** Start `LGDShop.API` project.
  - **Important:** This must be running on http://localhost:5001 or https://localhost:44344

**2.** Start `StsServerIdentity` project.
  - **Important:** This must be running on http://localhost:5000 or https://localhost:44318

**3.** Start any SPA project.
  - **Important:** This must be running on http://localhost:4200 or https://localhost:4200 (valid certificate)
  
## Branch

Don't touch master branch, use develop instead 

## Missing Secret

Manully added the following misssing files: 
1. sts_dev_cert.pfx in STS project root folder 
2. replace local secret.json by right-click STS project - manage user secrets


