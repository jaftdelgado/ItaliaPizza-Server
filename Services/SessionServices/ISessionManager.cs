using Services.Dtos;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ISessionManager
    {
        [OperationContract]
        PersonalDTO SignIn(string username, string password);

        [OperationContract]
        int UpdateActivity(int personalID);

        [OperationContract]
        int SignOut(int personalID);
    }
}
