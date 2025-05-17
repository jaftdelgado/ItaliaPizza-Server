using Model;
using Services.Dtos;
using System;
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

        public List<SupplyDTO> GetSuppliesAvailableByCategory(int categoryId, int? supplierId)
        {
            return dao.GetSuppliesAvailableByCategory(categoryId, supplierId)
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

        public List<SupplyDTO> GetAllSupplies()
        {
            var supplies = dao.GetAllSupplies();
            List<SupplyDTO> supplyList = new List<SupplyDTO>();
            foreach (var supply in supplies)
            {
                supplyList.Add(new SupplyDTO
                {
                    Id = supply.SupplyID,
                    Name = supply.SupplyName,
                    Price = supply.Price,
                    MeasureUnit = supply.MeasureUnit,
                    Brand = supply.Brand,
                    SupplyPic = supply.SupplyPic,
                    Description = supply.Description,
                    IsActive = supply.IsActive,
                    SupplyCategoryID = supply.SupplyCategoryID,
                    SupplierID = supply.SupplierID,
                    SupplierName = supply.Supplier?.SupplierName
                });
            }
            return supplyList;
        }

        public List<SupplyDTO> GetAllSuppliesPage(int pageNumber, int pageSize)
        {
            var supplies = dao.GetAllSupplies();
            List<SupplyDTO> supplyList = new List<SupplyDTO>();
            foreach (var supply in supplies.OrderBy(s => s.SupplyID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize))
            {
                supplyList.Add(new SupplyDTO
                {
                    Id = supply.SupplyID,
                    Name = supply.SupplyName,
                    Price = supply.Price,
                    MeasureUnit = supply.MeasureUnit,
                    Brand = supply.Brand,
                    SupplyPic = supply.SupplyPic,
                    Description = supply.Description,
                    IsActive = supply.IsActive,
                    SupplyCategoryID = supply.SupplyCategoryID,
                    SupplierID = supply.SupplierID,
                    SupplierName = supply.Supplier?.SupplierName
                });
            }
            return supplyList;
        }

        public int AddSupply(SupplyDTO supplyDTO)
        {
            var supply = new Supply
            {
                SupplyName = supplyDTO.Name,
                Price = supplyDTO.Price,
                MeasureUnit = supplyDTO.MeasureUnit,
                SupplyCategoryID = supplyDTO.SupplyCategoryID,
                Brand = supplyDTO.Brand,
                Stock = 0,
                IsActive = true,
                SupplyPic = supplyDTO.SupplyPic,
                Description = supplyDTO.Description
            };

            SupplyDAO supplyDAO = new SupplyDAO();
            int result = supplyDAO.AddSupply(supply);
            return result;
        }

        public bool UpdateSupply(SupplyDTO supplyDTO)
        {
            var updatedSupply = new Supply
            {
                SupplyID = supplyDTO.Id,
                SupplyName = supplyDTO.Name,
                Price = supplyDTO.Price,
                MeasureUnit = supplyDTO.MeasureUnit,
                Brand = supplyDTO.Brand,
                SupplyPic = supplyDTO.SupplyPic,
                Description = supplyDTO.Description,
                SupplyCategoryID = supplyDTO.SupplyCategoryID
            };

            return dao.UpdateSupply(updatedSupply);
        }

        public bool DeleteSupply(int supplyID)
        {
            SupplyDAO supplyDAO = new SupplyDAO();
            return supplyDAO.DeleteSupply(supplyID);
        }

        public bool ReactivateSupply(int supplyID)
        {
            SupplyDAO supplyDAO = new SupplyDAO();
            return supplyDAO.ReactivateSupply(supplyID);
        }

        public bool AssignSupplierToSupply(List<int> supplyIds, int supplierId)
        {
            return dao.AssignSupplierToSupply(supplyIds, supplierId);
        }

        public bool UnassignSupplierFromSupply(List<int> supplyIds, int supplierId)
        {
            return dao.UnassignSupplierFromSupply(supplyIds, supplierId);
        }

    }
}
