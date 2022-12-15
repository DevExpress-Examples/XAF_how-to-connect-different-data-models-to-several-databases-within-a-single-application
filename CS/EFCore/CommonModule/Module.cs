using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.ApplicationBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using CommonModule.BusinessObjects;
using DevExpress.ExpressApp.ApplicationBuilder.Internal;
using Microsoft.Extensions.Configuration;

namespace CommonModule;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class CommonModule : ModuleBase {

#if EASYTEST
    const string ConnectionStringName = "EasyTestConnectionString";
#else
    const string ConnectionStringName = "ConnectionString0";
#endif

    public CommonModule() {
        // 
        // CommonModuleModule
        // 
        AdditionalExportedTypes.Add(typeof(ApplicationUser));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifference));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
		DevExpress.ExpressApp.Security.SecurityModule.UsedExportedTypes = DevExpress.Persistent.Base.UsedExportedTypes.Custom;
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        // Manage various aspects of the application UI and behavior at the module level.
    }

    public static void SetupObjectSpace<TContext>(IObjectSpaceProviderBuilder<TContext> objectSpaceProviderBuilder)
            where TContext : IXafApplicationBuilder<TContext> {
        string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName]?.ConnectionString;
        objectSpaceProviderBuilder.AddSecuredEFCore()
            .WithDbContext<CommonModuleEFCoreDbContext>((application, options) => SetupDbContext(options, connectionString));
    }

    public static void SetupObjectSpace<TContext>(IObjectSpaceProviderServiceBasedBuilder<TContext> objectSpaceProviderBuilder, IConfiguration configuration)
        where TContext : IXafApplicationBuilder<TContext>, IAccessor<IServiceCollection> {
        string connectionString = configuration.GetConnectionString(ConnectionStringName);
        objectSpaceProviderBuilder.AddSecuredEFCore()
            .WithDbContext<CommonModuleEFCoreDbContext>((application, options) => SetupDbContext(options, connectionString));
    }

    static void SetupDbContext(DbContextOptionsBuilder options, string connectionString) {
        // Uncomment this code to use an in-memory database. This database is recreated each time the server starts. With the in-memory database, you don't need to make a migration when the data model is changed.
        // Do not use this code in production environment to avoid data loss.
        // We recommend that you refer to the following help topic before you use an in-memory database: https://docs.microsoft.com/en-us/ef/core/testing/in-memory
        //options.UseInMemoryDatabase("InMemory");
        ArgumentNullException.ThrowIfNull(connectionString);
        options.UseSqlServer(connectionString);
        options.UseChangeTrackingProxies();
        options.UseObjectSpaceLinkProxies();
        options.UseLazyLoadingProxies();
    }
}
