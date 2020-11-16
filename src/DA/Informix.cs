using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;

namespace DA
{
    public class ConexionIBM
    {   
      
        private OdbcConnection objConnection;
        public string connectionString { get; private set; }
        public OdbcTransaction Transaction { get; set; }
        public Exception MessageException;
        private string formatMessage = "{0} \n==>\n {1} \n<==\n";

        public ConexionIBM(string connection)
        {
            connectionString = connection;
            InitializeComponent();
        }
        #region InitializeComponent

        public void InitializeComponent()
        {
            var conect = ConfigurationManager.ConnectionStrings[connectionString].ToString();
            objConnection = new OdbcConnection(conect);
            OpenConnection();
            CloseConnection();
        }
        #endregion

        #region GetData

        public DataRow GetDataRow(OdbcCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                DataTable dt = GetDataTable(command, typecommand);
                if (dt != null)
                    if (dt.Rows.Count == 1)
                        return dt.Rows[0];

                return null;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw;
            }
        }

        public DataTable GetDataTable(OdbcCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                DataSet ds = GetDataSet(command, typecommand);
                DataTable dt = null;
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    else
                        dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw;
            }
        }

        public DataSet GetDataSet(OdbcCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                command.Connection = objConnection;
                DataSet ds = new DataSet();
                OpenConnection();
                command.CommandType = typecommand;
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                {
                    adapter.Fill(ds);
                }
                if (ds.Tables.Count != 0)
                    return ds;
            }
            catch (OdbcException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Error in SQL Command", ex.Message), ex);
                throw MessageException;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }

        public DataRow GetDataRow(string query)
        {
            try
            {
                DataTable dt = GetDataTable(query);
                if (dt != null)
                    if (dt.Rows.Count == 1)
                        return dt.Rows[0];

                return null;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw;
            }
        }

        public DataTable GetDataTable(string query)
        {
            try
            {
                DataSet ds = GetDataSet(query);
                DataTable dt = null;
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    else
                        dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
        }

        public DataSet GetDataSet(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(query, objConnection))
                {
                    adapter.SelectCommand.CommandTimeout = 90;
                    adapter.Fill(ds);
                }
                if (ds.Tables.Count != 0)
                    return ds;
            }
            catch (OdbcException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Error in SQL query", ex.Message), ex);
                MessageException = new Exception(string.Format("0:{0}-1:{1}-2:{2}", objConnection.Driver, objConnection.Database, objConnection.DataSource), MessageException);

                throw MessageException;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }

        #endregion

        #region ExecuteCommand
        /// <summary>
        /// ExecuteCommand NonQuery
        /// </summary>
        /// <param name="command">SqlCommand</param>
        /// <returns>int</returns>
        public int ExecuteNonQuery(OdbcCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                command.CommandTimeout = 60;
                command.Connection = objConnection;
                if (Transaction != null && Transaction.Connection != null)
                    command.Transaction = Transaction;
                OpenConnection();
                command.CommandType = typecommand;
                return command.ExecuteNonQuery();
            }
            catch (OdbcException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Error in SQL Command", ex.Message), ex);
                throw MessageException;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
            finally
            {
                if (Transaction == null)
                    CloseConnection();
            }

        }
        public int ExecuteNonQuery(string query)
        {
            try
            {
                using (OdbcCommand command = new OdbcCommand(query))
                {
                    command.CommandTimeout = 60;
                    command.Connection = objConnection;
                    if (Transaction != null && Transaction.Connection != null)
                        command.Transaction = Transaction;
                    OpenConnection();
                    command.CommandType = CommandType.Text;
                    return command.ExecuteNonQuery();
                }

            }
            catch (OdbcException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Error in SQL Command", ex.Message), ex);
                throw MessageException;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
            finally
            {
                if (Transaction == null)
                    CloseConnection();
            }

        }
        /// <summary>
        /// ExecuteCommand Scalar
        /// </summary>
        /// <param name="command">SqlCommand</param>
        /// <returns>object</returns>
        public object ExecuteScalar(OdbcCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                command.CommandTimeout = 60;
                command.Connection = objConnection;
                if (Transaction != null && Transaction.Connection != null)
                    command.Transaction = Transaction;
                OpenConnection();
                command.CommandType = typecommand;
                return command.ExecuteScalar();
            }
            catch (OdbcException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Error in SQL Command", ex.Message), ex);
                throw MessageException;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
            finally
            {
                if (Transaction == null)
                    CloseConnection();
            }
        }
        public object ExecuteScalar(string query)
        {
            try
            {
                using (OdbcCommand command = new OdbcCommand(query))
                {
                    command.CommandTimeout = 60;
                    command.Connection = objConnection;
                    if (Transaction != null && Transaction.Connection != null)
                        command.Transaction = Transaction;
                    OpenConnection();
                    command.CommandType = CommandType.Text;
                    return command.ExecuteScalar();
                }

            }
            catch (OdbcException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Error in SQL Command", ex.Message), ex);
                throw MessageException;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
            finally
            {
                if (Transaction == null)
                    CloseConnection();
            }
        }
        #endregion

        public OdbcParameter NewParameter(string name, OdbcType type, object value)
        {
            try
            {
                OdbcParameter parameter = new OdbcParameter(name, type);
                parameter.Value = value;
                return parameter;
            }
            catch (Exception ex)
            {
                MessageException = ex;
                throw MessageException;
            }
        }

        public bool OpenConnection()
        {
            try
            {
                if (objConnection == null)
                    InitializeComponent();
                if (objConnection.State == ConnectionState.Closed || objConnection.State == ConnectionState.Broken)
                {
                    objConnection.Open();
                    return true;
                }
            }
            catch (OdbcException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Failed to connect to SQL Server", ex.Message), ex);
            }
            catch (InvalidOperationException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Connection string not set", ex.Message), ex);
            }
            catch (Exception ex)
            {
                MessageException = ex;
            }

            return false;
        }

        public bool CloseConnection()
        {
            try
            {
                if (objConnection.State != ConnectionState.Executing & objConnection.State != ConnectionState.Fetching)
                {
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                        Transaction = null;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageException = ex;
            }

            return false;
        }
        /// <summary>
        /// BeginTransaction
        /// Open Connection and Start Transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (Transaction == null)
            {
                OpenConnection();
                Transaction = objConnection.BeginTransaction();
            }
            else if (Transaction.Connection == null)
            {
                OpenConnection();
                Transaction = objConnection.BeginTransaction();
            }
        }

    }
}
