# 🧬 Idyfa 

## User Identity Management Library for .NET

---
A library built on top of `ASP.NET Identity` to simplifies and customize it's functionalities, and add essential features for common needs like role-based authorization and permission-based Access Control to it. While ASP.NET Identity simplify authentication and basic authorization with built-in APIs, in most applications we need to implement extra features and customizatio in order to achive felxible `Auhtentication`, `Authorization`, and `Access Control` in the User Management section. <b>`Idyfa` is a Role-based Accesss & Permission Control and User Management library</b> that allows you to simply Control Security of your Application and it's Users efficiently without pain. It can be used with multiple databases to store User & Security data, and has easy to use APIs for common Authentication, Authorization, Access Control and User Manahement.
 
### Features
- Built on top of `ASP.NET Identity` and implements all it's features (Implementation for all Identity interfaces)
- Support for multiple Databases out of box. You can simply switch databases with installing a nuget package and modifying a value in your `appsetting.json` file, without needing for changes in your codes.
- Add extra features for Iraninan (Perisan) speaking developers and supports localizations and features a persian application would need. (ex: storing `NationalCode` in Users data and Implemented its logic)
- Provides easy to use, ready services for doing common tasks like `Authentication`, `Authorization`, `UserManagement`, `RoleManagement` and so on.

### 🧩 How to use
You can clone the repo and built the libraries yourself.
```
git clone https://github.com/imaun/Idyfa.git
cd ./Idyfa
dotnet build
```
or Install it via nuget 
```
dotnet add package Idyfa.Core
```
If your database is SqlServer you must Install `Idyfa.EntityFrameworkCore.SqlServer`. The SQLite library is also included and other databases will soon added.

### ⚡︎ Usage
You can browse [Sample project](https://github.com/imaun/Idyfa/tree/master/samples/Idyfa.Samples.BasicWeb) that uses `Idyfa` to bootstrap an ASP.NET Core applications with Sigin and Signup features. Please see [appsetting](https://github.com/imaun/Idyfa/blob/master/samples/Idyfa.Samples.BasicWeb/appsettings.json) file (under `Idyfa` section) in the sample project for a sample Idyfa configuration you need to set in your project in order to use Idyfa and its services. After setting `DbConfig`, `UserOptions` and other configs, You need to add Idyfa Services to your DI container on Startup.

```CS
var options = config.Get<IdyfaConfigRoot>().Idyfa;
var sqliteCfg = options.IdyfaDbConfig.Databases.FirstOrDefault(_ =>
    _.Name.Equals("SQLite", StringComparison.InvariantCultureIgnoreCase));
builder.Services.AddIdyfaSQLiteDatabase(sqliteCfg);
builder.Services.AddIdyfaEntityFrameworkCore();
builder.Services.AddIdyfaCore(options);
```
Above example is for SQLite, for SqlServer you need to Add SqlServer package and its services.

### Roadmap
This is the version `0.0.1` and just the beginning. This library will continue to developed and evolve. But it does not mean that it's not ready to use in real projects. I myself use it to develop User Management of 2 real and live ASP.NET Core projects. You can use it too, but as the [LICENSE] says "AS IS", WITHOUT WARRANTY OF ANY KIND. But If you need help or have any questions please feel free to [open an issue](https://github.com/imaun/Idyfa/issues/new/choose).
