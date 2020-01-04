# DotNetTool.Service
```
!!! Prerelease !!!
!!! API, arguments etc, can be changed in next versions !!!
!!! Hint still in development !!!
!!! Not all commands and not all arguments are implemented right now !!!
```
This library provides you the 'dotnet tool' command as a service. The commands are wrapped so you can handle very comfortable the commands of the 'dotnet tool' cli as a service.

## Prerequisites
* .NET Standard 2.0 compatible projects

## Install package

```bash
dotnet add package DotNetTool.Service --version 0.1.1-beta
```

![](./assets/pack-manager.png)


## Usage

### Build a instance of the dotnet tool
```csharp
var dotNetTool = DotNetToolFactory.Create();
```

### Install a dot net tool globally
```csharp
var installResult = await dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall);
```

### Install a dot net tool with tool path
```csharp
var installResult = await dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall , ToolPath);
```

### Uninstall a dot net tool globally
```csharp
var uninstallResult = await dotNetTool.UninstallAsync(DotNetToolToInstall);
```

### Uninstall a dot net tool with tool path
```csharp
var uninstallResult = await dotNetTool.UninstallAsync(DotNetToolToInstall, ToolPath);
```

### Update a dot net tool globally
[Hint update does not work with prerelease versions !!](https://docs.microsoft.com/de-de/dotnet/core/tools/dotnet-tool-update)
```csharp
var updateResult = await dotNetTool.UpdateAsync(DotNetToolToInstall);
```

### Update a dot net tool with tool path
[Hint update does not work with prerelease versions !!](https://docs.microsoft.com/de-de/dotnet/core/tools/dotnet-tool-update)
```csharp
var updateResult = await dotNetTool.UpdateAsync(DotNetToolToInstall, ToolPath);
```

### List all tools which are globally installed
```csharp
var listResult = await dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall);
```

### List all tools which are installed at a specific tool path
```csharp
var listResult = await dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall , ToolPath);
```

