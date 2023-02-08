using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.ExpressApp.ApplicationBuilder.Internal;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ClassLibrary2 {
    public class XafModule2 : ModuleBase {

        const string ConnectionStringName = "ConnectionString2";

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            XafModuleUpdater2 updater = new XafModuleUpdater2(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }

        public static void SetupObjectSpace<TContext>(IObjectSpaceProviderBuilder<TContext> objectSpaceProviderBuilder)
            where TContext : IXafApplicationBuilder<TContext> {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName]?.ConnectionString;
            objectSpaceProviderBuilder.AddSecuredEFCore()
                .WithDbContext<ClassLibrary2EFCoreDbContext>((application, options) => SetupDbContext(options, connectionString));
        }

        public static void SetupObjectSpace<TContext>(IObjectSpaceProviderServiceBasedBuilder<TContext> objectSpaceProviderBuilder, IConfiguration configuration)
            where TContext : IXafApplicationBuilder<TContext>, IAccessor<IServiceCollection> {
            string connectionString = configuration.GetConnectionString(ConnectionStringName);
            objectSpaceProviderBuilder.AddSecuredEFCore()
                .WithDbContext<ClassLibrary2EFCoreDbContext>((application, options) => SetupDbContext(options, connectionString));
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
    public class XafModuleUpdater2 : ModuleUpdater {
        public XafModuleUpdater2(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            // Check whether it is a valid ObjectSpace to create objects of a certain type.
            if (ObjectSpace.CanInstantiate(typeof(PersistentClass2))) {
                string str = "test2";
                PersistentClass2 theObject = ObjectSpace.FindObject<PersistentClass2>(CriteriaOperator.Parse("PersistentPropertyX = ?", str));
                if (theObject == null) {
                    theObject = ObjectSpace.CreateObject<PersistentClass2>();
                    theObject.PersistentPropertyX = str;
                }
                ObjectSpace.CommitChanges();
            }
        }
    }
}
