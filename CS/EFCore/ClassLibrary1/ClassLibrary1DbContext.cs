using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;

namespace ClassLibrary1;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class ClassLibrary1ContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<ClassLibrary1EFCoreDbContext>()
            .UseSqlServer(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new ClassLibrary1EFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class ClassLibrary1DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ClassLibrary1EFCoreDbContext> {
	public ClassLibrary1EFCoreDbContext CreateDbContext(string[] args) {
		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		//var optionsBuilder = new DbContextOptionsBuilder<ClassLibraryEFCoreDbContext>();
		//optionsBuilder.UseSqlServer("Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DB1");
        //optionsBuilder.UseChangeTrackingProxies();
        //optionsBuilder.UseObjectSpaceLinkProxies();
		//return new ClassLibraryEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(ClassLibrary1ContextInitializer))]
public class ClassLibrary1EFCoreDbContext : DbContext {
	public ClassLibrary1EFCoreDbContext(DbContextOptions<ClassLibrary1EFCoreDbContext> options) : base(options) {
	}
	public DbSet<ModuleInfo1> ModulesInfo1 { get; set; }
    public DbSet<PersistentClass1> PersistentClasses1 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
    }
}
