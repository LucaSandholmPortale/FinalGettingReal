using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GettingReal
{
    class Overblik
    {
        private static string connectionsString =
            "Server=EALSQL1.eal.local; Database = DB2017_C03; User Id = user_C03; PassWord=SesamLukOp_03;";

        public List<string> ShowKNumberList()
        {
            List<string> kNumberList = new List<string>();
            string kNummer, kNummer_I_Brug, medarbejder_Navn;
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();
                    SqlCommand overblik = new SqlCommand("spOverblikOverKnumre", kNumberDB);
                    overblik.CommandType = CommandType.StoredProcedure;

                    SqlDataReader showKNumber = overblik.ExecuteReader();

                    if (showKNumber.HasRows)
                    {
                        while (showKNumber.Read())
                        {
                            kNummer = showKNumber["KNUMMER"].ToString();
                            kNummer_I_Brug = showKNumber["KNUMMER_I_BRUG"].ToString();
                            medarbejder_Navn = showKNumber["MEDARBEJDER_NAVN"].ToString();
                            kNumberList.Add(kNummer + " " + kNummer_I_Brug + " " + medarbejder_Navn);
                            return kNumberList;
                        }
                    }
                }
                catch (SqlException error)
                {
                    kNumberList.Add("Fejl " + error.Message);
                    return kNumberList;
                } 
                return kNumberList;
            }
        }

        public List<string> SpSeatingList()
        {
            List<string> pladsListe = new List<string>();
            string kNummer, kNummer_I_Brug, plads, plads_I_Brug, medarbejder_Navn;
            using (SqlConnection seatListDB = new SqlConnection(connectionsString))
            {
                try
                {
                    seatListDB.Open();
                    SqlCommand overblik = new SqlCommand("spPladsOverblik",seatListDB);
                    overblik.CommandType = CommandType.StoredProcedure;

                    SqlDataReader showSeating = overblik.ExecuteReader();

                    if(showSeating.HasRows)
                    {

                        while(showSeating.Read())
                        {
                            pladsListe.Add(kNummer = showSeating["KNUMMER"].ToString());
                            pladsListe.Add(kNummer_I_Brug = showSeating["KNUMMER_I_BRUG"].ToString());
                            pladsListe.Add(plads = showSeating["PLADS"].ToString());
                            pladsListe.Add(plads_I_Brug = showSeating["PLADS_I_BRUG"].ToString());
                            pladsListe.Add(medarbejder_Navn = showSeating["MEDARBEJDER_NAVN"].ToString());
                            return pladsListe;
                        }
                    }
                }
                catch(SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                }
                return pladsListe;
            }
        }
    }
}
