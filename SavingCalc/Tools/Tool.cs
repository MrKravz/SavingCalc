using MySql.Data.MySqlClient;
using SavingCalc.Economy;
using SavingCalc.IDEntities;
using System;
using System.IO;

namespace SavingCalc.Tools
{
    public static class Tool
    {

        public static int FindPercent(double sum, double part)
        {
            return Convert.ToInt32((part * 100) / sum);
        }
        public static int FindDayPercent(double previousSum, double nextSum)
        {
            return Convert.ToInt32(((nextSum - previousSum) * 100) / nextSum);
        }


        //public static bool DBExist(SQLConnection connection, User user)
        //{
        //    try
        //    {
        //        connection.openConnection();
        //        MySqlCommand Query = new MySqlCommand();
        //        Query.Connection = connection;
        //        Query.CommandText = "SELECT CONVERT(BIT, COUNT(*)) FROM sys.tables WHERE name = N'proverka'";
        //        Query.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        
//        int[] users = new int[table.Rows.Count];
//        int i = default;
//                if (reader.HasRows)
//                {
//                    while (reader.Read())
//                    {
//                        users[i] = Convert.ToInt32(reader.GetValue(0));
//                        i++;
//                    }
//                 }
//         db.closeConnection();
//         MessageBox.Show(users[1].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
