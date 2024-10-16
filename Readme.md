<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128588378/23.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4896)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# How to connect different data models to several databases within a single application with Entity Framework Core

This example demonstrates how to create custom XAF modules with custom business objects and logic that work with separate databases. These modules do not depend on each other and thus can be reused in other applications independently.  

This [EFCore](./CS/EFCore/) example solution is based on the following tutorial: [How to: Use Multiple Data Models Connected to Different Databases in Entity Framework Core](https://docs.devexpress.com/eXpressAppFramework/404322).

Note that business classes linked to different `ObjectSpaceProviders` are considered to be isolated from each other and thus cannot have direct links between them, e.g., an association between two classes. Consider using the [How to prevent altering the legacy database schema when creating an XAF application](https://github.com/DevExpress-Examples/xaf-how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application) or alternative solutions if you need interlinks between classes from different data stores.

> **Note** 
> For information on how to achieve the same functionality with XPO on .NET 6+ and .NET Framework, see the **Readme.md** files in the respective solution folders:
>
> [.NET 6+](https://github.com/DevExpress-Examples/XAF_how-to-connect-different-data-models-to-several-databases-within-a-single-application/tree/23.1.2%2B/CS/XPO/.NET) 
>
> [.NET Framework](https://github.com/DevExpress-Examples/XAF_how-to-connect-different-data-models-to-several-databases-within-a-single-application/tree/23.1.2%2B/CS/XPO/.NetFramework) 

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

<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=XAF_how-to-connect-different-data-models-to-several-databases-within-a-single-application&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=XAF_how-to-connect-different-data-models-to-several-databases-within-a-single-application&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
