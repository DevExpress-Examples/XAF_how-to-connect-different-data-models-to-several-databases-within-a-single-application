<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128588378/22.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4896)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to connect different data models to several databases within a single application with Entity Framework Core

This example demonstrates how to create custom XAF modules with custom business objects and logic that would work with separate databases. These modules do not depend on each other and thus can be reused in other applications as a whole.  
Usually, the connection to the database is set up in the executable [application project](http://documentation.devexpress.com/#Xaf/CustomDocument2569). Typically, it is performed in the configuration file or directly in the code of the application's designer or within the Main function/Global application class. To learn more, please check out this help topic: [Connect an XAF Application to a Database Provider](http://documentation.devexpress.com/#Xaf/CustomDocument3155) In this example, you will learn how to establish a connection to the database directly from your module projects.

> **Note** 
> This Readme focuses on Entity Framework Core. For information on how to achieve the same functionality with XPO on .NET 6+ and .NET Framework, see the **Readme.md** files in the respective solution folders:
> - [.NET 6+](./CS/XPO/.NET)
> - [.NET Framewok](./CS/XPO/.NetFramework)

## Implementation Details

**1.** In the configuration files for the Blazor and WinForms projects, define three separate connection strings: one connection string for a database that stores security data and two additional connection strings for separate databases used to store application data.

**2.** Configure the main module's DbContext to store data used by the Security System (users, roles and login information).

  ```cs
  public class CommonModuleEFCoreDbContext : DbContext {
    // ...
    public DbSet<PermissionPolicyRole> Roles { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationUserLoginInfo> UserLoginInfos { get; set; }
    // ...
  }
  ```

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

**4.** For each new module, implement its own DbContext and persistent classes as shown in example solution's _ClassLibraryN/ClassLibraryNDbContext.cs_ and _ClassLibraryN/PersistentClassN.cs_ files.

**5.** Add static methods that configure the ObjectSpace and DbContexts (`SetupObjectSpace()` and `SetupDbContext()`) to all module classes in the solution:

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

