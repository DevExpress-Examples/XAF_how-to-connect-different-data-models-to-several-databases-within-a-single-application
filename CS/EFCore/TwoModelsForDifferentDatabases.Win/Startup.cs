using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.ExpressApp.Win;
using Microsoft.EntityFrameworkCore;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.ExpressApp.Design;

namespace TwoModelsForDifferentDatabases.Win;

public class ApplicationBuilder : IDesignTimeApplicationFactory {
    public static WinApplication BuildApplication(string connectionString) {
        var builder = WinApplication.CreateBuilder();
        builder.UseApplication<TwoModelsForDifferentDatabasesWindowsFormsApplication>();
        builder.Modules
            .AddConditionalAppearance()
            .AddValidation(options => {
                options.AllowValidationDetailsAccess = false;
            })
            .Add<CommonModule.CommonModule>()
        	.Add<TwoModelsForDifferentDatabasesWinModule>()
            .Add<ClassLibrary1.XafModule1>()
            .Add<ClassLibrary2.XafModule2>();
        CommonModule.CommonModule.SetupObjectSpace(builder.ObjectSpaceProviders);
        ClassLibrary1.XafModule1.SetupObjectSpace(builder.ObjectSpaceProviders);
        ClassLibrary2.XafModule2.SetupObjectSpace(builder.ObjectSpaceProviders);
        builder.ObjectSpaceProviders.AddNonPersistent();
        builder.Security
            .UseIntegratedMode(options => {
                options.RoleType = typeof(PermissionPolicyRole);
                options.UserType = typeof(CommonModule.BusinessObjects.ApplicationUser);
                options.UserLoginInfoType = typeof(CommonModule.BusinessObjects.ApplicationUserLoginInfo);
            })
            .UsePasswordAuthentication();
        builder.AddBuildStep(application => {
            application.ConnectionString = connectionString;
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && application.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        });
        var winApplication = builder.Build();
        return winApplication;
    }

    XafApplication IDesignTimeApplicationFactory.Create()
        => BuildApplication(XafApplication.DesignTimeConnectionString);
}
