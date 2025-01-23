namespace ConsoleOracleLocal
{
    using Oracle.ManagedDataAccess.Client;
    using System.Data;


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start job!\n");

            // ローカルへの接続
            var cnnStr = "User Id=dbuser; password=sasa;" +
                       //EZ Connect Format is [hostname]:[port]/[service_name]
                       //Examine working TNSNAMES.ORA entries to find these values
                       "Data Source=192.168.11.15:1521/orcl; Pooling=false;";

            GetData(cnnStr);

            Console.WriteLine("\nend job!");
            Console.ReadLine();

        }

        static void GetData(string cnnStr)
        {
            using (var cnn = new OracleConnection(cnnStr))
            {
                cnn.Open();

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