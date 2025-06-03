using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;
using Services.SupplyServices;
using Services.FinanceServices;
using Services.OrderServices;
using Services.WasteServices;

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
        private readonly ISessionManager _sessionService;
        private readonly IWasteManager _wasteService;

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
            _sessionService = new SessionService();
            _wasteService = new WasteService();
        }

        public bool Ping()
        {
            return true;
        }

        #region Customer
        public int AddCustomer(CustomerDTO customer) => _customerService.AddCustomer(customer);
        public bool IsCustomerEmailAvailable(string email) => _customerService.IsCustomerEmailAvailable(email);

        public List<CustomerDTO> GetCustomers() => _customerService.GetCustomers();
        public bool UpdateCustomer(CustomerDTO customerDTO) => _customerService.UpdateCustomer(customerDTO);
        public bool DeleteCustomer(int customerID) => _customerService.DeleteCustomer(customerID);
        public bool ReactivateCustomer(int customerID) => _customerService.ReactivateCustomer(customerID);
        public List<CustomerDTO> GetActiveCustomers() => _customerService.GetActiveCustomers();
        #endregion

        #region Product
        public int AddProduct(ProductDTO productDTO) => _productService.AddProduct(productDTO);
        public bool UpdateProduct(ProductDTO productDTO) => _productService.UpdateProduct(productDTO);
        public List<ProductDTO> GetAllProducts(bool activeOnly = false) => _productService.GetAllProducts(activeOnly);
        public bool DeleteProduct(int productId) => _productService.DeleteProduct(productId);
        public bool ReactivateProduct(int productId) => _productService.ReactivateProduct(productId);
        public bool IsProductDeletable(int productId) => _productService.IsProductDeletable(productId);
        #endregion

        #region Personal
        public List<PersonalDTO> GetAllPersonals() => _personalService.GetAllPersonals();

        public List<DeliveryDriverDTO> GetDeliveryDrivers() => _personalService.GetDeliveryDrivers();
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
        public List<SupplyDTO> GetAllSupplies(bool activeOnly = false) => _supplyService.GetAllSupplies(activeOnly = false);
        public List<SupplyDTO> GetAllSuppliesPage(int pageNumber, int pageSize) => _supplyService.GetAllSuppliesPage(pageNumber, pageSize);
        public int AddSupply(SupplyDTO supplyDTO) => _supplyService.AddSupply(supplyDTO);
        public bool UpdateSupply(SupplyDTO supplyDTO) => _supplyService.UpdateSupply(supplyDTO);
        public bool DeleteSupply(int supplyID) => _supplyService.DeleteSupply(supplyID);
        public bool ReactivateSupply(int supplyID) => _supplyService.ReactivateSupply(supplyID);
        public bool AssignSupplierToSupply(List<int> supplyIds, int supplierId) => _supplyService.AssignSupplierToSupply(supplyIds, supplierId);
        public bool UnassignSupplierFromSupply(List<int> supplyIds, int supplierId) => _supplyService.UnassignSupplierFromSupply(supplyIds, supplierId);
        public List<RecipeSupplyDTO> GetSuppliesByRecipe(int recipeId) => _supplyService.GetSuppliesByRecipe(recipeId);
        public bool IsSupplyDeletable(int supplyId) => _supplyService.IsSupplyDeletable(supplyId);
        #endregion

        #region Supplier
        public List<SupplierDTO> GetAllSuppliers() => _supplierService.GetAllSuppliers();
        public List<SupplierDTO> GetSuppliersByCategory(int categoryId) => _supplierService.GetSuppliersByCategory(categoryId);
        public int AddSupplier(SupplierDTO supplierDTO) => _supplierService.AddSupplier(supplierDTO);
        public bool UpdateSupplier(SupplierDTO supplierDTO) => _supplierService.UpdateSupplier(supplierDTO);
        public bool DeleteSupplier(int supplierID) => _supplierService.DeleteSupplier(supplierID);
        public bool ReactivateSupplier(int supplierID) => _supplierService.ReactivateSupplier(supplierID);

        public bool CanDeleteSupplier(int supplierId) => _supplierService.CanDeleteSupplier(supplierId);
        #endregion

        #region Finance
        public List<TransactionDTO> GetCurrentTransactions() => _financeService.GetCurrentTransactions();
        public bool RegisterOrderPayment(int orderId) => _financeService.RegisterOrderPayment(orderId);
        public CashRegisterDTO GetOpenCashRegisterInfo() => _financeService.GetOpenCashRegisterInfo();
        public bool OpenCashRegister(decimal initialAmout) => _financeService.OpenCashRegister(initialAmout);
        public bool CloseCashRegister(decimal cashierAmount) => _financeService.CloseCashRegister(cashierAmount);
        public int RegisterCashOut(decimal amount, string description) => _financeService.RegisterCashOut(amount, description);
        public int RegisterSupplierOrderExpense(int supplierOrderID) => _financeService.RegisterSupplierOrderExpense(supplierOrderID);

        public bool HasOpenCashRegister() => _financeService.HasOpenCashRegister();
        #endregion
        
        #region OrderSupplier
        public List<SupplierOrderDTO> GetAllSupplierOrders() => _supplierOrderService.GetAllSupplierOrders();
        public int AddSupplierOrder(SupplierOrderDTO orderDTO) => _supplierOrderService.AddSupplierOrder(orderDTO);
        public bool UpdateSupplierOrder(SupplierOrderDTO orderDTO) => _supplierOrderService.UpdateSupplierOrder(orderDTO);
        public bool DeliverOrder(int supplierOrderID) => _supplierOrderService.DeliverOrder(supplierOrderID);
        public bool CancelSupplierOrder(int supplierOrderID) => _supplierOrderService.CancelSupplierOrder(supplierOrderID);
        #endregion

        #region Orders
        public List<OrderDTO> GetOrders(List<int> statusList, bool includeLocal, bool includeDelivery) => _orderService.GetOrders(statusList, includeLocal, includeDelivery);

        public bool ChangeOrderStatus(int orderId, int newStatus, int roleId) => _orderService.ChangeOrderStatus(orderId, newStatus, roleId);

        public int AddLocalOrder(OrderDTO orderDTO) => _orderService.AddLocalOrder(orderDTO);

        public int AddDeliveryOrder(OrderDTO orderDTO, DeliveryDTO deliveryDTO) => _orderService.AddDeliveryOrder(orderDTO, deliveryDTO);
        #endregion

        #region Recipe
        public List<ProductDTO> GetProductsWithRecipe(bool includeSteps = false) => _recipeService.GetProductsWithRecipe(includeSteps);

        public int AddRecipe(RecipeDTO recipeDTO) => _recipeService.AddRecipe(recipeDTO);

        public bool UpdateRecipe(RecipeDTO recipeDTO) => _recipeService.UpdateRecipe(recipeDTO);

        public bool DeleteRecipe(int recipeId) => _recipeService.DeleteRecipe(recipeId);
        #endregion

        #region Session
        public PersonalDTO SignIn(string username, string password) => _sessionService.SignIn(username, password);
        public int UpdateActivity(int personalID) => _sessionService.UpdateActivity(personalID);
        public int SignOut(int personalID) => _sessionService.SignOut(personalID);
        #endregion

        #region Waste
        public bool RegisterSupplyLoss(SupplyDTO supplyDTO) => _wasteService.RegisterSupplyLoss(supplyDTO);
        #endregion
    }
}