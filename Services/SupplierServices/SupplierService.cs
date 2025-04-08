using Model;

namespace Services
{
    public class SupplierService : ISupplierManager
    {
        public int RegisterSupplier(SupplierDTO supplierDTO)
        {
            var supplier = new Supplier
            {
                ContactName = supplierDTO.ContactName,
                PhoneNumber = supplierDTO.PhoneNumber,
                CategorySupply = supplierDTO.CategorySupply
            };

            SupplierDAO supplierDAO = new SupplierDAO();

            int result = supplierDAO.RegisterSupplier(supplier);
            return result;
        }
    }
}
