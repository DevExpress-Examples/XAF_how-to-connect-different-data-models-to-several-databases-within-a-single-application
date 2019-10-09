using System;
using System.Collections.Generic;
using System.Configuration;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;

namespace ClassLibrary2 {
    public class XafModule2 : ModuleBase {
        // Here we will have a single instance, which is initialized only once during the application life cycle.
        private static XPObjectSpaceProvider objectSpaceProvider2 = null;
        public override void Setup(XafApplication application) {
            base.Setup(application);
            application.CreateCustomObjectSpaceProvider += application_CreateCustomObjectSpaceProvider;
        }
        void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
            XafApplication application = (XafApplication)sender;
            if (objectSpaceProvider2 == null) {
                XpoTypeInfoSource typeInfoSource2 = new XpoTypeInfoSource((TypesInfo)application.TypesInfo,
                    typeof(PersistentClass2), typeof(ModuleInfo2)
                );
                objectSpaceProvider2 = new XPObjectSpaceProvider(
                    new ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings["ConnectionStringDatabase2"].ConnectionString),
                    application.TypesInfo,
                    typeInfoSource2, true
                );
            }
            e.ObjectSpaceProviders.Add(objectSpaceProvider2);
            e.IsObjectSpaceProviderOwner = false;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            XafModuleUpdater2 updater = new XafModuleUpdater2(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
    }
    public class XafModuleUpdater2 : ModuleUpdater {
        public XafModuleUpdater2(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            // Check whether it is a valid ObjectSpace to create objects of a certain type.
            if (ObjectSpace.Database.Contains("2")) {
            //if (ObjectSpace.CanInstantiate(typeof(PersistentClass2))) {
                string str = "test2";
                PersistentClass2 theObject = ObjectSpace.FindObject<PersistentClass2>(CriteriaOperator.Parse("PersistentProperty2 = ?", str));
                if (theObject == null) {
                    theObject = ObjectSpace.CreateObject<PersistentClass2>();
                    theObject.PersistentProperty2 = str;
                }
            }
        }
    }
}