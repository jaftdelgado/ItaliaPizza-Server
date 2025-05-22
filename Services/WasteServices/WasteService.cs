using Model;
using Services.Dtos;
using Services.SupplyServices;

namespace Services.WasteServices
{
    public class WasteService : IWasteManager
    {
        public bool RegisterSupplyLoss(SupplyDTO supplyDTO)
        {
            var updateSupplyStock = new Supply
            {
                SupplyID = supplyDTO.Id,
                Stock = supplyDTO.Stock
            };

            var dao = new WasteDAO();
            return dao.RegisterSupplyLoss(updateSupplyStock);
        }
    }
}
