using Model;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class SupplierService : ISupplierManager
    {
        public List<SupplierDTO> GetAllSuppliers()
        {
            var dao = new SupplierDAO();
            var suppliers = dao.GetAllSuppliers();

            return suppliers.Select(s => new SupplierDTO
            {
                Id = s.SupplierID,
                SupplierName = s.SupplierName,
                ContactName = s.ContactName,
                PhoneNumber = s.PhoneNumber,
                EmailAddress = s.EmailAddress,
                Description = s.Description,
                CategorySupply = s.CategorySupply
            }).ToList();
        }

        public int AddSupplier(SupplierDTO supplierDTO)
        {
            var supplier = new Supplier
            {
                SupplierName = supplierDTO.SupplierName,
                ContactName = supplierDTO.ContactName,
                PhoneNumber = supplierDTO.PhoneNumber,
                EmailAddress = supplierDTO.EmailAddress,
                Description = supplierDTO.Description,
                CategorySupply = supplierDTO.CategorySupply
            };

            SupplierDAO supplierDAO = new SupplierDAO();
            int result = supplierDAO.AddSupplier(supplier);
            return result;
        }

    }
}
