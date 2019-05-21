using System.Data.SqlClient;

namespace VehicleRental.Models
{
    public static class DBUtils
    {
        private static string sqlServer;
        private static string database;
        private static string user;
        private static string password;

        public static void config(string _sqlServer, string _database, string _user, string _passowrd)
        {
            sqlServer = _sqlServer;
            database = _database;
            user = _user;
            password = _passowrd;
        }

        public static SqlConnection GetSQLConnection()
        {
            string connectionString = @"Data Source=" + sqlServer + ";Initial Catalog="
                       + database + ";Persist Security Info=True;User ID=" + user + ";Password=" + password;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }
    }
}
