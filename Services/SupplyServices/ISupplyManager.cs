using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services.SupplyServices
{
    [ServiceContract]
    public interface ISupplyManager
    {
        [OperationContract]
        List<SupplyDTO> GetSuppliesBySupplier(int supplierId);
        
        [OperationContract]
        List<SupplyDTO> GetSuppliesAvailableByCategory(int categoryId, int? supplierId);

        [OperationContract]
        List<SupplyDTO> GetAllSupplies();

        [OperationContract]
        int AddSupply(SupplyDTO supplyDTO);

        [OperationContract]
        bool UpdateSupply(SupplyDTO supplyDTO);
        
        [OperationContract]
        bool DeleteSupply(int supplyId);
        
        [OperationContract]
        bool ReactivateSupply(int supplyId);

        [OperationContract]
        bool AssignSupplierToSupply(List<int> supplyIds, int supplierId);

        [OperationContract]
        bool UnassignSupplierFromSupply(List<int> supplyIds, int supplierId);

        [OperationContract]
        bool IsSupplyDeletable(int supplyId);
    }
}
