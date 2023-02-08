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
    private readonly IConfiguration configuration;

    public XafModule1(IConfiguration configuration) : this() {
        this.configuration = configuration;
    }

    public XafModule1() { }

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
        if (typeInfoSource1 == null) {
            lock (lockObj) {
                if (typeInfoSource1 == null) {
                    typeInfoSource1 = new XpoTypeInfoSource((TypesInfo)application.TypesInfo,
                        typeof(PersistentClass1)
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
            typeInfoSource1, true
        );
        objectSpaceProvider1.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
        e.ObjectSpaceProviders.Add(objectSpaceProvider1);
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
