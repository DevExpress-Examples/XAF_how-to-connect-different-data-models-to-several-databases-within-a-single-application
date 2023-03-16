<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128588378/22.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4896)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to connect different data models to several databases within a single application with Entity Framework Core

This example demonstrates how to create custom XAF modules with custom business objects and logic that would work with separate databases. These modules do not depend on each other and thus can be reused in other applications as a whole.  

The [EFCore](./EFCore) example solution illustrates the following documentation article: [How to: Use Multiple Data Models Connected to Different Databases in Entity Framework Core](https://docs.devexpress.com/eXpressAppFramework/404322).

> **Note** 
> For information on how to achieve the same functionality with XPO on .NET 6+ and .NET Framework, see the **Readme.md** files in the respective solution folders:
> - [.NET 6+](./CS/XPO/.NET)
> - [.NET Framework](./CS/XPO/.NetFramework)

## Files to Review

### Modules:

* [ClassLibrary1DbContext](./CS/EFCore/ClassLibrary1/ClassLibrary1DbContext.cs)
* [PersistentClass1.cs](./CS/EFCore/ClassLibrary1/PersistentClass1.cs)
* [XafModule1.cs](./CS/EFCore/ClassLibrary1/XafModule1.cs)
* [ClassLibrary2DbContext](./CS/EFCore/ClassLibrary2/ClassLibrary2DbContext.cs)
* [PersistentClass2.cs](./CS/EFCore/ClassLibrary2/PersistentClass2.cs)
* [XafModule2.cs](./CS/EFCore/ClassLibrary2/XafModule2.cs)
* **[Module.cs](./CS/EFCore/CommonModule/Module.cs)**

### Blazor application:

* [Startup.cs](./CS/EFCore/TwoModelsForDifferentDatabases.Blazor.Server/Startup.cs)
* [appsettings.json](./CS/EFCore/TwoModelsForDifferentDatabases.Blazor.Server/appsettings.json)

### WinForms application:

* [Startup.cs](./CS/EFCore/TwoModelsForDifferentDatabases.Win/Startup.cs)
* [App.config](./CS/EFCore/TwoModelsForDifferentDatabases.Win/App.config)

## Documentation

* [Business Model Design with Entity Framework Core](https://docs.devexpress.com/eXpressAppFramework/401886/business-model-design-orm/business-model-design-with-entity-framework-core)

