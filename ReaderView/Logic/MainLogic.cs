using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using ReaderView.Model;

namespace ReaderView.Logic
{
    public static class MainLogic
    {
        private static NpgsqlConnection getConnect()
        {
            // Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres; " +
               "Password=postgres;Database=readerdb;");
            return conn;
        }

        public static string CheckConnect()
        {
            using var con = getConnect();
            con.Open();
            using var cmd = new NpgsqlCommand("SELECT version()", con);

            string version = cmd.ExecuteScalar().ToString().Split(',')[0];
            con.Close();
            return ($"PostgreSQL version: {version}");
            
        }
       
        public static List<PeopleViewModel> GetAll()
        {
            List<PeopleViewModel> list = new List<PeopleViewModel>();

            var con = getConnect();
            using (con)
            {
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(
                  "SELECT * FROM people;", con);

                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PeopleViewModel model = new PeopleViewModel();
                        model.Id = reader.GetInt32(0);
                        model.Name = reader.GetString(1);
                        model.Surname = reader.GetString(2);
                        list.Add(model);
                    }
                }
                reader.Close();
            }
            con.Close();
            return list;
        }
    }
}
