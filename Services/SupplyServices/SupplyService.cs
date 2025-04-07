using Model;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Services.SupplyServices
{
    public class SupplyService : ISupplyManager
    {
        private readonly SupplyDAO dao = new SupplyDAO();

        public List<SupplyCategoryDTO> GetAllCategories()
        {
            return dao.GetCategories()
                .Select(c => new SupplyCategoryDTO
                {
                    Id = c.SupplyCategoryID,
                    Name = c.CategoryName
                }).ToList();
        }

        public List<SupplierDTO> GetSuppliersByCategory(int categoryId)
        {
            return dao.GetSuppliersByCategory(categoryId)
                .Select(s => new SupplierDTO
                {
                    Id = s.SupplierID,
                    ContactName = s.ContactName,
                    PhoneNumber = s.PhoneNumber
                }).ToList();
        }

        public List<SupplyDTO> GetSuppliesBySupplier(int supplierId)
        {
            return dao.GetSuppliesBySupplier(supplierId)
                .Select(s => new SupplyDTO
                {
                    Id = s.SupplyID,
                    Name = s.SupplyName,
                    Price = s.Price,
                    MeasureUnit = s.MeasureUnit
                }).ToList();
        }
    }
}
