namespace CrawlDataService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SyncOmsServiceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.SyncOmsServiceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // SyncOmsServiceProcessInstaller1
            // 
            this.SyncOmsServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SyncOmsServiceProcessInstaller1.Password = null;
            this.SyncOmsServiceProcessInstaller1.Username = null;
            // 
            // SyncOmsServiceInstaller1
            // 
            this.SyncOmsServiceInstaller1.DelayedAutoStart = true;
            this.SyncOmsServiceInstaller1.ServiceName = "Rapid Logistics Update Data";
            this.SyncOmsServiceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SyncOmsServiceProcessInstaller1,
            this.SyncOmsServiceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SyncOmsServiceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller SyncOmsServiceInstaller1;
    }
}