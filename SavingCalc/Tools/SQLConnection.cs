using MySql.Data.MySqlClient;

namespace SavingCalc.Tools
{
    public class SQLConnection
    {
        public MySqlConnection connect = new MySqlConnection("server = localhost; port = 3306;database = savingcalcdb; user = root; password = root; SSL-mode=none;");
        public void openConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }
        public void closeConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }
        public MySqlConnection getConnection() { return connect; }
    }
}
