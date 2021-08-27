<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128588378/13.2.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4896)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [ModuleInfo1.cs](./CS/ClassLibrary1/ModuleInfo1.cs) (VB: [ModuleInfo1.vb](./VB/ClassLibrary1/ModuleInfo1.vb))
* [PersistentClass1.cs](./CS/ClassLibrary1/PersistentClass1.cs) (VB: [PersistentClass1.vb](./VB/ClassLibrary1/PersistentClass1.vb))
* **[XafModule1.cs](./CS/ClassLibrary1/XafModule1.cs) (VB: [XafModule1.vb](./VB/ClassLibrary1/XafModule1.vb))**
* [ModuleInfo2.cs](./CS/ClassLibrary2/ModuleInfo2.cs) (VB: [ModuleInfo2.vb](./VB/ClassLibrary2/ModuleInfo2.vb))
* [PersistentClass2.cs](./CS/ClassLibrary2/PersistentClass2.cs) (VB: [PersistentClass2.vb](./VB/ClassLibrary2/PersistentClass2.vb))
* [XafModule2.cs](./CS/ClassLibrary2/XafModule2.cs) (VB: [XafModule2.vb](./VB/ClassLibrary2/XafModule2.vb))
<!-- default file list end -->
# How to connect different XPO data models to several databases within a single application


<p><strong>Scenario</strong></p>
<p>This example demonstrates how to create custom XAF modules with custom business objects and logic that would work with separate databases. These modules do not depend on each other and thus can be reused in other applications as a whole.<br>Usually, the connection to the database is set up in the executable <a href="http://documentation.devexpress.com/#Xaf/CustomDocument2569">application project</a>. Typically, it is performed in the configuration file or directly in the code of the application's designer or within the Main function/Global application class. To learn more, please check out this help topic: <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3155">Connect an XAF Application to a Database Provider</a> In this example, you will learn how to establish a connection to the database directly from your module projects.</p>
<br>
<p><strong>Steps to implement</strong></p>
<p><strong>1.</strong> Add two new custom XAF modules into a new XAF solution using the XAF Solution Wizard <a href="https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument118046">as  described here</a>;</p>
<p><strong>2.</strong> Add required persistent classes into these modules as shown in the <em>ClassLibraryN/PersistentClassN.xx</em> files of this example solution;</p>
<p><strong>3.</strong> Add service <em>ModuleInfo</em> classes into these modules as shown in the <em>ClassLibraryN/ModuleInfoN.xx</em> files of this example solution;</p>
<p><strong>4.</strong> In <em>YourModuleName/Module.xx</em> files, override the <em>Setup(XafApplication application)</em> methods of the ModuleBase descendants to handle the <em>CreateCustomObjectSpaceProvider</em> event of the XafApplication class as shown in the <em>ClassLibraryX/XafModuleN.xx</em> files of this example solution;</p>
<p><strong>5.</strong> Build the solution, invoke the <a href="http://documentation.devexpress.com/#Xaf/CustomDocument2828"><u>Module Designer</u></a> for the platform-agnostic module (<em>YourSolutionName.Module/Module.xx</em>), and drag the created custom modules from the Toolbox:</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-connect-different-xpo-data-models-to-several-databases-within-a-single-application-e4896/13.2.9+/media/95572a4e-4ac0-4852-bdd4-de411b72df28.png"></p>
<p>Alternatively, you can add the same modules via the Application Designer invoked for the executable projects (as demonstrated in this example).</p>
<p><strong>6. </strong>Declare connection strings in the configuration files of your application as shown in the <em>TwoXpoModelsForDifferentDatabases.Web\Web.config</em> and <em>TwoXpoModelsForDifferentDatabases.Win\App.config</em> files (see ConnectionStringDatabaseX under the <connectionStrings/> element). These connection strings are used in the modules via the ConfigurationManager.ConnectionStrings API, but you can always modify the way your modules obtains this data.<strong><br><br>Important notes</strong></p>
<p><strong>1. </strong>Each module has a single static XPObjectSpaceProvider instance, which is initialized only once during the application life cycle.<br><strong>2. </strong>Each ModuleUpdater class checks whether it is valid to create initial data of a certain type from this module via the IObjectSpace.CanInstantiate method.<br><strong>3.</strong> Business classes linked to different ObjectSpaceProviders are considered to be isolated from each other and thus cannot have direct links between them, e.g., there is an association between two classes. Consider using the <a href="https://www.devexpress.com/Support/Center/p/E1150">How to prevent altering the legacy database schema when creating an XAF application</a> or alternative solutions if you need interlinks between classes from different data stores.</p>
<p><strong>4.</strong> If several <em>XPObjectSpaceProvider</em> objects are connected to the same database, then it would be necessary to additionally map the service <em>XPObjectType</em> class to different tables in each <em>XPDictionary</em>. You can do this in the <em>application_CreateCustomObjectSpaceProvider</em> method by adding <em>DevExpress.Xpo.PersistentAttribute</em> with a modified mapping: </p>


```cs
...
                XPClassInfo ci = typeInfoSource1.XPDictionary.GetClassInfo(typeof(XPObjectType));
                if (ci != null) {
                    ci.RemoveAttribute(typeof(PersistentAttribute));
                    ci.AddAttribute(new PersistentAttribute("Service_XPObjectType1"));
                }
...
```


<p><strong>5.</strong> <a href="https://documentation.devexpress.com/eXpressAppFramework/113663/Concepts/Business-Model-Design/Business-Model-Design-with-XPO/Domain-Components/Domain-Components-Overview">Domain Components</a> (DC) cannot be used with this approach, because the XpoTypeInfoSource constructor expects a list of entity types as a parameter, which is not known at this moment. You cannot call the RegisterEntity/GenerateEntities methods of the XafTypesInfo class to obtain this list either, because in this case the types will be registered for all XpoTypeInfoSource objects within the application.</p>
<p><strong>6.</strong> To manipulate objects, create an object space using the XafApplication.CreateObjectSpace(Type) method passing the Type argument. The database to which the created object space is connected depends on the passed type.</p>
<p><strong><br><br>See also:<br> </strong><a href="https://www.devexpress.com/Support/Center/p/E1150">How to prevent altering the legacy database schema when creating an XAF application</a></p>

<br/>


