using Model;
using Services.Dtos;
using Services.SupplyServices;

namespace Services.WasteServices
{
    public class WasteService : IWasteManager
    {
        private readonly IWasteDAO _dao;

        public WasteService() : this(new WasteDAO()) { }

        public WasteService(IWasteDAO dao)
        {
            _dao = dao;
        }
        public bool RegisterSupplyLoss(SupplyDTO supplyDTO)
        {
            var updateSupplyStock = new Supply
            {
                SupplyID = supplyDTO.Id,
                Stock = supplyDTO.Stock
            };

            var _dao = new WasteDAO();
            return _dao.RegisterSupplyLoss(updateSupplyStock);
        }
    }
}
