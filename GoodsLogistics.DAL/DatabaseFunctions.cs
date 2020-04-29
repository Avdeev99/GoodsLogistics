using System.Data;
using Microsoft.Data.SqlClient;

namespace GoodsLogistics.DAL
{
    public class DatabaseFunctions
    {
        public static void Backup(string databaseName, string backupPath)
        {
            var commandText = $@"BACKUP DATABASE [{databaseName}] TO DISK = N'{backupPath}' WITH NOFORMAT, INIT, NAME = N'{databaseName}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            using (SqlConnection connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=GoodsLogisticsDb;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
