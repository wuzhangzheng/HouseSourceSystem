using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace build
{
    public class DataBase
    {
        public SqlConnection getConnection()
        {
            return new SqlConnection("server=.;uid=sa;pwd=;database=build");
        }
        SqlConnection con = null;
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回一个具体值</returns>
        public object QueryScalar(string sql)
        {
            open();
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    return cmd.ExecuteScalar();
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 执行SQL语句,带参数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        public object QueryScalar(string sql, SqlParameter[] prams)
        {
            open();
            try
            {
                using (SqlCommand cmd = CreateCommandSql(sql, prams))
                {
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 要执行SQL语句,该方法返回一个DATATABLE
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataTable Query (string sql)
        {
            open();
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))
            {
                using (DataTable dt=new DataTable ())
                {
                    sda.Fill(dt);
                    return dt;
                }
            }            
        }
        /// <summary>
        /// 执行SQL语句,带参数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        public DataTable Query(string sql,SqlParameter[] prams)
        {
            open();
            SqlCommand cmd = CreateCommandSql(sql, prams);
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
        /// <summary>
        /// 返回影响记录的行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的行数</returns>
        public int RunSql(string sql)
        {
            open();            
            try
            {
                using (SqlCommand cmd =new SqlCommand (sql,con))
                {
                  return  cmd.ExecuteNonQuery();
                }
            }
            catch 
            {
                return 0;
            }
        }
        /// <summary>
        /// 返回影响记录的行数，带参数的
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        public int RunSql(string sql,SqlParameter[] prams)
        {
            open();
            SqlCommand cmd = CreateCommandSql(sql, prams);
            try
            {
                    return cmd.ExecuteNonQuery();          
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 创建sqlcommand对象用来构建sql语句
        /// </summary>
        /// <param name="sql">sql语句param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        private SqlCommand CreateCommandSql(string sql, SqlParameter[] prams)
        {
            open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if (prams != null)
            {
                foreach (var item in prams)
                {
                    cmd.Parameters.Add(item);
                }
            }
            return cmd;
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        private void open()
        {
            if (con == null)
            {
                con = new SqlConnection("server=.;uid=sa;pwd=;database=build");
            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
    }
}
