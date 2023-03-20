using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.ApplicationBuilder;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.ExpressApp.ApplicationBuilder.Internal;

namespace ClassLibrary2;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class XafModule2 : ModuleBase {

#if EASYTEST
    const string ConnectionStringName = "EasyTestConnectionString";
#else
    const string ConnectionStringName = "ConnectionString2";
#endif

    private static readonly object lockObj = new object();
    // Here we will have a single instance, which is initialized only once during the application life cycle.
    private static XpoTypeInfoSource typeInfoSource2;

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }

    static void InitTypeInfoSource(ITypesInfo typesInfo) {
        if (typeInfoSource2 == null) {
            lock (lockObj) {
                if (typeInfoSource2 == null) {
                    typeInfoSource2 = new XpoTypeInfoSource((TypesInfo)typesInfo,
                        typeof(PersistentClass2)
                    );
                }
            }
        }
    }

    public static void SetupObjectSpace<TContext>(IObjectSpaceProviderServiceBasedBuilder<TContext> builder, IConfiguration configuration)
    where TContext : IXafApplicationBuilder<TContext>, IAccessor<IServiceCollection> {
        string connectionString = configuration.GetConnectionString(ConnectionStringName);
        var dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, null, true);
        builder.Add(delegate (IServiceProvider serviceProvider) {
            ISelectDataSecurityProvider selectDataSecurityProvider = (ISelectDataSecurityProvider)serviceProvider.GetRequiredService<ISecurityStrategyBase>();
            ITypesInfo typesInfo = serviceProvider.GetRequiredService<ITypesInfo>();
            return CreateObjectSpaceProvider(selectDataSecurityProvider, typesInfo, dataStoreProvider);
        });
    }

    public static void SetupObjectSpace<TContext>(IObjectSpaceProviderBuilder<TContext> builder) where TContext : IXafApplicationBuilder<TContext> {
        string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        var dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, null, false);
        builder.Add(delegate (XafApplication application, CreateCustomObjectSpaceProviderEventArgs _) {
            return CreateObjectSpaceProvider((ISelectDataSecurityProvider)application.Security, application.TypesInfo, dataStoreProvider);
        });
    }

    static IObjectSpaceProvider CreateObjectSpaceProvider(ISelectDataSecurityProvider selectDataSecurityProvider, ITypesInfo typesInfo, IXpoDataStoreProvider xpoDataStoreProvider) {
        InitTypeInfoSource(typesInfo);
        var objectSpaceProvider = new SecuredObjectSpaceProvider(
            selectDataSecurityProvider,
            xpoDataStoreProvider,
            typesInfo,
            typeInfoSource2,
            true
        );
        objectSpaceProvider.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
        return objectSpaceProvider;
    }
}

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        // Check whether it is a valid ObjectSpace to create objects of a certain type.
        if (ObjectSpace.CanInstantiate(typeof(PersistentClass2))) {
            string str = "test2";
            PersistentClass2 theObject = ObjectSpace.FindObject<PersistentClass2>(CriteriaOperator.Parse("PersistentProperty2A = ?", str));
            if (theObject == null) {
                theObject = ObjectSpace.CreateObject<PersistentClass2>();
                theObject.PersistentProperty2A = str;
                theObject.PersistentProperty2B = str;
            }
            ObjectSpace.CommitChanges();
        }
    }
}
