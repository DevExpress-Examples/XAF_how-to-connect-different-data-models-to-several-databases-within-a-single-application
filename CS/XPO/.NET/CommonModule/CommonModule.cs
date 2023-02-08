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
    private readonly IConfiguration configuration;

    public CommonModule(IConfiguration configuration) : this() {
        this.configuration = configuration;
    }

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
        application.CreateCustomObjectSpaceProvider += application_CreateCustomObjectSpaceProvider;
        (application.Security as SecurityStrategy)?.RegisterXPOAdapterProviders(new SecurityPermissionsProviderDefault(application));
        application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
    }

    private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e) {
        (e.ObjectSpace as CompositeObjectSpace)?.PopulateAdditionalObjectSpaces((XafApplication)sender);
    }

    void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
        XafApplication application = (XafApplication)sender;
        if (typeInfoSource == null) {
            lock (lockObj) {
                if (typeInfoSource == null) {
                    typeInfoSource = new XpoTypeInfoSource((TypesInfo)application.TypesInfo,
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
        string connectionString = configuration != null
            ? configuration.GetConnectionString(ConnectionStringName)
            : ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        IObjectSpaceProvider objectSpaceProvider1 = new SecuredObjectSpaceProvider(
            (SecurityStrategyComplex)application.Security,
            new ConnectionStringDataStoreProvider(connectionString),
            application.TypesInfo,
            typeInfoSource, true
        );
        objectSpaceProvider1.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
        e.ObjectSpaceProviders.Add(objectSpaceProvider1);
        e.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(application.TypesInfo, null));
    }
}
