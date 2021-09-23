using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonModule.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Adapters;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;

namespace CommonModule {
    public class Module : ModuleBase {
        public Module() {
            this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifference));
            this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect));
            this.AdditionalExportedTypes.Add(typeof(ApplicationUser));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
        }
        private static readonly object lockObj = new object();
        // Here we will have a single instance, which is initialized only once during the application life cycle.
        private static XpoTypeInfoSource typeInfoSource = null;
        public override void Setup(XafApplication application) {
            base.Setup(application);
            application.CreateCustomObjectSpaceProvider += application_CreateCustomObjectSpaceProvider;
            application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
        }
        private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e) {
            (e.ObjectSpace as CompositeObjectSpace)?.PopulateAdditionalObjectSpaces((XafApplication)sender);
        }
        void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
            XafApplication application = (XafApplication)sender;
            if(typeInfoSource == null) {
                lock(lockObj) {
                    if(typeInfoSource == null) {
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
            IObjectSpaceProvider objectSpaceProvider = new SecuredObjectSpaceProvider(
                (SecurityStrategyComplex)application.Security,
                    new ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings["ConnectionStringDatabaseCommon"].ConnectionString),
                    application.TypesInfo,
                    typeInfoSource, true
                );
            objectSpaceProvider.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
            e.ObjectSpaceProviders.Add(objectSpaceProvider);
            e.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(application.TypesInfo, null));
            ((SecurityStrategyComplex)application.Security).RegisterXPOAdapterProviders(new SecurityPermissionsProviderDefault(application));
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new CommonModule.DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
    }
}
