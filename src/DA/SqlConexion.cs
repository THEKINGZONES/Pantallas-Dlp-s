using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DA
{
    public class SqlConexion
    {
        /// <summary>
        /// Giovani Torres De Loera
        /// Tijuana, Baja California
        /// </summary>
        private SqlConnection objConnection;
        public string connectionString { get; private set; }
        public SqlTransaction Transaction { get; set; }
        public Exception MessageException;
        private string formatMessage = "{0} \n==>\n {1} \n<==\n";


        #region InitializeComponent
        /// <summary>
        /// Initialize Conexion
        /// </summary>
        /// <param name="connectionString">Conexion Name</param>
        /// <returns>bool</returns>
        public SqlConexion(string connectionName, bool isConfigurationManager)
        {
            connectionString = connectionName;
            if (isConfigurationManager)
                connectionString = ConfigurationManager.ConnectionStrings[connectionName].ToString();
            InitializeComponent();
        }
        /// <summary>
        /// Initializa Conexion
        /// </summary>
        /// <param name="server">Server Name</param>
        /// <param name="dataBase">DataBase</param>
        /// <returns>bool</returns>
        public SqlConexion(string server, string dataBase)
        {
            connectionString = string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=true;", server, dataBase);
            InitializeComponent();
        }
        /// <summary>
        /// Initialize Conexion
        /// </summary>
        /// <param name="server">Server Name</param>
        /// <param name="dataBase">DataBase</param>
        /// <param name="user">User</param>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        public SqlConexion(string server, string dataBase, string user, string password)
        {
            connectionString = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", server, dataBase, user, password);
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            objConnection = new SqlConnection(connectionString);
            bool conect = OpenConnection();
            CloseConnection();
        }
        #endregion

        #region GetData

        public DataRow GetDataRow(SqlCommand command, CommandType typecommand = CommandType.StoredProcedure)
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

        public DataTable GetDataTable(SqlCommand command, CommandType typecommand = CommandType.StoredProcedure)
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

        public DataSet GetDataSet(SqlCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                command.Connection = objConnection;
                DataSet ds = new DataSet();
                OpenConnection();
                command.CommandType = typecommand;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(ds);
                }
                if (ds.Tables.Count != 0)
                    return ds;
            }
            catch (SqlException ex)
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
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, objConnection))
                {
                    adapter.Fill(ds);
                }
                if (ds.Tables.Count != 0)
                    return ds;
            }
            catch (SqlException ex)
            {
                MessageException = new Exception(string.Format(formatMessage, "Error in SQL query", ex.Message), ex);
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
        public int ExecuteNonQuery(SqlCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                command.Connection = objConnection;
                if (Transaction != null && Transaction.Connection != null)
                    command.Transaction = Transaction;
                OpenConnection();
                command.CommandType = typecommand;
                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
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
                using (SqlCommand command = new SqlCommand(query))
                {
                    command.Connection = objConnection;
                    if (Transaction != null && Transaction.Connection != null)
                        command.Transaction = Transaction;
                    OpenConnection();
                    command.CommandType = CommandType.Text;
                    return command.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
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
        public object ExecuteScalar(SqlCommand command, CommandType typecommand = CommandType.StoredProcedure)
        {
            try
            {
                command.Connection = objConnection;
                if (Transaction != null && Transaction.Connection != null)
                    command.Transaction = Transaction;
                OpenConnection();
                command.CommandType = typecommand;
                return command.ExecuteScalar();
            }
            catch (SqlException ex)
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
                using (SqlCommand command = new SqlCommand(query))
                {
                    command.Connection = objConnection;
                    if (Transaction != null && Transaction.Connection != null)
                        command.Transaction = Transaction;
                    OpenConnection();
                    command.CommandType = CommandType.Text;
                    return command.ExecuteScalar();
                }

            }
            catch (SqlException ex)
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

        public SqlParameter NewParameter(string name, SqlDbType type, object value)
        {
            try
            {
                SqlParameter parameter = new SqlParameter(name, type);
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
            catch (SqlException ex)
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
