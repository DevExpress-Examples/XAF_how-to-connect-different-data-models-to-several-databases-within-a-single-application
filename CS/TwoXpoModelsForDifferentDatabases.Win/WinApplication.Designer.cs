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
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.xafModule11 = new ClassLibrary1.XafModule1();
            this.xafModule21 = new ClassLibrary2.XafModule2();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.\\SQLEXPRESS;Initial Catalog=T" +
    "woXpoModelsForDifferentDatabases";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // TwoXpoModelsForDifferentDatabasesWindowsFormsApplication
            // 
            this.ApplicationName = "TwoXpoModelsForDifferentDatabases";
            this.Connection = this.sqlConnection1;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.xafModule11);
            this.Modules.Add(this.xafModule21);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.TwoXpoModelsForDifferentDatabasesWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.TwoXpoModelsForDifferentDatabasesWindowsFormsApplication_CustomizeLanguagesList);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private ClassLibrary1.XafModule1 xafModule11;
        private ClassLibrary2.XafModule2 xafModule21;
    }
}
