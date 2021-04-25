using HashtilCagesMap.Entities;
using Microsoft.Maps.MapControl.WPF;
using MySql.Data.MySqlClient;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HashtilCagesMap.DataBase
{
    class DBManagerOld : DBHelperOld
    {
        string query;
        public ObservableCollection<CageAudit> cageAuditClasses = new ObservableCollection<CageAudit>();
        public ObservableCollection<CageAudit> cageauditcompare = new ObservableCollection<CageAudit>();
        public ObservableCollection<TotalCageCx> totalCageCxes = new ObservableCollection<TotalCageCx>();
        public ObservableCollection<CageAudit> cagecost = new ObservableCollection<CageAudit>();

        public  ObservableCollection<UserLogin> SelectLoginTable(string query)
        {

            MySqlCommand cmd = new MySqlCommand(query, con);
            if (con.State == 0)
            {
                con.Open();
            }
            var rdr = cmd.ExecuteReader();
            UserLogin userLogin = new UserLogin();
            ObservableCollection<UserLogin> userLogins = new ObservableCollection<UserLogin>();
            while (rdr.Read())
            {
                userLogin.UserId = Convert.ToInt32(rdr["id"]);
                userLogin.UserName = Convert.ToString(rdr["username"]);
                userLogin.UserNickName = Convert.ToString(rdr["usernickname"]);
                userLogin.UserLevel = Convert.ToInt32(rdr["userlevel"]);
                userLogins.Add(userLogin);


            }
            con.Close();
            rdr.Close();
            return userLogins;
           
        }     
      
      
        public void Crud(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, con);

                if (con.State == 0)
                {
                    con.Open();
                }



                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("פקודה לא הוכנסה! נא לפנות לקובי");
            }
        }
        public ObservableCollection<CageAudit> SelectAllTable(string table)
        {

            query = $"SELECT * FROM  {table} ;";


            MySqlCommand cmd = new MySqlCommand(query, con);
            if (con.State == 0)
            {
                con.Open();
            }

            var rdr = cmd.ExecuteReader();
            cageauditcompare.Clear();
            while (rdr.Read())
            {

                var CxCageTable = new CageAudit
                {
                    CxId = Convert.ToInt32(rdr["row_id"].ToString()),
                    Cx = Convert.ToString(rdr["cx"].ToString()),
                    Date = Convert.ToDateTime(rdr["date"].ToString()),
                    Time = Convert.ToDateTime(rdr["time"].ToString()),
                    Contractor = Convert.ToString(rdr["contractor"]),
                    User = Convert.ToString(rdr["user"].ToString()),
                    Status = Convert.ToString(rdr["status"]),
                    NumOfCage = Convert.ToInt32(rdr["num_of_cage"].ToString()),
                    Sector = Convert.ToString(rdr["sector"]),
                    //Lon = Convert.ToDouble(rdr["Lon"].ToString()),
                    // Lan = Convert.ToDouble(rdr["Lan"].ToString()),
                    //location = new Location(Convert.ToDouble(rdr["Lan"].ToString()), Convert.ToDouble(rdr["Lon"].ToString())),
                    TotalCageCost = Convert.ToInt32(rdr["costtotalcage"]),
                    CagePrice = Convert.ToInt32(rdr["cageprice"])
                    
                
                };
               
                
                cageauditcompare.Add(CxCageTable);
            }
            
            if(cageauditcompare.Count() > cageAuditClasses.Count())
            {
                foreach(var cage in cageauditcompare)
                {
                    if(cageAuditClasses.Contains(cage))
                    {
                        cageauditcompare.Remove(cage);
                    }
                }
                con.Close();
                rdr.Close();
                return cageauditcompare;
            }
            con.Close();
            rdr.Close();
            return cageAuditClasses;

        }

       


        public ObservableCollection<CageAudit> SelectAllTableCost(string table)
        {

            query = $"SELECT * FROM  {table} ;";


            MySqlCommand cmd = new MySqlCommand(query, con);
            if (con.State == 0)
            {
                con.Open();
            }

            var rdr = cmd.ExecuteReader();
            cagecost.Clear();
            while (rdr.Read())
            {

                var CxCageTable = new CageAudit
                {
                    CxId = Convert.ToInt32(rdr["row_id"].ToString()),
                    Cx = Convert.ToString(rdr["cx"].ToString()),
                    Date = Convert.ToDateTime(rdr["date"].ToString()),
                    Time = Convert.ToDateTime(rdr["time"].ToString()),
                    Contractor = Convert.ToString(rdr["contractor"]),
                    User = Convert.ToString(rdr["user"].ToString()),
                    Status = Convert.ToString(rdr["status"]),
                    NumOfCage = Convert.ToInt32(rdr["num_of_cage"].ToString()),
                    Sector = Convert.ToString(rdr["sector"]),
                    //Lon = Convert.ToDouble(rdr["Lon"].ToString()),
                    // Lan = Convert.ToDouble(rdr["Lan"].ToString()),
                    //location = new Location(Convert.ToDouble(rdr["Lan"].ToString()), Convert.ToDouble(rdr["Lon"].ToString())),
                    TotalCageCost = Convert.ToInt32(rdr["costtotalcage"]),
                    CagePrice = Convert.ToInt32(rdr["cageprice"])


                };


                cagecost.Add(CxCageTable);
            }

            con.Close();
            rdr.Close();
            return cagecost;

        }
        public ObservableCollection<TotalCageCx> SelectAllCxTable(string table)
        {
            string query = "SELECT * FROM " + table + ";";

            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();

            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var TotalCageCx = new TotalCageCx
                {
                    CxId = Convert.ToInt32(rdr["id"]),
                    CxName = Convert.ToString(rdr["cx"]),
                    TotalCages = Convert.ToInt32(rdr["total_cage"])
                };
                totalCageCxes.Add(TotalCageCx);

            }
            return totalCageCxes;
        }


    }
}
