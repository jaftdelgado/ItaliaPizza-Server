using Services.Dtos;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;


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
        private readonly IFinanceManager _financeService;
        private readonly ISupplierOrderManager _supplierOrderService;
        private readonly ISupplyManager supplyService = new SupplyService();


        // Constructor que crea las instancias directamente
        public MainService()
        {
            _supplyService = new SupplyService();
            _supplierService = new SupplierService();
            _personalService = new PersonalService();
            _productService = new ProductService();
            _financeService = new FinanceService();
            _supplierOrderService = new SupplierOrderService();
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

        public List<SupplyCategoryDTO> GetAllCategories()
        {
            return supplyService.GetAllCategories();
        }

        public List<SupplierDTO> GetSuppliersByCategory(int categoryId)
        {
            return supplyService.GetSuppliersByCategory(categoryId);
        }

        public List<SupplyDTO> GetSuppliesBySupplier(int supplierId)
        {
            return supplyService.GetSuppliesBySupplier(supplierId);
        }
        public int RegisterOrder(SupplierOrderDTO dto)
        {
            return _supplierOrderService.RegisterOrder(dto);
        }

        public bool RegisterTransaction(TransactionDTO transaction)
        {
            return _financeService.RegisterTransaction(transaction);
        }

        public LinkedList<SupplyDTO> GetAllSupplies()
        {
            return supplyService.GetAllSupplies();
        }
    }
}