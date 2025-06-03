using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IPersonalManager
    {
        [OperationContract]
        List<PersonalDTO> GetAllPersonals();

        [OperationContract]
        List<DeliveryDriverDTO> GetDeliveryDrivers();

        [OperationContract]
        int AddPersonal(PersonalDTO personalDTO);

        [OperationContract]
        bool UpdatePersonal(PersonalDTO personalDTO);

        [OperationContract]
        bool DeletePersonal(int personalID);

        [OperationContract]
        bool ReactivatePersonal(int personalID);

        [OperationContract]
        bool IsUsernameAvailable(string username);

        [OperationContract]
        bool IsRfcUnique(string rfc);

        [OperationContract]
        bool IsPersonalEmailAvailable(string email);
    }
}
