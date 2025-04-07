using ServerUtilities;
using Services;
using System;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(MainService)))
            {
                try
                {
                    serviceHost.Open();

                    DateTime currentDateTime = DateTime.Now;
                    Console.WriteLine($"ItaliaPizza server is running - Start Time: [{currentDateTime}]");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting services: {ex.Message}");
                    Logger.Log($"Error starting services - : {ex.Message}");
                }
                finally
                {
                    serviceHost.Abort();
                }
            }
        }
    }
}
