using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DTO_PladsOverblik;


namespace GettingReal
{
    class Controller
    {
        Tildeling tildeling = new Tildeling();
        Overblik overblik = new Overblik();
        Admin admin = new Admin();

        public List<string> ShowKNumberList()
        {
            return overblik.ShowKNumberList();
        }

        public string GetFirstAvailableKNumber(int medarbejder_ID)
        {
            return tildeling.GetFirstAvailableKNumber(medarbejder_ID);
        }

        public string ReleaseKNumberInDB(string kNumberToBeReleased)
        {
            return tildeling.ReleaseKNumberInDB(kNumberToBeReleased);
        }

        public bool CheckUsernameAndPassword(string userName, string password)
        {
            return admin.CheckUsernameAndPassword(userName, password);
        }

        public bool ChangePasswordInDB(string userName, string newPassword)
        {
            return admin.ChangePasswordInDB(userName, newPassword);
        }

        public string HasPasswordBeenUpdated(bool changeAdminPassword)
        {
            return admin.HasPasswordBeenUpdated(changeAdminPassword);
        }

        public int RequestDesiredKNumber(string desiredKNumber, int employee_ID)
        {
            return tildeling.SpRequestDesiredKNumber(desiredKNumber, employee_ID);
        }

        public List<string> ShowSeatingList()
        {
            return overblik.SpSeatingList();
        }
    } 
}
