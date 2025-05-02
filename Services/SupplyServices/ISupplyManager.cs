using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services.SupplyServices
{
    [ServiceContract]
    public interface ISupplyManager
    {
        [OperationContract]
        List<SupplyCategoryDTO> GetAllCategories();

        [OperationContract]
        List<SupplierDTO> GetSuppliersByCategory(int categoryId);

        [OperationContract]
        List<SupplyDTO> GetSuppliesBySupplier(int supplierId);
        
        [OperationContract]
        List<SupplyDTO> GetSuppliesAvailableByCategory(int categoryId);

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
    }
}
