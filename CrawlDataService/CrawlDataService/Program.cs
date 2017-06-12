using System.ServiceProcess;

namespace CrawlDataService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            //While debugging this section is used.
            CrawlDataService myService = new CrawlDataService();
            myService.onDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#else
            //In Release this section is used. This is the "normal" way.
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new CrawlDataService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
