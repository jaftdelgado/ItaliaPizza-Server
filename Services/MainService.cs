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
        private readonly ISupplierOrderManager _supplierOrderService;

        // Constructor que crea las instancias directamente
        public MainService()
        {
            _supplyService = new SupplyService();
            _supplierService = new SupplierService();
            _personalService = new PersonalService();
            _productService = new ProductService();
            _supplierOrderService = new SupplierOrderService();
        }

        public bool Ping()
        {
            Console.WriteLine("Ping received. The server is available.");
            return true;
        }

        // Implementación de métodos 

        public int AddProduct(ProductDTO productDTO)
        {
            return _productService.AddProduct(productDTO);
        }
        public int RegisterOrder(SupplierOrderDTO orderDTO)
        {
            return _supplierOrderService.RegisterOrder(orderDTO);
        }

        public List<SupplyCategoryDTO> GetAllSupplyCategories()
        {
            return _supplierOrderService.GetAllSupplyCategories();
        }

        public List<SupplierDTO> GetSuppliersByCategoryName(string categoryName)
        {
            return _supplierOrderService.GetSuppliersByCategoryName(categoryName);
        }


        public List<SupplyDTO> GetSuppliesBySupplier(int supplierId)
        {
            return _supplierOrderService.GetSuppliesBySupplier(supplierId);
        }
    }
}