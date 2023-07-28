using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// slf_DbHelper 的摘要说明
/// </summary>
public class slf_DbHelper
{
	public slf_DbHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    protected static string m_connectionstring = null;

    /// <summary>
    /// 查询次数统计
    /// </summary>
    private static int m_querycount = 0;

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public static string connectionString
    {
        get
        {
            if (m_connectionstring == null)
            {
                m_connectionstring = "Data Source=XXXXX;Initial Catalog=XXXX;User ID=XXXX;Password=XXXXXX;";
            }
            return m_connectionstring;
        }
        set
        {
            m_connectionstring = value;
        }
    }
    /// <summary>
    /// Mccshow数据接口
    /// </summary>

    private static object lockHelper = new object();
    #region  执行简单SQL语句
    public static SqlConnection GetConnection()
    {
        try
        {
            return new SqlConnection(connectionString);
        }
        catch
        {
            throw new Exception("sql connection is not invalid");
        }
    }
    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public static int ExecuteSql(string SQLString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    connection.Close();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    connection.Close();
                    throw new Exception(E.Message);
                }
            }
        }
    }
    #endregion
    /// <summary>
    /// 为SqlCommand添加参数
    /// </summary>
    /// <param name="command">SqlCommand对象</param>
    /// <param name="commandParameters">SqlParameter[]对象</param>
    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        foreach (SqlParameter p in commandParameters)
        {
            if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null)) { p.Value = DBNull.Value; }
            command.Parameters.Add(p);
        }
    }

    public static DataSet ExecuteDS(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException("connection");

        // 预处理
        SqlCommand cmd = new SqlCommand();
        bool mustCloseConnection = false;
        PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

        // 创建IDbDataAdapter和DataSet.
        //using (IDbDataAdapter da = Factory.CreateDataAdapter())
        SqlDataAdapter da = new SqlDataAdapter();
        {
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            // 填充DataSet.
            da.Fill(ds);

            m_querycount++;

            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return ds;
        }
    }

    public static DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

        // 创建并打开数据库连接对象,操作完成释放对象.

        using (SqlConnection connection = new SqlConnection())
        {
            connection.ConnectionString = connectionString;
            connection.Open();

            // 调用指定数据库连接字符串重载方法.
            return ExecuteDS(connection, commandType, commandText, commandParameters);
        }
    }
    /// <summary>
    /// 执行带一个存储过程参数的的SQL语句。
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
    /// <returns>影响的记录数</returns>
    public static int ExecuteSql(string SQLString, string content)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(SQLString, connection);
            System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
            myParameter.Value = content;
            cmd.Parameters.Add(myParameter);
            try
            {
                connection.Open();
                int rows = cmd.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }
    /// <returns>返回一个包含结果集的DataSet</returns>
    public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("ConnectionString");

        // 创建并打开数据库连接对象,操作完成释放对象.

        using (SqlConnection connection = new SqlConnection())
        {
            connection.ConnectionString = connectionString;
            connection.Open();

            // 调用指定数据库连接字符串重载方法.
            return ExecuteDataTable(connection, commandType, commandText, commandParameters);
        }
    }
    /// <param name="connection">一个有效的数据库连接对象</param>
    /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
    /// <param name="commandText">存储过程名或SQL语句</param>
    /// <param name="commandParameters">SqlParamter参数数组</param>
    /// <returns>返回一个包含结果集的DataSet</returns>
    public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException("connection");

        // 预处理
        SqlCommand cmd = new SqlCommand();
        bool mustCloseConnection = false;
        PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

        // 创建IDbDataAdapter和DataSet.
        //using (IDbDataAdapter da = Factory.CreateDataAdapter())
        SqlDataAdapter da = new SqlDataAdapter();
        {
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            // 填充DataSet.
            da.Fill(ds);

            m_querycount++;

            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return ds.Tables[0];
        }
    }
    /// <summary>
    /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
    /// </summary>
    /// <param name="command">要处理的DbCommand</param>
    /// <param name="connection">数据库连接</param>
    /// <param name="transaction">一个有效的事务或者是null值</param>
    /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>
    /// <param name="commandText">存储过程名或都SQL命令文本</param>
    /// <param name="commandParameters">和命令相关联的DbParameter参数数组,如果没有参数为'null'</param>
    /// <param name="mustCloseConnection"><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
    {
        if (command == null) throw new ArgumentNullException("command");
        if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

        // If the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            connection.Open();
        }
        else
        {
            mustCloseConnection = false;
        }

        // 给命令分配一个数据库连接.
        command.Connection = connection;

        // 设置命令文本(存储过程名或SQL语句)
        command.CommandText = commandText;

        // 分配事务
        if (transaction != null)
        {
            if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            command.Transaction = transaction;
        }

        // 设置命令类型.
        command.CommandType = commandType;

        // 分配命令参数
        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }
        return;
    }
    /// <summary>
    /// 执行指定数据库连接对象的数据阅读器.
    /// </summary>
    /// <remarks>
    /// 如果是BaseDbHelper打开连接,当连接关闭DataReader也将关闭.
    /// 如果是调用都打开连接,DataReader由调用都管理.
    /// </remarks>
    /// <param name="connection">一个有效的数据库连接对象</param>
    /// <param name="transaction">一个有效的事务,或者为 'null'</param>
    /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
    /// <param name="commandText">存储过程名或SQL语句</param>
    /// <param name="commandParameters">DbParameters参数数组,如果没有参数则为'null'</param>
    /// <param name="connectionOwnership">标识数据库连接对象是由调用者提供还是由BaseDbHelper提供</param>
    /// <returns>返回包含结果集的DbDataReader</returns>
    private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException("connection");

        bool mustCloseConnection = false;
        // 创建命令
        SqlCommand cmd = new SqlCommand();
        try
        {
            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // 创建数据阅读器
            SqlDataReader dataReader;
            dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            m_querycount++;
            bool canClear = true;
            foreach (SqlParameter commandParameter in cmd.Parameters)
            {
                if (commandParameter.Direction != ParameterDirection.Input)
                    canClear = false;
            }

            if (canClear)
            {
                //cmd.Dispose();
                cmd.Parameters.Clear();
            }


            return dataReader;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }
    /// <summary>
    /// 运行SQL角本
    /// </summary>
    /// <param name="connStr">connStr</param>
    /// <param name="commandType">CommandType</param>
    /// <param name="strSql">SQL角本串</param>
    /// <param name="commandParameters">SqlParameter[]</param>
    /// <returns>返回操作是否成功</returns>
    public static bool ExecuteSql(CommandType commandType, String strSql, params SqlParameter[] commandParameters)
    {
        bool State = true;
        try
        {
            using (SqlConnection cn = GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, cn, (SqlTransaction)null, commandType, strSql, commandParameters);
                int retval = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cn.Close();

            }
        }
        catch (Exception)
        {
            State = false;
        }
        return State;
    }
    /// <summary>
    /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
    /// </summary>
    /// <param name="strSQL">SQL语句</param>
    /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
    /// <returns>影响的记录数</returns>
    public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
            myParameter.Value = fs;
            cmd.Parameters.Add(myParameter);
            try
            {
                connection.Open();
                int rows = cmd.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }


    /// <summary>
    /// 这种方法打开,并指派一个connection, transaction, command type and parameters
    /// </summary>
    /// <param name="command">SqlCommand对象</param>
    /// <param name="connection">SqlConnection对象</param>
    /// <param name="transaction">SqlTransaction对象</param>
    /// <param name="commandType">CommandType对象</param>
    /// <param name="commandText">SQL字符串</param>
    /// <param name="commandParameters">SqlParameter[]对象</param>
    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
    {
        if (connection.State != ConnectionState.Open) connection.Open();
        command.Connection = connection;
        command.CommandText = commandText;
        if (transaction != null) command.Transaction = transaction;
        command.CommandType = commandType;
        if (commandParameters != null) AttachParameters(command, commandParameters);
        return;
    }
    /// <summary>
    /// 执行一个SQL，返回第一行的第一列
    /// </summary>
    /// <param name="connection">SqlConnection</param>
    /// <param name="commandType">CommandType</param>
    /// <param name="commandText">SQL</param>
    /// <param name="commandParameters">SqlParameter[]参数集</param>
    /// <returns>返回第一行的第一列</returns>
    public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);
            object retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }
    }
    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    public static object GetSingle(string SQLString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        connection.Close();
                        return null;
                    }
                    else
                    {
                        connection.Close();
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
            }
        }
    }
    /// <summary>
    /// 执行查询语句，返回SqlDataReader
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>SqlDataReader</returns>
    public static SqlDataReader ExecuteReader(string strSQL)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(strSQL, connection);
        try
        {
            connection.Open();
            SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return myReader;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            connection.Close();
            throw new Exception(e.Message);
        }

    }
    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataSet</returns>
    public static DataSet Query(string SQLString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            connection.Close();
            connection.Dispose();
            return ds;
        }
    }

    /// <summary>
    /// 执行查询语句，返回DataTable
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataTable</returns>
    public static DataTable QueryTable(string SQLString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            connection.Close();
            connection.Dispose();
            return ds.Tables[0];
        }
    }

    /// <summary>
    /// 执行查询语句，返回第一行第一列的值
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns></returns>
    public static string QueryValue(string SQLString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            connection.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return String.Empty;
            }
        }
    }

    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    connection.Close();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    connection.Close();
                    throw new Exception(E.Message);
                }
            }
        }
    }

    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    connection.Close();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
            }
        }
    }
    /// <summary>
    /// 执行查询语句，返回SqlDataReader
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>SqlDataReader</returns>
    public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            connection.Open();
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
            cmd.Parameters.Clear();
            connection.Close();
            connection.Dispose();
            return myReader;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            connection.Close();
            connection.Dispose();
            throw new Exception(e.Message);
        }

    }
    /// <param name="ConnectionString">一个有效的数据库连接字符串</param>
    /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
    /// <param name="commandText">存储过程名或SQL语句</param>
    /// <param name="commandParameters">SqlParamter参数数组(new DbParameter("@prodid", 24))</param>
    /// <returns>返回包含结果集的DbDataReader</returns>
    public static IDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("ConnectionString");
        SqlConnection connection = null;
        try
        {
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            return ExecuteReader(connection, null, commandType, commandText, commandParameters);
        }
        catch
        {
            // If we fail to return the SqlDatReader, we need to close the connection ourselves
            if (connection != null) connection.Close();
            throw;
        }

    }
    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataSet</returns>
    public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            //connection.Open();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                connection.Close();
                return ds;
            }
        }
    }
    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        if (trans != null)
            cmd.Transaction = trans;
        cmd.CommandType = CommandType.Text;//cmdType;
        if (cmdParms != null)
        {
            foreach (SqlParameter parm in cmdParms)
                cmd.Parameters.Add(parm);
        }
    }

    public static void PrepareCmd(SqlParameter[] parameters, SqlCommand cmd)
    {
        if (parameters != null)
        {
            foreach (SqlParameter item in parameters)
            {
                cmd.Parameters.Add(item);
            }
        }
    }

    #region 存储过程操作

    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>SqlDataReader</returns>
    public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlDataReader returnReader;
        connection.Open();
        SqlCommand command = BuildQueryCommand(storedProcName, parameters);
        command.CommandType = CommandType.StoredProcedure;
        returnReader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
        connection.Close();
        return returnReader;
    }


    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="tableName">DataSet结果中的表名</param>
    /// <returns>DataSet</returns>
    public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
    {

        string sdgfg = connectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            DataSet dataSet = new DataSet();
            connection.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = BuildQueryCommand(storedProcName, parameters);
            sqlDA.Fill(dataSet, tableName);
            connection.Close();
            return dataSet;
        }
    }


    /// <summary>
    /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>SqlCommand</returns>
    private static SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(storedProcName, connection);
        connection.Open();
        command.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter parameter in parameters)
        {
            command.Parameters.Add(parameter);
        }
        connection.Close();
        return command;
    }

    /// <summary>
    /// 执行存储过程，返回影响的行数		
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="rowsAffected">影响的行数</param>
    /// <returns></returns>
    public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            int result;
            connection.Open();
            SqlCommand command = BuildIntCommand(storedProcName, parameters);
            command.Connection = connection;
            rowsAffected = command.ExecuteNonQuery();
            result = (int)command.Parameters["ReturnValue"].Value;
            connection.Close();
            return result;
        }
    }

    /// <summary>
    /// 执行存储过程，返回影响的行数		
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="rowsAffected">影响的行数</param>
    /// <returns></returns>
    public static int RunProcedures(string storedProcName, IDataParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            int result;
            connection.Open();
            SqlCommand command = BuildIntCommand(storedProcName, parameters);
            command.Connection = connection;
            result = command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }

    /// <summary>
    /// 执行存储过程，返回指定的OUTPUT参数（ll）		
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="returnValueName">输出参数的名称</param>
    /// <returns></returns>
    public static object ReturnProcedureValue(string storedProcName, IDataParameter[] parameters, string returnValueName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = BuildIntCommand(storedProcName, parameters);
            command.Connection = connection;
            command.ExecuteNonQuery();
            if (returnValueName != "")
            {
                object result = command.Parameters[returnValueName].Value;
                connection.Close();
                return result;
            }
            else
            {
                object result = command.Parameters["ReturnValue"].Value;
                connection.Close();
                return result;
            }
        }
    }
    #endregion
    #region 生成参数
    /// <summary>
    /// 生成参数
    /// </summary>
    /// <param name="ParamName"></param>
    /// <param name="DbType"></param>
    /// <param name="Size"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
    }
    public static SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ParamName"></param>
    /// <param name="DbType"></param>
    /// <param name="Size"></param>
    /// <param name="Direction"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
    {
        SqlParameter param;
        param = new SqlParameter(ParamName, DbType, Size);
        param.Direction = Direction;
        if (!(Direction == ParameterDirection.Output && Value == null))
            param.Value = Value;

        return param;
    }
    #endregion 生成参数结束
    /// <summary>
    /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>SqlCommand 对象实例</returns>
    private static SqlCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = BuildQueryCommand(storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            connection.Close();
            return command;
        }
    }
    /// <summary>
    /// 执行指定数据库连接对象的命令,指定参数值,返回结果集中的第一行第一列.
    /// </summary>
    /// <param name="SQLstring"></param>
    /// <returns></returns>
    private static object ExecuteScalar(string SQLstring)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQLstring, connection))
            {
                try
                {
                    connection.Open();
                    cmd.CommandType = CommandType.Text;
                    object obj = cmd.ExecuteScalar();
                    connection.Close();
                    return obj;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    connection.Close();
                    throw new Exception(E.Message);
                }
            }
        }
    }

    /// <summary>  
    /// 批量导入datatable  
    /// </summary>  
    /// <param name="dt">数据表</param>  
    /// <param name="TabelName">数据表名</param>  
    /// <param name="dtColum">列集合</param>  
    public static int InsertTable(DataTable dt, string TabelName, DataColumnCollection ydtColum, DataColumnCollection execldtColum)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //声明SqlBulkCopy ,using释放非托管资源  
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(connection))
                {
                    connection.Open();
                    //一次批量的插入的数据量  
                    //sqlBC.BatchSize = 1000;  
                    //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除  
                    //sqlBC.BulkCopyTimeout = 60;  

                    //設定 NotifyAfter 属性，以便在每插入10000 条数据时，呼叫相应事件。   
                    //sqlBC.NotifyAfter = 10000;  
                    // sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);  

                    //设置要批量写入的表  
                    sqlBC.DestinationTableName = TabelName;

                    //自定义的datatable和数据库的字段进行对应  

                    for (int i = 0; i < execldtColum.Count; i++)
                    {
                        sqlBC.ColumnMappings.Add(i, i + 1);
                    }
                    //批量写入  
                    sqlBC.WriteToServer(dt);
                }
                connection.Close();
                //导入成功，返回1  
                return 1;
            }
        }
        catch
        {
            //导入失败返回-1  
            return -1;

        }

    }
}