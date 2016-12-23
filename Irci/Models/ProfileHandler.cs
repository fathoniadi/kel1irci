using System;
using System.Collections.Generic;
using Irci.Entity;
using Npgsql;
using System.Web.Helpers;
namespace Irci.Models
{
    public class ProfileHandler
    {
        private NpgsqlConnection dbCon;
        private NpgsqlCommand dbCmd;
        private List<Profile> profiles;

        public ProfileHandler()
        {
            dbCon = new Connection().getConnection();
            dbCon.Open();
            dbCmd = new NpgsqlCommand();
            profiles = new List<Profile>();
        }

        public bool insertAuthorship(List<string> idProfiles, string idArticle)
        {
            dbCmd.Connection = dbCon;
            foreach (var idProfile in idProfiles)
            {
                dbCmd.CommandText = "Insert into new_irci.authorship(idarticle, idprofile) values ('"+idArticle+"','"+idProfile+"')";
                if (dbCmd.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbCmd.Connection.Open();
                }
                try
                {
                    dbCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("error insert authorship");
                }
            }
            return true;
        }

        public List<Profile> getProfiles()
        {
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = "SELECT idprofile, idaccount, idprofilemain, namaprofile, instansiprofile, deskripsiprofile FROM irci_new.records LIMIT 5";
            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    // edit sini
                    // profiles.Add(new Profile() { Judul = result[0].ToString(), Submission = result[1].ToString(), Bahasa = result[2].ToString(), Deskripsi = result[3].ToString(), Publisher = result[4].ToString(), URL = result[5].ToString() });
                    var profile = new Profile() { ID = result[0].ToString(), IDAccount = result[1].ToString(), IDProfileMain = result[2].ToString(), Nama = result[3].ToString(), Instansi = result[4].ToString(), Deskripsi = result[5].ToString() };
                    profiles.Add(profile);
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
            return profiles;
        }

        /*public void insertAuthorship(string idarticle, string idprofile)
        {
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = "SELECT idprofile, idaccount, idprofilemain, namaprofile, instansiprofile, deskripsiprofile FROM irci_new.records LIMIT 5";
            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    // edit sini
                    // profiles.Add(new Profile() { Judul = result[0].ToString(), Submission = result[1].ToString(), Bahasa = result[2].ToString(), Deskripsi = result[3].ToString(), Publisher = result[4].ToString(), URL = result[5].ToString() });
                    var profile = new Profile() { ID = result[0].ToString(), IDAccount = result[1].ToString(), IDProfileMain = result[2].ToString(), Nama = result[3].ToString(), Instansi = result[4].ToString(), Deskripsi = result[5].ToString() };
                    profiles.Add(profile);
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
        }*/

        public List<Profile> GetProfiles(String keyword_nama)
        {
            dbCmd.Connection = dbCon;
            //dbCmd.Connection.Open();
            List<Profile> profiles = new List<Profile>();

            dbCmd.CommandText = "select idprofile, idaccount, idprofilemain, namaprofile, instansiprofile, deskripsiprofile from new_irci.profile where namaprofile LIKE '%' || @keyword || '%' limit 30; ";
            dbCmd.Parameters.Add(new NpgsqlParameter("@keyword", keyword_nama));

            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    // idProfile = int.Parse(result[0].ToString());
                    //return idProfile;
                    var profiletemp = new Profile()
                    {
                        ID = result[0].ToString(),
                        IDAccount = result[1].ToString(),
                        IDProfileMain = result[2].ToString(),
                        Nama = result[3].ToString(),
                        Instansi = result[4].ToString(),
                        Deskripsi = result[5].ToString()
                    };

                    profiles.Add(profiletemp);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            dbCmd.Connection.Close();

            return profiles;
        }

        public Profile GetOneProfile(String _idProfile)
        {
            dbCmd.Connection = dbCon;
            Profile profile = new Profile();
            try
            {
                dbCmd.Connection.Open();
            }
            catch (InvalidOperationException e) { }

            dbCmd.CommandText = "select * from new_irci.profile where idprofile="+ _idProfile +";";
            System.Diagnostics.Debug.WriteLine(dbCmd.CommandText);

            try
            {
                var result = dbCmd.ExecuteReader();
                while (result.Read())
                {
                    var profiletemp = new Profile()
                    {
                        ID = result[0].ToString(),
                        IDAccount = result[1].ToString(),
                        IDProfileMain = result[2].ToString(),
                        Nama = result[3].ToString(),
                        Instansi = result[4].ToString(),
                        Deskripsi = result[5].ToString()
                    };

                    profile = profiletemp;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            
            return profile;
        }

        public int createProfile(string nama, string instansi, int idaccount)
        {

            dbCmd.Connection = dbCon;

            //var idprofile =0;
            // System.Diagnostics.Debug.Write(article.Submission.Split(' ')[0]);

            dbCmd.CommandText = "insert into new_irci.profile (namaprofile, instansiprofile, idaccount) values('" + nama + "', '" + instansi+ "', "+idaccount+ ") RETURNING idprofile";
            dbCmd.CommandType = System.Data.CommandType.Text;
            try
            {
                dbCmd.Connection.Open();
            }
            catch (InvalidOperationException e)
            {

            }

            try
            {
                var result = dbCmd.ExecuteReader();
                var idprofile = 0;

                while (result.Read())
                {
                    System.Diagnostics.Debug.WriteLine(int.Parse(result[0].ToString()));
                    
                    idprofile = int.Parse(result[0].ToString());
                    UpdateIDProfileMain(result[0].ToString());
                    return idprofile;
                    break;
                    //return idArticle;

                }
                dbCmd.Connection.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return 0;
        }

        public string insertProfile(Profile profile)
        {
            dbCmd.Connection = dbCon;

            var idProfile = "";

            dbCmd.CommandText = "insert into new_irci.profile (namaprofile, deskripsiprofile) values('"+profile.Nama+"', '"+profile.Deskripsi+"') ON CONFLICT(namaprofile) DO UPDATE SET deskripsiprofile=EXCLUDED.deskripsiprofile RETURNING idprofile";
            dbCmd.CommandType = System.Data.CommandType.Text;
          
            dbCmd.CommandTimeout = 0;
            if (dbCmd.Connection.State == System.Data.ConnectionState.Closed)
            {
                dbCmd.Connection.Open();
            }
            try
            {
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e) { }
            
            try
            {
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            dbCmd.CommandText = "SELECT currval('profile_id_seq');";
            
            try
            {
                dbCmd.Connection.Open();
            }
            catch(Exception e) {}
            
            try 
            {
                var result = dbCmd.ExecuteReader();
                while (result.Read())
                {
                    idProfile = result[0].ToString();
                    break;
                }
                dbCmd.Connection.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            
            return idProfile;
        }

        

        public void MergeProfile(string[] idprofile, string idaccount)
        {
            string IDProfile = GetProfileFromAccount(idaccount);

            dbCmd.Connection = dbCon;

            string IDProfileMain = IDProfile;       // EDIT SINI
            string IDAccount = idaccount;             // EDIT SINI
            // System.Diagnostics.Debug.Write(article.Submission.Split(' ')[0]);

            string ids = "";
            for (int i = 0; i < idprofile.Length; i++)
            {
                ids += " idprofile=";
                ids += idprofile[i];
                if (idprofile.Length - i != 1)
                    ids += " or ";
            }

            dbCmd.CommandText = "update new_irci.profile set idprofilemain=" + IDProfileMain + ", idaccount="+ IDAccount +" where " + ids;
            dbCmd.CommandType = System.Data.CommandType.Text;

            System.Diagnostics.Debug.WriteLine(dbCmd.CommandText);

            try
            {
                dbCmd.Connection.Open();
            }
            catch (InvalidOperationException e) {}

            try
            {
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            dbCmd.Connection.Close();
        }

        public string GetProfileFromAccount(string idaccount)
        {
            string IDProfile = "";
            dbCmd.Connection = dbCon;
            
            dbCmd.CommandText = "select idprofile from new_irci.account where idaccount="+idaccount+";";
            dbCmd.CommandType = System.Data.CommandType.Text;

            try
            {
                dbCmd.Connection.Open();
            }
            catch (InvalidOperationException e) { }

            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    IDProfile = result[0].ToString();
                }
                dbCmd.Connection.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return IDProfile;
        }

        public string GetNamaProfileFromProfile(string idprofile)
        {
            string NamaProfile = "";
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = "select namaprofile from new_irci.profile where idprofile=" + idprofile + ";";

            dbCmd.CommandType = System.Data.CommandType.Text;

            try
            {
                dbCmd.Connection.Open();
            }
            catch (InvalidOperationException e) { }

            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    NamaProfile = result[0].ToString();
                }
                dbCmd.Connection.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return NamaProfile;
        }

        public void UpdateIDProfileMain(string idprofile)
        {
            dbCmd.Connection = dbCon;

            dbCmd.CommandText = "update new_irci.profile set idprofilemain="+ idprofile +" where idprofile="+idprofile;
            dbCmd.CommandType = System.Data.CommandType.Text;

            dbCmd.CommandTimeout = 0;
            
            try
            {
                dbCmd.Connection.Open();
            }
            catch (Exception e)
            {}

            try
            {
                dbCmd.ExecuteNonQuery();
                dbCmd.Connection.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}