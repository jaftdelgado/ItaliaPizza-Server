using Services.Dtos;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;
using Services;
using Services.OrderServices;

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

        #region Customer
        public int AddCustomer(CustomerDTO customer) => _customerService.AddCustomer(customer);
        public bool IsCustomerEmailAvailable(string email) => _customerService.IsCustomerEmailAvailable(email);
        #endregion

        public ProductDTO AddProduct(ProductDTO productDTO) => _productService.AddProduct(productDTO);

        #region Personal
        public List<PersonalDTO> GetAllPersonals() => _personalService.GetAllPersonals();
        public int AddPersonal(PersonalDTO personalDTO) => _personalService.AddPersonal(personalDTO);
        public bool UpdatePersonal(PersonalDTO personalDTO) => _personalService.UpdatePersonal(personalDTO);
        public bool DeletePersonal(int personalID) => _personalService.DeletePersonal(personalID);
        public bool ReactivatePersonal(int personalID) => _personalService.ReactivatePersonal(personalID);
        public bool IsUsernameAvailable(string username) => _personalService.IsUsernameAvailable(username);
        public bool IsRfcUnique(string rfc) => _personalService.IsRfcUnique(rfc);
        public bool IsPersonalEmailAvailable(string email) => _personalService.IsPersonalEmailAvailable(email);
        #endregion

        #region Supply
        public List<SupplyCategoryDTO> GetAllCategories() => _supplyService.GetAllCategories();

        public List<SupplierDTO> GetSuppliersByCategory(int categoryId) => _supplyService.GetSuppliersByCategory(categoryId);

        public List<SupplyDTO> GetSuppliesBySupplier(int supplierId) => _supplyService.GetSuppliesBySupplier(supplierId);

        public LinkedList<SupplyDTO> GetAllSupplies() => _supplyService.GetAllSupplies();
        #endregion

        #region Supplier
        public List<SupplierDTO> GetAllSuppliers() => _supplierService.GetAllSuppliers();
        public int AddSupplier(SupplierDTO supplierDTO) => _supplierService.AddSupplier(supplierDTO);
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