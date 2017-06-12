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
            this.FukingCrawlDataProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.FukingCrawlDataInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // FukingCrawlDataProcessInstaller1
            // 
            this.FukingCrawlDataProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.FukingCrawlDataProcessInstaller1.Password = null;
            this.FukingCrawlDataProcessInstaller1.Username = null;
            // 
            // FukingCrawlDataInstaller1
            // 
            this.FukingCrawlDataInstaller1.ServiceName = "FukingCrawlData";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FukingCrawlDataProcessInstaller1,
            this.FukingCrawlDataInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller FukingCrawlDataProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller FukingCrawlDataInstaller1;
    }
}