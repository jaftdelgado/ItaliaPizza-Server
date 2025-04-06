using Model;
using Model.DAO;
using Services.Dtos;
using System;
using System.Collections.Generic;

namespace Services
{
    public class SupplierOrderService : ISupplierOrderManager
    {
        public int RegisterOrder(SupplierOrderDTO orderDTO)
        {
            var order = new Supplier_Order
            {
                SupplierID = orderDTO.SupplierID,
                SupplyID = orderDTO.SupplyID,
                OrderedDate = orderDTO.OrderedDate,
                Quantity = orderDTO.Quantity,
                Status = orderDTO.Status
            };

            SupplierOrderDAO dao = new SupplierOrderDAO();
            return dao.AddOrder(order);
        }

        public List<SupplyCategoryDTO> GetAllSupplyCategories()
        {
            var dao = new SupplyCategoryDAO();
            var categories = dao.GetAll();
            var list = new List<SupplyCategoryDTO>();

            foreach (var category in categories)
            {
                list.Add(new SupplyCategoryDTO
                {
                    SupplyCategoryID = category.SupplyCategoryID,
                    CategoryName = category.CategoryName
                });
            }

            return list;
        }

        public List<SupplierDTO> GetSuppliersByCategoryName(string categoryName)
        {
            var dao = new SupplierDAO();
            var suppliers = dao.GetByCategoryName(categoryName);
            var list = new List<SupplierDTO>();

            foreach (var s in suppliers)
            {
                list.Add(new SupplierDTO
                {
                    SupplierID = s.SupplierID,
                    ContactName = s.ContactName,
                    PhoneNumber = s.PhoneNumber,
                    CategorySupply = s.CategorySupply // Puedes eliminar si ya no lo usas
                });
            }

            return list;
        }


        public List<SupplyDTO> GetSuppliesBySupplier(int supplierId)
        {
            var dao = new SupplyDAO();
            var supplies = dao.GetBySupplier(supplierId);
            var list = new List<SupplyDTO>();

            foreach (var s in supplies)
            {
                list.Add(new SupplyDTO
                {
                    SupplyID = s.SupplyID,
                    SupplyName = s.SupplyName,
                    Brand = s.Brand,
                    Price = s.Price,
                    Stock = s.Stock,
                    MeasureUnit = s.MeasureUnit,
                    SupplierID = s.SupplierID,
                    SupplyCategoryID = s.SupplyCategoryID
                });
            }

            return list;
        }
    }
}

