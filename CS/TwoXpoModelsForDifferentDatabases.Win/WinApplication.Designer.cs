namespace TwoXpoModelsForDifferentDatabases.Win {
    partial class TwoXpoModelsForDifferentDatabasesWindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.xafModule11 = new ClassLibrary1.XafModule1();
            this.xafModule21 = new ClassLibrary2.XafModule2();
            this.commonModule = new CommonModule.Module();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.securityStrategyComplex1.SupportNavigationPermissionsForTypes = false;
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
            // ApplicationUser descends from PermissionPolicyUser and supports OAuth authentication. For more information, refer to the following help topic: https://docs.devexpress.com/eXpressAppFramework/402197
            // If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
            this.securityStrategyComplex1.UserType = typeof(CommonModule.BusinessObjects.ApplicationUser);
            // 
            // securityModule1
            // 
            this.securityModule1.UserType = typeof(CommonModule.BusinessObjects.ApplicationUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
            // Comment out the following line if using PermissionPolicyUser or a custom user type.
            this.authenticationStandard1.UserLoginInfoType = typeof(CommonModule.BusinessObjects.ApplicationUserLoginInfo);
            // 
            // TwoXpoModelsForDifferentDatabasesWindowsFormsApplication
            // 
            this.ApplicationName = "TwoXpoModelsForDifferentDatabases";
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.xafModule11);
            this.Modules.Add(this.xafModule21);
            this.Modules.Add(this.commonModule);
            this.Modules.Add(this.securityModule1);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.TwoXpoModelsForDifferentDatabasesWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.TwoXpoModelsForDifferentDatabasesWindowsFormsApplication_CustomizeLanguagesList);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private ClassLibrary1.XafModule1 xafModule11;
        private ClassLibrary2.XafModule2 xafModule21;
        private CommonModule.Module commonModule;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
    }
}
