using Services.Dtos;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MainService : IMainManager
    {
        // Servicios subyacentes (creados internamente)
        private readonly ISupplyManager _supplyService;
        private readonly ISupplierManager _supplierService;
        private readonly IPersonalManager _personalService;
        private readonly IProductManager _productService;

        // Constructor que crea las instancias directamente
        public MainService()
        {
            _supplyService = new SupplyService();
            _supplierService = new SupplierService();
            _personalService = new PersonalService();
            _productService = new ProductService();
        }

        public bool Ping()
        {
            Console.WriteLine("Ping received. The server is available.");
            return true;
        }

        public int AddProduct(ProductDTO productDTO) => _productService.AddProduct(productDTO);

        public int AddPersonal(PersonalDTO personalDTO) => _personalService.AddPersonal(personalDTO);
        public bool IsUsernameAvailable(string username) => _personalService.IsUsernameAvailable(username);
        public bool IsRfcUnique(string rfc) => _personalService.IsRfcUnique(rfc);
    }
}