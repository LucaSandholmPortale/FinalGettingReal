using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GettingReal
{
    class Admin
    {
        private static string connectionsString =
            "Server=EALSQL1.eal.local; Database = DB2017_C03; User Id = user_C03; PassWord=SesamLukOp_03;";

        public bool CheckUsernameAndPassword(string userName, string password)
        {
            string userNameReceived = string.Empty, passwordReceived = string.Empty;
            bool validateLogin = false;

            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();

                    SqlCommand admin = new SqlCommand("spCheckCredentials", kNumberDB);
                    admin.CommandType = CommandType.StoredProcedure;
                    admin.Parameters.Add(new SqlParameter("@SentInUserName", userName));
                    admin.Parameters.Add(new SqlParameter("@SentInPassword", password));

                    SqlDataReader receivedUsernameAndPassword = admin.ExecuteReader();
                    while (receivedUsernameAndPassword.Read())
                    {
                        userNameReceived = receivedUsernameAndPassword.GetString(0);
                        passwordReceived = receivedUsernameAndPassword.GetString(1);
                    }
                    if (userName == userNameReceived)
                    {
                        if (password == passwordReceived)
                        {
                            validateLogin = true;
                            return validateLogin;
                        }
                        else
                        {
                            return validateLogin;
                        }
                    }
                    else
                    {
                        return validateLogin;
                    }
                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                    return validateLogin;
                }
            }
        }

        public bool ChangePasswordInDB(string userName, string newPassword)
        {
            bool isPasswordUpdated = false;
            string hasPasswordUpdated = string.Empty;
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();

                    SqlCommand changePassword = new SqlCommand("spChangeAdminPassword", kNumberDB);
                    changePassword.CommandType = CommandType.StoredProcedure;
                    changePassword.Parameters.Add(new SqlParameter("@Username", userName));
                    changePassword.Parameters.Add(new SqlParameter("@UpdatePassword", newPassword));

                    changePassword.ExecuteNonQuery();

                    SqlCommand getPassword = new SqlCommand("spGetPassword", kNumberDB);
                    getPassword.CommandType = CommandType.StoredProcedure;
                    getPassword.Parameters.Add(new SqlParameter("@userName", userName));

                    SqlDataReader reader = getPassword.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            hasPasswordUpdated = reader["Pass"].ToString();
                        }
                    }
                    if (newPassword == hasPasswordUpdated)
                    {
                        isPasswordUpdated = true;
                        return isPasswordUpdated;
                    }
                    else
                    {
                        isPasswordUpdated = false;
                        return isPasswordUpdated;
                    }

                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                    return isPasswordUpdated;
                }
            }
        }
        public string HasPasswordBeenUpdated(bool changeAdminPassword)
        {
            string hasPasswordBeenUpdated = string.Empty;

            if (changeAdminPassword == true)
            {
                hasPasswordBeenUpdated = ("Password has been changed");
                return hasPasswordBeenUpdated;
            }
            else
            {
                hasPasswordBeenUpdated = ("Password has not been changed, try again");
                return hasPasswordBeenUpdated;
            }
        }
    }
}
