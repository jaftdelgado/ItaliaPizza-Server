using Services.Dtos;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IPersonalManager
    {
        [OperationContract]
        int AddPersonal(PersonalDTO personalDTO);
    }
}
