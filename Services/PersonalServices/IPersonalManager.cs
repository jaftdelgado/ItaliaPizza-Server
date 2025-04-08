using Services.Dtos;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IPersonalManager
    {
        [OperationContract]
        int AddPersonal(PersonalDTO personalDTO);

        [OperationContract]
        bool IsUsernameAvailable(string username);

        [OperationContract]
        bool IsRfcUnique(string rfc);

        [OperationContract]
        bool IsEmailAvailable(string email);
    }
}
