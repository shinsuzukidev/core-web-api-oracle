using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace ConsoleDBHelper.Util
{
    public class DbAccess: IDisposable
    {
        private OracleConnection? _cnn = null ;
        private OracleTransaction? _tran = null;
        private bool disposedFlag = false;
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DbAccess()
        {
            // ConfigurationManagerを使用
            this._cnn = new OracleConnection(ConfigurationManager.AppSettings["connectionString"]);
        }

        public DbAccess(string connectionString)
        {
            this._cnn = new OracleConnection(connectionString);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (this.disposedFlag == false)
            {
                if (disposing)
                {
                    if (this._tran != null)
                    {
                        this._tran.Dispose();
                    }

                    if (this._cnn != null)
                    {
                        this.Close();
                        this._cnn.Dispose();
                    }
                }
                this.disposedFlag = true;
            }
        }

        public void Open()
        { 
            if (this._cnn!.State == System.Data.ConnectionState.Closed)
            {
                this._cnn.Open();
            }
        }

        public void Close()
        {
            if (this._cnn != null && this._cnn.State == ConnectionState.Open)
            {
                this._cnn.Close();
            }
        }

        public DbTransaction BeginTransaction()
        {
            this._tran = this._cnn!.BeginTransaction(IsolationLevel.Serializable);
            return this._tran;
        }

        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            this._tran = this._cnn!.BeginTransaction(isolationLevel);
            return this._tran;
        }

        public void Commit()
        {
            this._tran?.Commit();
        }

        public void Rollback()
        {
            this._tran?.Rollback();
        }

        public OracleCommand CreateCommand()
        {
            var cmd = this._cnn!.CreateCommand();
            cmd!.BindByName = true;
            return cmd;
        }

        public DataTable ExecuteQuery(OracleCommand cmd)
        {
            try
            {
                cmd.Connection = this._cnn;
                cmd.Transaction = this._tran;
                var da = new OracleDataAdapter(cmd);
                DataSet? ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }

        public int ExecNonQuery(DbCommand cmd)
        {
            try
            {
                cmd.Connection = this._cnn;
                cmd.Transaction = this._tran;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }

        public T? ExecuteScalar<T>(DbCommand cmd)
        {
            try
            {
                cmd.Connection = this._cnn;
                cmd.Transaction = this._tran;
                return (T?)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }

        public static void AddParameter(OracleCommand cmd, string parameterName, OracleDbType type, Object value)
        {
            OracleParameter param = cmd.CreateParameter();
            param.ParameterName = parameterName;
            param.OracleDbType = type;
            param.Direction = ParameterDirection.Input;
            param.Value = value;
            cmd.Parameters.Add(param);
        }

    }
}
