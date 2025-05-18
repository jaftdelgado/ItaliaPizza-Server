using Services.Dtos;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;
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
        private readonly ISesionManager _sesionService;

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
            _sesionService = new SesionService();
        }

        public bool Ping()
        {
            Console.WriteLine("Ping received. The server is available.");
            return true;
        }

        #region Customer
        public int AddCustomer(CustomerDTO customer) => _customerService.AddCustomer(customer);
        public bool IsCustomerEmailAvailable(string email) => _customerService.IsCustomerEmailAvailable(email);

        public List<CustomerDTO> GetCustomers() => _customerService.GetCustomers();
        public bool UpdateCustomer(CustomerDTO customerDTO) => _customerService.UpdateCustomer(customerDTO);
        public bool DeleteCustomer(int customerID) => _customerService.DeleteCustomer(customerID);
        public bool ReactivateCustomer(int customerID) => _customerService.ReactivateCustomer(customerID);
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
        public List<SupplyDTO> GetSuppliesBySupplier(int supplierId) => _supplyService.GetSuppliesBySupplier(supplierId);
        public List<SupplyDTO> GetSuppliesAvailableByCategory(int categoryId, int? supplierId) => _supplyService.GetSuppliesAvailableByCategory(categoryId, supplierId);
        public List<SupplyDTO> GetAllSupplies() => _supplyService.GetAllSupplies();
        public int AddSupply(SupplyDTO supplyDTO) => _supplyService.AddSupply(supplyDTO);
        public bool UpdateSupply(SupplyDTO supplyDTO) => _supplyService.UpdateSupply(supplyDTO);
        public bool DeleteSupply(int supplyID) => _supplyService.DeleteSupply(supplyID);
        public bool ReactivateSupply(int supplyID) => _supplyService.ReactivateSupply(supplyID);
        public bool AssignSupplierToSupply(List<int> supplyIds, int supplierId) => _supplyService.AssignSupplierToSupply(supplyIds, supplierId);
        public bool UnassignSupplierFromSupply(List<int> supplyIds, int supplierId) => _supplyService.UnassignSupplierFromSupply(supplyIds, supplierId);
        public bool IsSupplyDeletable(int supplyId) => _supplyService.IsSupplyDeletable(supplyId);
        #endregion

        #region Supplier
        public List<SupplierDTO> GetAllSuppliers() => _supplierService.GetAllSuppliers();
        public List<SupplierDTO> GetSuppliersByCategory(int categoryId) => _supplierService.GetSuppliersByCategory(categoryId);
        public int AddSupplier(SupplierDTO supplierDTO) => _supplierService.AddSupplier(supplierDTO);
        public bool UpdateSupplier(SupplierDTO supplierDTO) => _supplierService.UpdateSupplier(supplierDTO);
        public bool DeleteSupplier(int supplierID) => _supplierService.DeleteSupplier(supplierID);
        public bool ReactivateSupplier(int supplierID) => _supplierService.ReactivateSupplier(supplierID);
        #endregion

        #region Finance
        public bool RegisterOrderPayment(int orderId) => _financeService.RegisterOrderPayment(orderId);
        #endregion

        #region OrderSupplier
        public List<SupplierOrderDTO> GetAllSupplierOrders() => _supplierOrderService.GetAllSupplierOrders();
        public int AddSupplierOrder(SupplierOrderDTO orderDTO) => _supplierOrderService.AddSupplierOrder(orderDTO);
        public bool UpdateSupplierOrder(SupplierOrderDTO orderDTO) => _supplierOrderService.UpdateSupplierOrder(orderDTO);
        public bool DeliverOrder(int supplierOrderID) => _supplierOrderService.DeliverOrder(supplierOrderID);
        public bool CancelSupplierOrder(int supplierOrderID) => _supplierOrderService.CancelSupplierOrder(supplierOrderID);
        #endregion

        #region Orders
        public List<OrderItemSummaryDTO> GetOrderItemsByOrderID(int orderID) => _orderService.GetOrderItemsByOrderID(orderID);

        public List<OrderSummaryDTO> GetDeliveredOrders() => _orderService.GetDeliveredOrders();
        #endregion

        #region Recipe
        public int RegisterRecipe(RecipeDTO recipe, List<RecipeSupplyDTO> supplies) => _recipeService.RegisterRecipe(recipe, supplies);
        public List<RecipeDTO> GetRecipes() => _recipeService.GetRecipes();
        #endregion

        #region Session
        public PersonalDTO Login(string username, string password) => _sesionService.Login(username, password);
        public int updateActivity(int personalID) => _sesionService.updateActivity(personalID);
        #endregion
    }
}