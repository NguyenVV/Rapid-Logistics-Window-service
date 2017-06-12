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
            this.CrawlDataProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.CrawlDataInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // CrawlDataProcessInstaller1
            // 
            this.CrawlDataProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.CrawlDataProcessInstaller1.Password = null;
            this.CrawlDataProcessInstaller1.Username = null;
            // 
            // CrawlDataInstaller1
            // 
            this.CrawlDataInstaller1.DelayedAutoStart = true;
            this.CrawlDataInstaller1.ServiceName = "Rapid Logistics Update Data";
            this.CrawlDataInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.CrawlDataProcessInstaller1,
            this.CrawlDataInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller CrawlDataProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller CrawlDataInstaller1;
    }
}