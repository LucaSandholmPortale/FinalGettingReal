using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GettingReal
{
    class Tildeling
    {

        private static string connectionsString =
        "Server=EALSQL1.eal.local; Database = DB2017_C03; User Id = user_C03; PassWord=SesamLukOp_03;";

        public string GetFirstAvailableKNumber(int medarbejder_ID)
        {
            string kNumber = string.Empty;
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();
                    SqlCommand tildeling = new SqlCommand("spuGivFørsteKNummer", kNumberDB);
                    tildeling.CommandType = CommandType.StoredProcedure;

                    tildeling.Parameters.Add(new SqlParameter("@MEDARBEJDER_ID", medarbejder_ID));

                    SqlDataReader givKnummerIkkeIBrug = tildeling.ExecuteReader();

                    kNumber = givKnummerIkkeIBrug["@KNUMMER"].ToString();

                    return kNumber;
                }
                catch
                {
                    return kNumber;
                }
            }
        }


        public int SpRequestDesiredKNumber(string desiredKNumber, int employee_ID)
        {
            int isKNumberTaken = 0;
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();
                    SqlCommand wishKNumber = new SqlCommand("spWishKNumber", kNumberDB);
                    wishKNumber.CommandType = CommandType.StoredProcedure;
                    wishKNumber.Parameters.Add(new SqlParameter("@MEDARBEJDER_ID_1", employee_ID));
                    wishKNumber.Parameters.Add(new SqlParameter("@KNUMMER_1", desiredKNumber));

                    SqlDataReader receivedMedarbejder_IDAndKnummer = wishKNumber.ExecuteReader();
                    while (receivedMedarbejder_IDAndKnummer.Read())
                    {
                        employee_ID = Convert.ToInt32(receivedMedarbejder_IDAndKnummer.GetString(0));
                        desiredKNumber = receivedMedarbejder_IDAndKnummer.GetString(1);
                        isKNumberTaken = Convert.ToInt32(receivedMedarbejder_IDAndKnummer["@KNumberOccupied"]);
                    }
                    return isKNumberTaken;
                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                    return 0;
                }

            }
        }

        public string ReleaseKNumberInDB(string kNumberToBeReleased)
        {
            string isKNumberFree = string.Empty;
            bool hasKNumberBeenReleased = false;
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();
                    SqlCommand releaseKNumber = new SqlCommand("spReleaseKNumber", kNumberDB);
                    releaseKNumber.CommandType = CommandType.StoredProcedure;
                    releaseKNumber.Parameters.Add(new SqlParameter("@kNumberToBeReleased", kNumberToBeReleased));

                    SqlDataReader releasedKNumber = releaseKNumber.ExecuteReader();
                    while (releasedKNumber.Read())
                    {
                        hasKNumberBeenReleased = Convert.ToBoolean(releasedKNumber["KNUMMER_I_BRUG"]);
                    }
                    if (hasKNumberBeenReleased == true)
                    {
                        isKNumberFree = ("K-nummer blev frigjort");
                        return isKNumberFree;
                    }
                    else
                    {
                        isKNumberFree = ("K-nummer blev ikke frigjort");
                        return isKNumberFree;
                    }
                }
                catch (SqlException error)
                {
                    return ("Fejl: " + error.Message);
                }
            }
        }
    }
}
