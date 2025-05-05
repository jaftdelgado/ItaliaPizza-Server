using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class SesionService : ISesionManager
    {
        PersonalDTO ISesionManager.Login(string username, string password)
        {
            SesionDAO sesionDAO = new SesionDAO();
            PersonalDTO personal = sesionDAO.Login(username, password);
            if (personal != null)
            {
                return personal;
            }
            else
            {
                return null;
            }
        }
        int ISesionManager.updateActivity(int personalID)
        {
            SesionDAO sesionDAO = new SesionDAO();
            int result = sesionDAO.UpdateActivity(personalID);
            return result;
        }
    }
}
