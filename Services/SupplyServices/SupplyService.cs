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
                    PhoneNumber = s.PhoneNumber,
                    CategorySupply = s.CategorySupply
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
                    MeasureUnit = s.MeasureUnit,
                    Brand = s.Brand,
                    SupplyCategoryID = s.SupplyCategoryID,
                    SupplierID = s.SupplierID
                }).ToList();
        }

        public LinkedList<SupplyDTO> GetAllSupplies()
        {
            var supplies = dao.GetAllSupplies();
            LinkedList<SupplyDTO> supplyList = new LinkedList<SupplyDTO>();
            foreach (var supply in supplies)
            {
                supplyList.AddLast(new SupplyDTO
                {
                    Id = supply.SupplyID,
                    Name = supply.SupplyName,
                    Price = supply.Price,
                    MeasureUnit = supply.MeasureUnit,
                    Brand = supply.Brand,
                    SupplyCategoryID = supply.SupplyCategoryID,
                    SupplierID = supply.SupplierID
                });
            }
            return supplyList;
        }
    }
}
