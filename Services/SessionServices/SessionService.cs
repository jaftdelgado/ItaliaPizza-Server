using Services.Dtos;

namespace Services
{
    class SessionService : ISessionManager
    {
        public PersonalDTO SignIn(string username, string password)
        {
            SessionDAO sesionDAO = new SessionDAO();
            PersonalDTO personal = sesionDAO.SignIn(username, password);

            if (personal != null) return personal;

            else return null;
        }

        public int UpdateActivity(int personalID)
        {
            SessionDAO sesionDAO = new SessionDAO();
            int result = sesionDAO.UpdateActivity(personalID);
            return result;
        }

        public int SignOut(int personalID)
        {
            SessionDAO sessionDAO = new SessionDAO();
            return sessionDAO.SignOut(personalID);
        }

    }
}
