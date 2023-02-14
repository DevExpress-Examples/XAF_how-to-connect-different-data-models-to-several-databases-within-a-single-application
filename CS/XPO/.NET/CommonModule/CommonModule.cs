using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security;
using CommonModule.BusinessObjects;
using DevExpress.ExpressApp.Security.Adapters;
using DevExpress.ExpressApp.ApplicationBuilder;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.ExpressApp.ApplicationBuilder.Internal;

namespace CommonModule;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class CommonModule : ModuleBase {

#if EASYTEST
    const string ConnectionStringName = "EasyTestConnectionString";
#else
    const string ConnectionStringName = "ConnectionString0";
#endif

    private static readonly object lockObj = new object();
    // Here we will have a single instance, which is initialized only once during the application life cycle.
    private static XpoTypeInfoSource typeInfoSource;

    public CommonModule() {
		// 
		// TwoXpoModelsForDifferentDatabasesModule
		// 
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifference));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect));
        AdditionalExportedTypes.Add(typeof(ApplicationUser));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
    }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        (application.Security as SecurityStrategy)?.RegisterXPOAdapterProviders(new SecurityPermissionsProviderDefault(application));
    }

    static void InitTypeInfoSource(ITypesInfo typesInfo) {
        if (typeInfoSource == null) {
            lock (lockObj) {
                if (typeInfoSource == null) {
                    typeInfoSource = new XpoTypeInfoSource((TypesInfo)typesInfo,
                        typeof(DevExpress.Persistent.BaseImpl.ModelDifference),
                        typeof(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect),
                        typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser),
                        typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole),
                        typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyTypePermissionObject),
                        typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyObjectPermissionsObject),
                        typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyMemberPermissionsObject),
                        typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyNavigationPermissionObject),
                        typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyActionPermissionObject),
                        typeof(ApplicationUser)
                    );
                }
            }
        }
    }

    public static void SetupObjectSpace<TContext>(IObjectSpaceProviderServiceBasedBuilder<TContext> builder, IConfiguration configuration)
        where TContext : IXafApplicationBuilder<TContext>, IAccessor<IServiceCollection> {
        builder.Add(delegate (IServiceProvider serviceProvider) {
            string connectionString = configuration.GetConnectionString(ConnectionStringName);
            ISelectDataSecurityProvider selectDataSecurityProvider = (ISelectDataSecurityProvider)serviceProvider.GetRequiredService<ISecurityStrategyBase>();
            ITypesInfo typesInfo = serviceProvider.GetRequiredService<ITypesInfo>();
            return CreateObjectSpaceProvider(selectDataSecurityProvider, typesInfo, connectionString);
        });
    }

    public static void SetupObjectSpace<TContext>(IObjectSpaceProviderBuilder<TContext> builder) where TContext : IXafApplicationBuilder<TContext> {
        builder.Add(delegate (XafApplication application, CreateCustomObjectSpaceProviderEventArgs _) {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            return CreateObjectSpaceProvider((ISelectDataSecurityProvider)application.Security, application.TypesInfo, connectionString);
        });
    }

    static IObjectSpaceProvider CreateObjectSpaceProvider(ISelectDataSecurityProvider selectDataSecurityProvider, ITypesInfo typesInfo, string connectionString) {
        InitTypeInfoSource(typesInfo);
        var objectSpaceProvider = new SecuredObjectSpaceProvider(
            selectDataSecurityProvider,
            new ConnectionStringDataStoreProvider(connectionString),
            typesInfo,
            typeInfoSource,
            true
        );
        objectSpaceProvider.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
        return objectSpaceProvider;
    }
}
