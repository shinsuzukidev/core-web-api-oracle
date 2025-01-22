namespace ConsoleOracle
{
    using Oracle.ManagedDataAccess.Client;
    using System.Data;
    using System.Data.SqlTypes;
    using System.Security.Cryptography;

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            var cnnStr = "User Id=system; password=sasa;" +
                       //EZ Connect Format is [hostname]:[port]/[service_name]
                       //Examine working TNSNAMES.ORA entries to find these values
                       "Data Source=192.168.11.20:1521/orcl.hrgm.jp; Pooling=false;";

            // InsertData(cnnStr);
            // Update(cnnStr);
            // Delete(cnnStr);
            GetData(cnnStr);

            Console.ReadLine();
        }

        static void Delete(string cnnStr)
        {
            using (var cnn = new OracleConnection(cnnStr))
            {
                cnn.Open();
                cnn.BeginTransaction();

                try
                {
                    var sql = "delete from mysample where ID=:p_ID";
                    OracleCommand cmd = cnn.CreateCommand();
                    cmd.BindByName = true;  // 名前でバインド
                    cmd.CommandText = sql;

                    // パラメータを作成
                    cmd.Parameters.Add(new OracleParameter("p_ID", OracleDbType.Decimal)).Value = 5;
                    cmd.ExecuteNonQuery();
                    cnn.Commit();
                }
                catch (Exception ex)
                {
                    cnn.Rollback();
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        static void Update(string cnnStr) 
        {
            using (var cnn = new OracleConnection(cnnStr))
            {
                cnn.Open();
                cnn.BeginTransaction();

                try
                {
                    var sql = "update mysample set AGE=:p_AGE where ID=:p_ID";
                    OracleCommand cmd = cnn.CreateCommand();
                    cmd.BindByName = true;  // 名前でバインド
                    cmd.CommandText = sql;

                    // パラメータを作成
                    cmd.Parameters.Add(new OracleParameter("p_ID", OracleDbType.Decimal)).Value = 5;
                    cmd.Parameters.Add(new OracleParameter("p_AGE", OracleDbType.Decimal)).Value = 99;
                    cmd.ExecuteNonQuery();
                    cnn.Commit();
                }
                catch (Exception ex)
                {
                    cnn.Rollback();
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        static void InsertData(string cnnStr)
        {
            using (var cnn = new OracleConnection(cnnStr))
            {
                cnn.Open();
                cnn.BeginTransaction();

                try
                {
                    var sql = "insert into mysample values (:p_ID, :p_NAME, :p_AGE)";
                    OracleCommand cmd = cnn.CreateCommand();
                    cmd.BindByName = true;  // 名前でバインド
                    cmd.CommandText = sql;

                    // パラメータを作成
                    cmd.Parameters.Add(new OracleParameter("p_ID", OracleDbType.Decimal)).Value = 5;
                    cmd.Parameters.Add(new OracleParameter("p_NAME", OracleDbType.NVarchar2)).Value = "akai";
                    cmd.Parameters.Add(new OracleParameter("p_AGE", OracleDbType.Decimal)).Value = 40;
                    cmd.ExecuteNonQuery();
                    cnn.Commit();
                }
                catch (Exception ex)
                {
                    cnn.Rollback();
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        static void GetData(string cnnStr)
        {
            using (var cnn = new OracleConnection(cnnStr))
            {
                cnn.Open();
                //var sql = "select * from mysample where NAME=:p_NAME and AGE=:p_AGE";
                //OracleCommand cmd = cnn.CreateCommand();
                //cmd.BindByName = true;  // 名前でバインド
                //cmd.CommandText = sql;

                //// パラメータを作成
                //var param1 = new OracleParameter("p_NAME", OracleDbType.NVarchar2);
                //param1.Value = "tanaka";
                //cmd.Parameters.Add(param1);
                //// parameter
                ////cmd.Parameters.Add(new OracleParameter("p_NAME", OracleDbType.NVarchar2)).Value = "tanaka";

                //var param2 = new OracleParameter("p_AGE", OracleDbType.Decimal);
                //param2.Value = 21;
                //cmd.Parameters.Add(param2);


                var sql = "select * from mysample order by ID";
                OracleCommand cmd = cnn.CreateCommand();
                cmd.BindByName = true;  // 名前でバインド
                cmd.CommandText = sql;

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                var ds = new DataSet();
                adapter.Fill(ds);
                var dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Console.WriteLine(dt.Rows[i]["ID"] + "," + dt.Rows[i]["NAME"] + "," + dt.Rows[i]["AGE"]);
                }

                cnn.Clone();
            }

        }
    }
}
