using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ISupplierManager
    {
        [OperationContract]
        List<SupplierDTO> GetAllSuppliers();

        [OperationContract]
        int AddSupplier(SupplierDTO supplierDTO);

        [OperationContract]
        bool UpdateSupplier(SupplierDTO supplierDTO);

        [OperationContract]
        bool DeleteSupplier(int supplierID);

        [OperationContract]
        bool ReactivateSupplier(int supplierID);
    }
}
