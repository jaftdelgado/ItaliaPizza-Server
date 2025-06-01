using Services.Dtos;
using Services;

public class SessionService : ISessionManager
{
    private readonly ISessionDAO _dao;

    public SessionService() : this(new SessionDAO()) { }

    public SessionService(ISessionDAO dao)
    {
        _dao = dao;
    }

    public PersonalDTO SignIn(string username, string password)
    {
        return _dao.SignIn(username, password);
    }

    public int UpdateActivity(int personalID)
    {
        return _dao.UpdateActivity(personalID);
    }

    public int SignOut(int personalID)
    {
        return _dao.SignOut(personalID);
    }
}
