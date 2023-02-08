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
    private readonly IConfiguration configuration;

    public XafModule2(IConfiguration configuration) : this() {
        this.configuration = configuration;
    }

    public XafModule2() { }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.CreateCustomObjectSpaceProvider += application_CreateCustomObjectSpaceProvider;
    }

    void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
        XafApplication application = (XafApplication)sender;
        if (typeInfoSource2 == null) {
            lock (lockObj) {
                if (typeInfoSource2 == null) {
                    typeInfoSource2 = new XpoTypeInfoSource((TypesInfo)application.TypesInfo,
                        typeof(PersistentClass2)
                    );
                }
            }
        }
        string connectionString = configuration != null
            ? configuration.GetConnectionString(ConnectionStringName)
            : ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        IObjectSpaceProvider objectSpaceProvider2 = new SecuredObjectSpaceProvider(
            (SecurityStrategyComplex)application.Security,
            new ConnectionStringDataStoreProvider(connectionString),
            application.TypesInfo,
            typeInfoSource2, true
        );
        objectSpaceProvider2.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
        e.ObjectSpaceProviders.Add(objectSpaceProvider2);
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
