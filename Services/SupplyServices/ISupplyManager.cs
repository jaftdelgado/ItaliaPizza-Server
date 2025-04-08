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
    }
}
