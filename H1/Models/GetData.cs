using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace H1.Models
{
    public class GetData
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["aaa"].ConnectionString);
        SqlDataAdapter adp = new SqlDataAdapter("", conn);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public DataTable querySql(string sql,CommandType cmt)
        {
            adp.SelectCommand.CommandType = cmt;
            adp.SelectCommand.CommandText = sql;
            adp.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }
        public DataTable querySql(string sql, CommandType cmt,string dtName)
        {
            adp.SelectCommand.CommandType = cmt;
            adp.SelectCommand.CommandText = sql;
            adp.Fill(ds, dtName);
            dt = ds.Tables[dtName];
            return dt;
        }

        public DataTable querySql(string sql, CommandType cmt,List<SqlParameter> para)
        {
            foreach (SqlParameter p in para) 
            {
                adp.SelectCommand.Parameters.Add(p);
            }
            adp.SelectCommand.CommandType = cmt;
            adp.SelectCommand.CommandText = sql;
            adp.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }
    }
}