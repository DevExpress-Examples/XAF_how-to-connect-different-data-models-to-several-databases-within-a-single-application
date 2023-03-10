# How to connect different data models to several databases within a single application with XPO on .NET 6+

This example demonstrates how to create custom XAF modules with custom business objects and logic that would work with separate databases. These modules do not depend on each other and thus can be reused in other applications as a whole.  
Usually, the connection to the database is set up in the executable [application project](http://documentation.devexpress.com/#Xaf/CustomDocument2569). Typically, it is performed in the configuration file or directly in the code of the application's designer or within the Main function/Global application class. To learn more, please check out this help topic: [Connect an XAF Application to a Database Provider](http://documentation.devexpress.com/#Xaf/CustomDocument3155) In this example, you will learn how to establish a connection to the database directly from your module projects.

## Implementation Details

**1.** In the configuration files for the Blazor and WinForms projects, define three separate connection strings: one connection string for a database that stores security data and two additional connection strings for separate databases used to store application data.

**2.** Implements persistent classes required to store security-related data to the main module (see the files in the example project's _[CommonModule/BusinessObjects](./CommonModule/BusinessObjects/)_ folder).

In the platform-specific projects, edit the _startup.cs_ files to configure the Security System so that it uses the main module's persistent classes to store security-related data:

  **ASP.NET Core Blazor:**

  ```cs
  public class Startup {
      // ...
      public void ConfigureServices(IServiceCollection services) {
          services.AddXaf(Configuration, builder => {
              builder.Security
                  .UseIntegratedMode(options => {
                      options.RoleType = typeof(PermissionPolicyRole);
                      options.UserType = typeof(CommonModule.BusinessObjects.ApplicationUser);
                      options.UserLoginInfoType = typeof(CommonModule.BusinessObjects.ApplicationUserLoginInfo);
                  })
              // ...
          });
      }
  }
  ```

  **Windows Forms:**

  ```cs
  public class ApplicationBuilder : IDesignTimeApplicationFactory {
      public static WinApplication BuildApplication(string connectionString) {
          var builder = WinApplication.CreateBuilder();
          // ...
          builder.Security
              .UseIntegratedMode(options => {
                  options.RoleType = typeof(PermissionPolicyRole);
                  options.UserType = typeof(CommonModule.BusinessObjects.ApplicationUser);
                  options.UserLoginInfoType = typeof(CommonModule.BusinessObjects.ApplicationUserLoginInfo);
              })
          // ...
  }
  ```

**3.** Add two new custom XAF modules into a new XAF solution using the XAF Solution Wizard [as described here](https://docs.devexpress.com/eXpressAppFramework/118046/application-shell-and-base-infrastructure/application-solution-components/modules).

**4.** Add required persistent classes into these modules as shown in the _ClassLibraryN/PersistentClassN.xx_ files.

**5.** Add static methods that configure the ObjectSpace (`SetupObjectSpace()` and `CreateObjectSpaceProvider()`) to all module classes in the solution:

- _ClassLibrary1/XafModule1.cs_
- _ClassLibrary2/XafModule2.cs_
- _CommonModule/Module.cs_

**6.** Make the following edits to the _Startup.cs_ files for the Blazor and WinForms projects:

- Register the modules as shown in the code sample below:

  ```cs
  public void ConfigureServices(IServiceCollection services) {
      // ...
      services.AddXaf(Configuration, builder => {
          // ...
          builder.Modules 
              .Add<ClassLibrary1.XafModule1>()
              .Add<ClassLibrary2.XafModule2>();
          // ...
      }
  }
  ```

- Call the `SetupObjectSpace()` static methods for the ClassLibrary1 and ClassLibrary2 modules. Blazor and WinForms require different overloads of this method:

  **ASP.NET Core Blazor:**

  ```cs
  public void ConfigureServices(IServiceCollection services) {
      // ...
      services.AddXaf(Configuration, builder => {
          // ...
          ClassLibrary1.XafModule1.SetupObjectSpace(builder.ObjectSpaceProviders, Configuration);
          ClassLibrary2.XafModule2.SetupObjectSpace(builder.ObjectSpaceProviders, Configuration);
          // ...
      }
  }
  ```

  **Windows Forms:**

  ```cs
  public void ConfigureServices(IServiceCollection services) {
      // ...
      services.AddXaf(Configuration, builder => {
          // ...
          ClassLibrary1.XafModule1.SetupObjectSpace(builder.ObjectSpaceProviders);
          ClassLibrary2.XafModule2.SetupObjectSpace(builder.ObjectSpaceProviders);
          // ...
      }
  }
  ```

## Files to Review

### Modules:

* [PersistentClass1.cs](./ClassLibrary1/PersistentClass1.cs)
* [XafModule1.cs](./ClassLibrary1/XafModule1.cs)
* [PersistentClass2.cs](./ClassLibrary2/PersistentClass2.cs)
* [XafModule2.cs](./ClassLibrary2/XafModule2.cs)
* **[Module.cs](./CommonModule/CommonModule.cs)**

### Blazor application:

* [Startup.cs](./TwoXpoModelsForDifferentDatabases.Blazor.Server/Startup.cs)
* [appsettings.json](./TwoXpoModelsForDifferentDatabases.Blazor.Server/appsettings.json)

### WinForms application:

* [Startup.cs](./TwoXpoModelsForDifferentDatabases.Win/Startup.cs)
* [appsettings.json](./TwoXpoModelsForDifferentDatabases.Win/App.config)

## More Examples

- [How to prevent altering the legacy database schema when creating an XAF application](https://github.com/DevExpress-Examples/XAF_how-to-prevent-altering-the-legacy-database-schema-when-creating-an-xaf-application-e1150)
- [Creating and consuming multiple XPObjectSpaceProviders in Web API Service](https://supportcenter.devexpress.com/internal/ticket/details/T1122851)