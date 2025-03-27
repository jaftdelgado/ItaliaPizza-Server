using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MainService : IMainManager
    {
        private readonly SupplyService _supplyService = new SupplyService();
        private readonly SupplierService _supplierService = new SupplierService();
        private readonly PersonalService _personalService = new PersonalService();

        public bool Ping()
        {
            Console.WriteLine("Ping received. The server is available.");
            return true;
        }
    }
}
