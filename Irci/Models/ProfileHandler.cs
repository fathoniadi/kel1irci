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

                var counter = 0;
                while (result.Read())
                {
                    // edit sini
                    // profiles.Add(new Profile() { Judul = result[0].ToString(), Submission = result[1].ToString(), Bahasa = result[2].ToString(), Deskripsi = result[3].ToString(), Publisher = result[4].ToString(), URL = result[5].ToString() });
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
            return profiles;
        }

        // createProfile
    }
}