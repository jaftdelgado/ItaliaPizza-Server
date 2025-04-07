using Services.Dtos;
using System.ServiceModel;

namespace Services.CustomerServices
{
    [ServiceContract]
    public interface ICustomerManager
    {
        [OperationContract]
        int RegisterCustomer(CustomerDTO customerDTO);
    }
}
