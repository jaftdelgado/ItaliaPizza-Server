using Services.Dtos;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;
using Services;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MainService : IMainManager
    {
        private readonly ICustomerManager _customerService;
        private readonly ISupplyManager _supplyService;
        private readonly ISupplierManager _supplierService;
        private readonly IPersonalManager _personalService;
        private readonly IProductManager _productService;
        private readonly IFinanceManager _financeService;
        private readonly ISupplierOrderManager _supplierOrderService;
        private readonly ISupplyManager _supplyService = new SupplyService();
        private readonly IOrderManager _orderService;
        private readonly IRecipeManager _recipeService;

        public MainService()
        {
            _customerService = new CustomerService();
            _supplyService = new SupplyService();
            _supplierService = new SupplierService();
            _personalService = new PersonalService();
            _productService = new ProductService();
            _financeService = new FinanceService();
            _supplierOrderService = new SupplierOrderService();
            _orderService = new OrderService();
            _recipeService = new RecipeService();
        }

        public bool Ping()
        {
            Console.WriteLine("Ping received. The server is available.");
            return true;
        }

        public int AddCustomer(CustomerDTO customer) => _customerService.AddCustomer(customer);
        public bool IsCustomerEmailAvailable(string email) => _customerService.IsCustomerEmailAvailable(email);


        public int AddProduct(ProductDTO productDTO) => _productService.AddProduct(productDTO);

        #region Personal
        public int AddPersonal(PersonalDTO personalDTO) => _personalService.AddPersonal(personalDTO);

        public bool IsUsernameAvailable(string username) => _personalService.IsUsernameAvailable(username);

        public bool IsRfcUnique(string rfc) => _personalService.IsRfcUnique(rfc);

        public bool IsEmailAvailable(string email) => _personalService.IsEmailAvailable(email);
        #endregion

        #region Supply
        public List<SupplyCategoryDTO> GetAllCategories() => _supplyService.GetAllCategories();

        public List<SupplierDTO> GetSuppliersByCategory(int categoryId) => _supplyService.GetSuppliersByCategory(categoryId);

        public List<SupplyDTO> GetSuppliesBySupplier(int supplierId) => _supplyService.GetSuppliesBySupplier(supplierId);

        public LinkedList<SupplyDTO> GetAllSupplies() => _supplyService.GetAllSupplies();
        #endregion

        #region Order
        public int RegisterOrder(SupplierOrderDTO dto) => _supplierOrderService.RegisterOrder(dto);

        public bool RegisterOrderPayment(OrderDTO dto) => _orderService.RegisterOrderPayment(dto);

        public List<OrderSummaryDTO> GetDeliveredOrders() => _orderService.GetDeliveredOrders();
        #endregion

        #region Recipe
        public int RegisterRecipe(RecipeDTO recipe, List<RecipeSupplyDTO> supplies) => _recipeService.RegisterRecipe(recipe, supplies);
        #endregion
    }
}