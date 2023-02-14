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
using DevExpress.ExpressApp.ApplicationBuilder.Internal;
using DevExpress.ExpressApp.ApplicationBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace ClassLibrary1;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class XafModule1 : ModuleBase {

#if EASYTEST
    const string ConnectionStringName = "EasyTestConnectionString";
#else
    const string ConnectionStringName = "ConnectionString1";
#endif

    private static readonly object lockObj = new object();
    // Here we will have a single instance, which is initialized only once during the application life cycle.
    private static XpoTypeInfoSource typeInfoSource1;

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }

    static void InitTypeInfoSource(ITypesInfo typesInfo) {
        if (typeInfoSource1 == null) {
            lock (lockObj) {
                if (typeInfoSource1 == null) {
                    typeInfoSource1 = new XpoTypeInfoSource((TypesInfo)typesInfo,
                        typeof(PersistentClass1)
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
            typeInfoSource1,
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
        if (ObjectSpace.CanInstantiate(typeof(PersistentClass1))) {
            string str = "test1";
            PersistentClass1 theObject = ObjectSpace.FindObject<PersistentClass1>(CriteriaOperator.Parse("PersistentProperty1A = ?", str));
            if (theObject == null) {
                theObject = ObjectSpace.CreateObject<PersistentClass1>();
                theObject.PersistentProperty1A = str;
                theObject.PersistentProperty1B = str;
            }
            ObjectSpace.CommitChanges();
        }
    }
}
