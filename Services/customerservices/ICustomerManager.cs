using Services.Dtos;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ICustomerManager
    {
        [OperationContract]
        int RegisterCustomer(CustomerDTO customerDTO, AddressDTO addressDTO);
    }
}
