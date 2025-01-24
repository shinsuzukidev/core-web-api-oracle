using Oracle.ManagedDataAccess.Client;
using Utility.DB;

namespace ConsoleDBHelper
{
    internal class Program
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("start.");

            using (var dba = new DbAccess())
            {
                dba.Open();
                var cmd = dba.CreateCommand();

                // ExecuteQuery
                logger.Info("> ExecuteQuery");
                cmd.CommandText = "select * from mysample where ID=:p_ID";
                dba.AddParameter(cmd, "p_ID", OracleDbType.Int64, 2);
                var dt = dba.ExecuteQuery(cmd);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Console.WriteLine(dt.Rows[i]["ID"] + "," + dt.Rows[i]["NAME"] + "," + dt.Rows[i]["AGE"]);
                }

                // ExecuteNoQuery
                logger.Info("> ExecuteNoQuery");
                try
                {
                    dba.BeginTransaction();
                    cmd = dba.CreateCommand();
                    cmd.CommandText = "insert into mysample values (:p_ID,:p_NAME,:p_AGE)";
                    dba.AddParameter(cmd, "p_AGE", OracleDbType.Int64, 80);
                    dba.AddParameter(cmd, "p_NAME", OracleDbType.NVarchar2, "nemoto");
                    dba.AddParameter(cmd, "p_ID", OracleDbType.Int64, 6);
                    dba.ExecNonQuery(cmd);
                    dba.Commit();
                }
                catch (Exception ex)
                {
                    dba.Rollback();
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                    return;
                }

                // ExecuteScalar
                logger.Info("> ExecuteScalar");
                cmd = dba.CreateCommand();
                cmd.CommandText = "select count(*) from mysample";
                var count = dba.ExecuteScalar<decimal>(cmd);
                Console.WriteLine($"count: {count}");


                // ExecuteReader
                logger.Info("> ExecuteReader");
                cmd = dba.CreateCommand();
                cmd.CommandText = "select * from mysample";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Name : " + reader["NAME"]);
                    }
                }
            }

            logger.Info("success.");
            Console.ReadLine();
        }

    }
}
