using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ICustomerManager
    {
        [OperationContract]
        List<CustomerDTO> GetCustomers();

        [OperationContract]
        int AddCustomer(CustomerDTO customerDTO);

        [OperationContract]
        bool IsCustomerEmailAvailable(string email);

        [OperationContract]
        bool UpdateCustomer(CustomerDTO customerDTO);

        [OperationContract]
        bool DeleteCustomer(int customerID);

        [OperationContract]
        bool ReactivateCustomer(int customerID);
    }
}
