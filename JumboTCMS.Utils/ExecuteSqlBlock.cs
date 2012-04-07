/*
 * 程序中文名称: 将博内容管理系统通用版
 * 
 * 程序英文名称: JumboTCMS
 * 
 * 程序版本: 5.2.X
 * 
 * 程序编写: 随风缘 (定制开发请联系：jumbot114#126.com,不接受免费的技术答疑,请见谅)
 * 
 * 官方网站: http://www.jumbotcms.net/
 * 
 * 商业服务: http://www.jumbotcms.net/about/service.html
 * 
 */

using System;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
namespace JumboTCMS.Utils
{
    public static class ExecuteSqlBlock
    {
        /// <summary>
        /// 执行Sql脚本块
        /// </summary>
        /// <param name="dbType">0为access,1为sqlserver</param>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="pathToScriptFile">脚本路径，物理路径</param>
        /// <returns></returns>
        public static bool Go(string dbType, string connectionString, string pathToScriptFile)
        {
            StreamReader _reader = null;
            Stream stream = null;
            if (!System.IO.File.Exists(pathToScriptFile))
                return false;
            try
            {

                string sql = "";
                stream = System.IO.File.OpenRead(pathToScriptFile);
                _reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                if (dbType == "0")//执行access
                {
                    using (OleDbConnection conn0 = new OleDbConnection(connectionString))
                    {
                        using (OleDbCommand command0 = new OleDbCommand())
                        {
                            conn0.Open();
                            command0.Connection = conn0;
                            command0.CommandType = System.Data.CommandType.Text;
                            while (null != (sql = ReadNextStatementFromStream(_reader)))
                            {
                                command0.CommandText = sql;
                                command0.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else//执行sqlserver
                {
                    using (SqlConnection conn1 = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command1 = new SqlCommand())
                        {
                            conn1.Open();
                            command1.Connection = conn1;
                            command1.CommandType = System.Data.CommandType.Text;
                            while (null != (sql = ReadNextStatementFromStream(_reader)))
                            {
                                command1.CommandText = sql;
                                command1.ExecuteNonQuery();
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _reader.Close();
                _reader.Dispose();
                stream.Close();
                stream.Dispose();
            }
        }
        private static string ReadNextStatementFromStream(StreamReader _reader)
        {
            StringBuilder sb = new StringBuilder();
            string lineOfText;
            while (true)
            {
                lineOfText = _reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                    {
                        return sb.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                if (lineOfText.TrimEnd().ToUpper() == "GO")
                {
                    break;
                }
                sb.AppendFormat("{0}\r\n", lineOfText);
            }
            return sb.ToString();
        }
    }
}
