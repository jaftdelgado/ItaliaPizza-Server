﻿using Model;
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

        public List<SupplyDTO> GetSuppliesAvailableByCategory(int categoryId)
        {
            return dao.GetSuppliesAvailableByCategory(categoryId)
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
                    SupplyCategoryID = supply.SupplyCategoryID,
                    SupplierID = supply.SupplierID
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
                SupplyPic = supplyDTO.SupplyPic,
                Description = supplyDTO.Description
            };

            SupplyDAO supplyDAO = new SupplyDAO();
            int result = supplyDAO.AddSupply(supply);
            return result;
        }

    }
}
