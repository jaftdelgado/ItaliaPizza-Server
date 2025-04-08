using Services.Dtos;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ICustomerManager
    {
        [OperationContract]
        int AddCustomer(CustomerDTO customerDTO);

        [OperationContract]
        bool IsCustomerEmailAvailable(string email);
    }
}
