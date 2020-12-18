using Npgsql;

namespace IdentityManagement.Infrastructure.DbContexts
{
    public static class CreateNewDatabase
    {
        public static void EnsureDatabase(string connectionString, string name)
        {
            bool dbExists;

            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            string query = "SELECT EXISTS(SELECT datname FROM pg_catalog.pg_database WHERE datname=" + "'" + name + "'" + ")";
            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = query;
                dbExists = (bool)cmd.ExecuteScalar();
                if (!dbExists)
                {
                    string cmdCreateDB = "CREATE DATABASE " + '"' + name + '"';
                    cmd.CommandText = cmdCreateDB;
                    cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }
    }
}
