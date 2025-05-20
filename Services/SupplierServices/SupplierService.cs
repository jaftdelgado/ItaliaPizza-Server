using Model;
using Services.SupplyServices;
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
                CategorySupply = s.CategorySupply,
                IsActive = s.IsActive
            }).ToList();
        }

        public List<SupplierDTO> GetSuppliersByCategory(int categoryId)
        {
            SupplierDAO dao = new SupplierDAO();

            return dao.GetSuppliersByCategory(categoryId)
                .Select(s => new SupplierDTO
                {
                    Id = s.SupplierID,
                    SupplierName = s.SupplierName,
                    ContactName = s.ContactName,
                    PhoneNumber = s.PhoneNumber,
                    CategorySupply = s.CategorySupply,
                    IsActive = s.IsActive
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
            int supplierId = supplierDAO.AddSupplier(supplier);
            return supplierId;
        }

        public bool UpdateSupplier(SupplierDTO supplierDTO)
        {
            var updatedSupplier = new Supplier
            {
                SupplierID = supplierDTO.Id,
                SupplierName = supplierDTO.SupplierName,
                ContactName = supplierDTO.ContactName,
                PhoneNumber = supplierDTO.PhoneNumber,
                EmailAddress = supplierDTO.EmailAddress,
                Description = supplierDTO.Description,
                CategorySupply = supplierDTO.CategorySupply
            };

            var dao = new SupplierDAO();
            return dao.UpdateSupplier(updatedSupplier);
        }

        public bool DeleteSupplier(int supplierID)
        {
            SupplierDAO supplierDAO = new SupplierDAO();
            return supplierDAO.DeleteSupplier(supplierID);
        }

        public bool ReactivateSupplier(int supplierID)
        {
            SupplierDAO supplierDAO = new SupplierDAO();
            return supplierDAO.ReactivateSupplier(supplierID);
        }
        public bool CanDeleteSupplier(int supplierId)
        {
            var dao = new SupplierDAO();
            return dao.CanDeleteSupplier(supplierId);
        }

    }
}
