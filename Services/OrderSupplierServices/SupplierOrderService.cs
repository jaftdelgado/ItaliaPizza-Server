using Model;
using System;
using System.Linq;
using ServerUtilities;
using Services.Dtos;
using System.Collections.Generic;

namespace Services
{
    public class SupplierOrderService : ISupplierOrderManager
    {
        public List<SupplierOrderDTO> GetAllSupplierOrders()
        {
            var dao = new SupplierOrderDAO();
            var orders = dao.GetAllSupplierOrders();

            return orders.Select(o => new SupplierOrderDTO
            {
                SupplierID = o.SupplierID,
                SupplierName = o.Supplier?.SupplierName ?? "Desconocido",
                OrderedDate = o.OrderedDate,
                Delivered = o.Delivered,
                OrderFolio = o.OrderFolio,
                Total = o.Total,
                Status = o.Status,
                Items = o.SupplierOrder_Supply.Select(s => new SupplierOrderDTO.OrderItemDTO
                {
                    SupplyID = s.SupplyID,
                    Quantity = s.Quantity,
                    Subtotal = s.Total,
                    SupplyName = s.Supply?.SupplyName ?? "N/D",
                    SupplyPic = s.Supply?.SupplyPic,
                    UnitPrice = s.Supply?.Price ?? 0,
                    MeasureUnit = s.Supply?.MeasureUnit ?? 0
                }).ToList()
            }).ToList();
        }

        public int AddSupplierOrder(SupplierOrderDTO orderDTO)
        {
            var dao = new SupplierOrderDAO();

            string orderFolio = OrderFolio.GenerateUniqueFolio(
                orderDTO.OrderedDate,
                dao.FolioExists 
            );

            var supplierOrder = new SupplierOrder
            {
                SupplierID = orderDTO.SupplierID,
                OrderedDate = orderDTO.OrderedDate,
                Delivered = null,
                OrderFolio = orderFolio,
                Total = orderDTO.Total,
                Status = 0
            };

            var supplies = orderDTO.Items.Select(item => new SupplierOrder_Supply
            {
                SupplyID = item.SupplyID,
                Quantity = item.Quantity,
                Total = item.Subtotal
            }).ToList();

            return dao.AddSupplierOrder(supplierOrder, supplies);
        }

        public bool CancelSupplierOrder(int supplierOrderID)
        {
            SupplierOrderDAO dao = new SupplierOrderDAO();
            return dao.CancelSupplierOrder(supplierOrderID);
        }

    }
}
