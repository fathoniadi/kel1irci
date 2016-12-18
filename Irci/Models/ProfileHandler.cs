using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irci.Entity;
using Npgsql;

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

        public List<Profile> GetProfiles(String keyword_nama)
        {
            dbCmd.Connection = dbCon;
            dbCmd.Connection.Open();
            List<Profile> profiles = new List<Profile>();

            dbCmd.CommandText = "select idprofile, idaccount, idprofilemain, namaprofile, instansiprofile, deskripsiprofile from new_irci.profile where namaprofile LIKE '%' || @keyword || '%'; ";
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
            dbCmd.Connection.Open();
            Profile profile = null;

            dbCmd.CommandText = "select idprofile, idaccount, idprofilemain, namaprofile, instansiprofile, deskripsiprofile from new_irci.profile where idprofile = @idprofile";
            dbCmd.Parameters.Add(new NpgsqlParameter("@idprofile", _idProfile));

            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    // idProfile = int.Parse(result[0].ToString());
                    //return idProfile;
                    profile.ID = result[0].ToString();
                    profile.IDAccount = result[1].ToString();
                    profile.IDProfileMain = result[2].ToString();
                    profile.Nama = result[3].ToString();
                    profile.Instansi = result[4].ToString();
                    profile.Deskripsi = result[5].ToString();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            dbCmd.Connection.Close();

            return profile;
        }

        public int insertProfile(Profile profile)
        {
            dbCmd.Connection = dbCon;
            dbCmd.Connection.Open();

            var idProfile = 0;
            // System.Diagnostics.Debug.Write(article.Submission.Split(' ')[0]);

            dbCmd.CommandText = 
                "insert into new_irci.profile (idaccount, idprofilemain, namaprofile, instansiprofile, deskripsiprofile) values(@idaccount, @idprofilemain, @nama, @instansi, @deskripsi)";
            dbCmd.CommandType = System.Data.CommandType.Text;
            dbCmd.Parameters.Add(new NpgsqlParameter("@idaccount", profile.IDAccount));
            dbCmd.Parameters.Add(new NpgsqlParameter("@idprofilemain", profile.IDProfileMain));
            dbCmd.Parameters.Add(new NpgsqlParameter("@nama", profile.Nama));
            dbCmd.Parameters.Add(new NpgsqlParameter("@instansi", profile.Instansi));
            dbCmd.Parameters.Add(new NpgsqlParameter("@deskripsi", profile.Deskripsi));
            try
            {
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            dbCmd.Connection.Close();
            dbCmd.Connection.Open();

            dbCmd.CommandText = "select idprofile from new_irci.profile where namaprofile = @nama and deskripsi = @deskripsi";
            dbCmd.Parameters.Add(new NpgsqlParameter("@nama", profile.Nama));
            dbCmd.Parameters.Add(new NpgsqlParameter("@deskripsi", profile.Deskripsi));

            try
            {
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    idProfile = int.Parse(result[0].ToString());
                    //return idArticle;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            System.Diagnostics.Debug.WriteLine("inserted");

            return idProfile;
        }
    }
}