using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Npgsql;

namespace Irci.Models
{
    public class AuthHandler
    {
        private NpgsqlConnection dbCon;
        private NpgsqlCommand dbCmd;
        public AuthHandler()
        {
            dbCon = new Connection().getConnection();
            dbCon.Open();
            dbCmd = new NpgsqlCommand();
            dbCmd.Connection = dbCon;
        }


        public string login(string email, string password)
        {
            dbCmd.Connection = dbCon;
            var idaccount = "";
            dbCmd.CommandText = "Select idaccount from new_irci.account where emailaccount = '" + email + "' and passaccount = '" + password + "' and roleaccount = 2 ";
            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    idaccount = result[0].ToString();
                    break;
                }


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return idaccount;
        }

        public int checkProfileMain(int id)
        {
            dbCmd.Connection = dbCon;
            var idaccount = 0;
            dbCmd.CommandText = "Select idprofile from new_irci.account where idaccount = "+id+" ";
            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    idaccount = int.Parse(result[0].ToString());
                    break;
                }
                return idaccount;
                

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            //return false;

            return idaccount;
        }

        public bool updateAccountProfile(int idprofile, int idaccount)
        {
            dbCmd.Connection = dbCon;
            //var idaccount = "";
            System.Diagnostics.Debug.WriteLine("Di model "+idprofile + idaccount);
            dbCmd.CommandText = "Update new_irci.account set idprofile = "+idprofile+" where idaccount = "+idaccount+"";
            try
            {
                dbCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return false;
        }

        public string register(string email, string password)
        {
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = "Insert into new_irci.account (emailaccount, passaccount, roleaccount) values('"+email+"','"+password+"',1)";
            try
            {
                //dbCmd.Connection.Open();
                dbCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("berhasil input");
               
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            dbCmd.Connection.Close();
            dbCmd.Connection.Open();
            var idaccount = "";
            dbCmd.CommandText = "Select idaccount from new_irci.account where emailaccount = '"+email+"' and passaccount = '"+password+"'";
            System.Diagnostics.Debug.WriteLine(dbCmd.CommandText);
            try
            {
                var result = dbCmd.ExecuteReader();
                
                while (result.Read())
                {
                    idaccount = result[0].ToString();
                    break;
                }   
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return idaccount;

        }
    }
}