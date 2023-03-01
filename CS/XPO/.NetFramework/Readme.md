# How to connect different data models to several databases within a single application with XPO on .NET Framework

This example demonstrates how to create custom XAF modules with custom business objects and logic that would work with separate databases. These modules do not depend on each other and thus can be reused in other applications as a whole.  
Usually, the connection to the database is set up in the executable [application project](http://documentation.devexpress.com/#Xaf/CustomDocument2569). Typically, it is performed in the configuration file or directly in the code of the application's designer or within the Main function/Global application class. To learn more, please check out this help topic: [Connect an XAF Application to a Database Provider](http://documentation.devexpress.com/#Xaf/CustomDocument3155) In this example, you will learn how to establish a connection to the database directly from your module projects.

## Steps to implement

**1.** Add two new custom XAF modules into a new XAF solution using the XAF Solution Wizard [as described here](https://docs.devexpress.com/eXpressAppFramework/118046/application-shell-and-base-infrastructure/application-solution-components/modules).

**2.** Add required persistent classes into these modules as shown in the _ClassLibraryN/PersistentClassN.xx_ files.

**3.** Add service _ModuleInfo_ classes into these modules as shown in the _ClassLibraryN/ModuleInfoN.xx_ files (only if you want to use [ModuleInfo](https://docs.devexpress.com/eXpressAppFramework/112795/deployment/update-application-and-database-versions-using-the-module-info-table/) to control migrations).

**4.** In _YourModuleName/Module.xx_ files, override the _Setup(XafApplication application)_ methods of the ModuleBase descendants to handle the _CreateCustomObjectSpaceProvider_ event of the XafApplication class as shown in the _ClassLibraryX/XafModuleN.xx_ files.

**5.** Build the solution, invoke the [_Module Designer_](https://docs.devexpress.com/eXpressAppFramework/112828/installation-upgrade-version-history/visual-studio-integration/module-designer) for the platform-agnostic module (_YourSolutionName.Module/Module.xx_), and drag the created custom modules from the Toolbox:

![](https://raw.githubusercontent.com/DevExpress-Examples/how-to-connect-different-xpo-data-models-to-several-databases-within-a-single-application-e4896/13.2.9+/media/95572a4e-4ac0-4852-bdd4-de411b72df28.png)

Alternatively, you can add the same modules via the Application Designer invoked for the executable projects (as demonstrated in this example).

**6.** Declare connection strings in the configuration files of your application as shown in the _TwoXpoModelsForDifferentDatabases.Web\Web.config_ and _TwoXpoModelsForDifferentDatabases.Win\App.config_ files (see _ConnectionStringDatabaseX_ under the element). These connection strings are used in the modules via the ConfigurationManager.ConnectionStrings API, but you can always modify the way your modules obtains this data.

**7.** If the application uses the Security System, security classes should be stored in either database. In this example, we create the *CommonModule* XAF module that uses a separate database and registers security classes stored in it. This module should also register security adapters as follows:

```cs
public override void Setup(XafApplication application) {
    base.Setup(application);
    application.CreateCustomObjectSpaceProvider += application_CreateCustomObjectSpaceProvider;
    (application.Security as SecurityStrategy)?.RegisterXPOAdapterProviders(new SecurityPermissionsProviderDefault(application));
}
```

## Important notes

**1.** Each module has a single static `XpoTypeInfoSource` instance, which is initialized only once during the application life cycle.

**2.** Each ModuleUpdater class checks whether it is valid to create initial data of a certain type from this module via the `IObjectSpace.CanInstantiate` method.

**3.** Business classes linked to different `ObjectSpaceProviders` are considered to be isolated from each other and thus cannot have direct links between them, e.g., an association between two classes. Consider using the [How to prevent altering the legacy database schema when creating an XAF application](https://github.com/DevExpress-Examples/XAF_how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application-e1150) or alternative solutions if you need interlinks between classes from different data stores.

**4.** If several `XPObjectSpaceProvider` objects are connected to the same database, then it would be necessary to additionally map the service `XPObjectType` class to different tables in each `XPDictionary`. You can do this in the `application_CreateCustomObjectSpaceProvider` method by adding `DevExpress.Xpo.PersistentAttribute` with a modified mapping:

```cs
        XPClassInfo ci = typeInfoSource1.XPDictionary.GetClassInfo(typeof(XPObjectType));
        if (ci != null) {
            ci.RemoveAttribute(typeof(PersistentAttribute));
            ci.AddAttribute(new PersistentAttribute("Service_XPObjectType1"));
        }
```

**5.** [Domain Components](https://docs.devexpress.com/eXpressAppFramework/113663/concepts/business-model-design/business-model-design-with-xpo/domain-components/domain-components-overview?v=18.2) (DC) cannot be used with this approach, because the XpoTypeInfoSource constructor expects a list of entity types as a parameter, which is not known at this moment. You cannot call the RegisterEntity/GenerateEntities methods of the XafTypesInfo class to obtain this list either, because in this case the types will be registered for all XpoTypeInfoSource objects within the application.

**6.** To manipulate objects, create an object space using the `XafApplication.CreateObjectSpace(Type)` method passing the `Type` argument. The database to which the created object space is connected depends on the passed type.

## Files to Review

* [PersistentClass1.cs](./ClassLibrary1/PersistentClass1.cs) (VB: [PersistentClass1.vb](../../../VB/ClassLibrary1/PersistentClass1.vb))
* [XafModule1.cs](./ClassLibrary1/XafModule1.cs) (VB: [XafModule1.vb](../../../VB/ClassLibrary1/XafModule1.vb))
* [PersistentClass2.cs](./ClassLibrary2/PersistentClass2.cs) (VB: [PersistentClass2.vb](../../../VB/ClassLibrary2/PersistentClass2.vb))
* [XafModule2.cs](./ClassLibrary2/XafModule2.cs) (VB: [XafModule2.vb](../../../VB/ClassLibrary2/XafModule2.vb))
* **[Module.cs](./CommonModule/CommonModule.cs) (VB: [CommonModule.vb](../../../VB/CommonModule/CommonModule.vb))**

## More Examples

- [How to prevent altering the legacy database schema when creating an XAF application](https://github.com/DevExpress-Examples/XAF_how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application-e1150)
- [Creating and consuming multiple XPObjectSpaceProviders in Web API Service](https://supportcenter.devexpress.com/internal/ticket/details/T1122851)