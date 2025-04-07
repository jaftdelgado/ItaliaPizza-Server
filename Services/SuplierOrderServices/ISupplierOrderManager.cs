using Services.Dtos;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ISupplierOrderManager
    {
        [OperationContract]
        int RegisterOrder(SupplierOrderDTO orderDTO);

        [OperationContract]
        List<SupplyCategoryDTO> GetAllSupplyCategories();

        [OperationContract]
        List<SupplierDTO> GetSuppliersByCategoryName(string categoryName);

        [OperationContract]
        List<SupplyDTO> GetSuppliesBySupplier(int supplierId);
    }
}
