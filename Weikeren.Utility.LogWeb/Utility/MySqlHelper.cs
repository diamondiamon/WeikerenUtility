using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.LogWeb.Utility
{
    public class WKRMySqlHelper
    {
        /// <summary>
        ///  给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return MySqlHelper.ExecuteNonQuery(connectionString, cmdText, commandParameters);

            //MySqlCommand cmd = new MySqlCommand();

            //using (MySqlConnection conn = new MySqlConnection(connectionString))
            //{
            //    PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
            //    int val = cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();
            //    return val;
            //}
        }


        ///// <summary>
        ///// 准备执行一个命令
        ///// </summary>
        ///// <param name="cmd">sql命令</param>
        ///// <param name="conn">OleDb连接</param>
        ///// <param name="trans">OleDb事务</param>
        ///// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        ///// <param name="cmdText">命令文本,例如:Select * from Products</param>
        ///// <param name="cmdParms">执行命令的参数</param>
        //private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        //{

        //    if (conn.State != ConnectionState.Open)
        //        conn.Open();

        //    cmd.Connection = conn;
        //    cmd.CommandText = cmdText;

        //    if (trans != null)
        //        cmd.Transaction = trans;

        //    cmd.CommandType = cmdType;

        //    if (cmdParms != null)
        //    {
        //        foreach (MySqlParameter parm in cmdParms)
        //            cmd.Parameters.Add(parm);
        //    }
        //}


    }
}