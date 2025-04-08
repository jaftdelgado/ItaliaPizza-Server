using Services.Dtos;

namespace Services
{
    public class SupplierOrderService : ISupplierOrderManager
    {
        public int RegisterOrder(SupplierOrderDTO dto)
        {
            var dao = new SupplierOrderDAO();
            return dao.RegisterSupplierOrder(dto);
        }
    }
}
