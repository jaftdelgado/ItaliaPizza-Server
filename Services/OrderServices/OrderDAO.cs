using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.OrderServices
{
    public class OrderDAO
    {
        public bool RegisterOrderPayment(OrderDTO dto)
        {
            using (var context = new italiapizzaEntities())
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderID == dto.OrderID);

                    if (order == null)
                        throw new InvalidOperationException("Orden no encontrada.");

                    if (order.Status != "Entregada")
                        throw new InvalidOperationException("La orden no tiene estado 'Entregada'.");

                    var estadoPagado = context.OrderStates.FirstOrDefault(s => s.Status == "Pagada");

                    if (estadoPagado == null)
                        throw new InvalidOperationException("El estado 'Pagada' no está registrado en la tabla OrderStates.");

                    order.Status = "Pagada";
                    order.IDState = estadoPagado.StateID;

                    context.SaveChanges();
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cambiar el estado de la orden: " + ex.Message);

                    Exception inner = ex;
                    while (inner.InnerException != null)
                        inner = inner.InnerException;

                    Console.WriteLine("Exception más interna: " + inner.Message);

                    tx.Rollback();
                    return false;
                }
            }
        }

        public List<OrderSummaryDTO> GetDeliveredOrders()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Orders
                    .Where(o => o.Status == "Entregada")
                    .Select(o => new OrderSummaryDTO
                    {
                        OrderID = o.OrderID,
                        OrderDate = o.OrderDate ?? DateTime.MinValue,
                        Total = o.Total ?? 0m,
                        Products = o.Product_Order.Select(po => new OrderItemSummaryDTO
                        {
                            Product = po.Product.Name,
                            Quantity = po.Quantity ?? 0
                        }).ToList()
                    })
                    .ToList();
            }
        }
    }
}
