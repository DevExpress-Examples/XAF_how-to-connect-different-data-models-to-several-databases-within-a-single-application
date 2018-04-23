using System;
using System.Collections.Generic;
using System.Configuration;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;

namespace ClassLibrary1 {
    public class XafModule1 : ModuleBase {
        // Here we will have a single instance, which is initialized only once during the application life cycle.
        private static XPObjectSpaceProvider objectSpaceProvider1 = null;

        public override void Setup(XafApplication application) {
            base.Setup(application);
            application.CreateCustomObjectSpaceProvider += application_CreateCustomObjectSpaceProvider;
        }
        void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
            XafApplication application = (XafApplication)sender;
            if (objectSpaceProvider1 == null) {
                XpoTypeInfoSource typeInfoSource1 = new XpoTypeInfoSource((TypesInfo)application.TypesInfo,
                    typeof(PersistentClass1), typeof(ModuleInfo1)
                );
                objectSpaceProvider1 = new XPObjectSpaceProvider(
                    new ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings["ConnectionStringDatabase1"].ConnectionString),
                    application.TypesInfo,
                    typeInfoSource1, true
                );
            }
            e.ObjectSpaceProviders.Add(objectSpaceProvider1);
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            XafModuleUpdater1 updater = new XafModuleUpdater1(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
    }
    public class XafModuleUpdater1 : ModuleUpdater {
        public XafModuleUpdater1(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            // Check whether it is a valid ObjectSpace to create objects of a certain type.
            if (ObjectSpace.Database.Contains("1")) {
            //if (ObjectSpace.CanInstantiate(typeof(PersistentClass1))) {
                string str = "test1";
                PersistentClass1 theObject = ObjectSpace.FindObject<PersistentClass1>(CriteriaOperator.Parse("PersistentProperty1 = ?", str));
                if (theObject == null) {
                    theObject = ObjectSpace.CreateObject<PersistentClass1>();
                    theObject.PersistentProperty1 = str;
                }
            }
        }
    }
}