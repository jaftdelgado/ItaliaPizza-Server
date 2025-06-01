using Model;
using Services.SupplyServices;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class SupplierService : ISupplierManager
    {
        private readonly ISupplierDAO _supplierDAO;

        public SupplierService() : this(new SupplierDAO()) { }

        public SupplierService(ISupplierDAO supplierDAO)
        {
            _supplierDAO = supplierDAO;
        }

        public List<SupplierDTO> GetAllSuppliers()
        {
            var suppliers = _supplierDAO.GetAllSuppliers();

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
            return _supplierDAO.GetSuppliersByCategory(categoryId)
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
                IsActive = true,
                CategorySupply = supplierDTO.CategorySupply
            };

            return _supplierDAO.AddSupplier(supplier);
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

            return _supplierDAO.UpdateSupplier(updatedSupplier);
        }

        public bool DeleteSupplier(int supplierID)
        {
            return _supplierDAO.DeleteSupplier(supplierID);
        }

        public bool ReactivateSupplier(int supplierID)
        {
            return _supplierDAO.ReactivateSupplier(supplierID);
        }

        public bool CanDeleteSupplier(int supplierId)
        {
            return _supplierDAO.CanDeleteSupplier(supplierId);
        }
    }
}
