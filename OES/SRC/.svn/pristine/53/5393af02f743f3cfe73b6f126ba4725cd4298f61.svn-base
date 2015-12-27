using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;


    public class DataToJson
    {
        /// <summary>
        /// 把表的指定行转换成json字符串，字符串如{"属性名"：值，"属性名"：值，"属性名"：值……}
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public static string Getjson(DataTable dt, int rowNumber)
        {
            StringBuilder json = new StringBuilder();      
            int s=rowNumber;
                DataRow rows = dt.Rows[s];
                json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = dt.Columns[j].ColumnName;
                    string columnType = dt.Columns[j].DataType.Name;
                    if (columnType == "Int32" || columnType == "Int16" || columnType == "Decimal")
                    {
                        json.AppendFormat("\"{0}\":\"{1}\"", columnName, rows.IsNull(columnName) ? "" : rows[columnName]);
                    }
                    else if (columnType == "Boolean")
                    {
                        json.AppendFormat("\"{0}\":{1}", columnName, rows.IsNull(columnName) ? "" : rows[columnName].ToString().ToLower());
                    }
                    else
                    {
                        json.AppendFormat("\"{0}\":\"{1}\"", columnName, rows[columnName]);
                    }

                    if (j < dt.Columns.Count - 1) json.Append(","); // add comma if not last column
                }
                json.Append("}");        
            return json.ToString();


        }
        /// <summary>
        ///ajax调用 获取数据 转换json 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string getjson(DataTable dt)
        {

            StringBuilder json = new StringBuilder();
            json.Append("[");
            for (int s = 0; s < dt.Rows.Count; s++)
            {
                DataRow rows = dt.Rows[s];
                json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = dt.Columns[j].ColumnName;
                    string columnType = dt.Columns[j].DataType.Name;

                    if (columnType == "Int32" || columnType == "Int16" || columnType == "Decimal")
                    {
                        json.AppendFormat("\"{0}\":\"{1}\"", columnName, rows.IsNull(columnName) ? "" : rows[columnName]);
                    }
                    else if (columnType == "Boolean")
                    {
                        json.AppendFormat("\"{0}\":{1}", columnName, rows.IsNull(columnName) ? "" : rows[columnName].ToString().ToLower());
                    }
                    else
                    {
                        json.AppendFormat("\"{0}\":\"{1}\"", columnName, rows[columnName]);
                    }

                    if (j < dt.Columns.Count - 1) json.Append(","); // add comma if not last column
                }
                json.Append("}");

                if (s < dt.Rows.Count - 1) json.Append(","); // add comma if not last row
            }
            json.Append("]");
            return json.ToString();
        }

        /// <summary>
        /// 获得分页sql
        /// </summary>
        /// <param name="row">每页的条数</param>
        /// <param name="page">当前页数</param>
        /// <param name="table">表</param>
        /// <returns></returns>
        public static string sqlpage(int row, int page, string table)
        {
            string sql = "SELECT TOP " + row + " *  FROM (SELECT ROW_NUMBER() OVER (ORDER BY UID DESC) AS RowNumber,* FROM " + table + ") A WHERE RowNumber > " + row + "*(" + page + "-1) ";
            return sql;
        }
        /// <summary>
        /// 获得分页sql
        /// </summary>
        /// <param name="row">每页的条数</param>
        /// <param name="page">当前页数</param>
        /// <param name="table">表</param>
        /// <returns></returns>
        public static string sqlpage(int row, int page, string table, string strwhere)
        {
            strwhere = strwhere.Trim();
            if (strwhere.ToLower().StartsWith("and")) strwhere = strwhere.Substring(3);//
            if (strwhere.ToLower().StartsWith("where")) strwhere = strwhere.Substring(5);
            string sql = "SELECT TOP " + row + " *  FROM (SELECT ROW_NUMBER() OVER (ORDER BY id) AS RowNumber,* FROM " + table + " where " + strwhere + ") A WHERE RowNumber > " + row + "*(" + page + "-1) " + "";
            return sql;
        }
      /// <summary>
        ///  获得分页sql
      /// </summary>
        /// <param name="row">每页的条数</param>
        /// <param name="page">当前页数</param>
      /// <param name="table">表面</param>
      /// <param name="strwhere">判定条件</param>
      /// <param name="orderby">排序规则</param>
      /// <returns></returns>
        public static string sqlpage1(int row, int page, string table, string strwhere,string orderby)
        {
            string sql = @"SELECT * FROM  (SELECT  ROW_NUMBER() OVER ( ORDER BY " + orderby + @" ) AS RowNum, *
                                          FROM   " + table + strwhere + " ) AS RowConstrainedResult WHERE   RowNum >="
                                                   +row*(page-1)+" AND RowNum < "+row*page+"ORDER BY RowNum";
            return sql;
        }
        /// <summary>
        /// 获取分页查询SQL
        /// </summary>
        /// <param name="row">每页行数</param>
        /// <param name="page">第几页</param>
        /// <param name="table">表名</param>
        /// <param name="strOrderby">排序条件，分页必须</param>
        /// <param name="strwhere">条件,以where开头</param>
        /// <returns>指定页与行的查询sql语句</returns>
        public static string sqlpage(int row, int page, string table, string strOrderby, string strwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT  * FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY  ");
            sql.Append(strOrderby);
            sql.Append("  ) AS RowNum, *    FROM  ");
            sql.Append(table);
            sql.Append("  ");
            sql.Append(strwhere);
            sql.Append(" ) AS RowConstrainedResult WHERE   RowNum >");
            sql.Append(row * (page - 1));
            sql.Append(" AND RowNum <= ");
            sql.Append(row * page);
            sql.Append(" ORDER BY RowNum");       
            return sql.ToString();
        }
        /// <summary>
        /// DataTable转换Json 用于EasyUI:DataGrid
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableJson(DataTable dt)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.AppendFormat("\"total\":{0}, ", dt.Rows.Count);
            jsonBuilder.Append("\"rows\":[ ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// DataTable转换Json 用于EasyUI:DataGrid 表和总数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="counts">总数</param>
        /// <returns></returns>
        public static string DataTableJson(DataTable dts, int counts)
        {
            DataTable dt = dts;
            //dt.Columns.Remove("Contents");
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.AppendFormat("\"total\":{0}, ", counts);
            jsonBuilder.Append("\"rows\":[ ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");

                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// DataTable转换Json 用于EasyUI:DataGrid 表和总数去掉content调用此方法:带参数表示不包含内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="counts">总数</param>
        /// <param name="why">是否保留内容部分</param>
        /// <returns></returns>
        /// 
        public static string DataTableJson(DataTable dts, int counts, string why)
        {
            DataTable dt = dts;
            dt.Columns.Remove(why);
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.AppendFormat("\"total\":{0}, ", counts);
            jsonBuilder.Append("\"rows\":[ ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        /// <summary>
        ///列表 递归将DataTable转化为适合jquery easy ui 控件tree ,combotree 的 json
        /// 该方法最后还要 将结果稍微处理下,将最前面的,"children" 字符去掉.
        /// </summary>
        /// <param name="dt">要转化的表</param>
        /// <param name="pField">表中的父节点字段</param>
        /// <param name="pValue">表中顶层节点的值,没有 可以输入为0</param>
        /// <param name="kField">关键字字段名称</param>
        /// <param name="TextField">要显示的文本 对应的字段</param>
        /// <returns></returns>
        public static string TableToEasyUITreeJson(DataTable dt, string pField, string pValue, string kField, string TextField)
        {
            StringBuilder sb = new StringBuilder();
            string filter = string.Empty;
            if (pValue.ToString() == "")
            {
                filter = string.Format("{0} is null", pField);
            }
            else
            {
                filter = string.Format("{0}='{1}'", pField, pValue);
            }
            DataRow[] drs = dt.Select(filter);
            if (drs.Length < 1)
                return "";
            sb.Append(",\"children\":[");
            foreach (DataRow dr in drs)
            {
                string pcv = dr[kField].ToString();
                sb.Append("{");
                sb.AppendFormat("\"id\":\"{0}\",", dr[kField].ToString());
                sb.AppendFormat("\"text\":\"{0}\",", dr[TextField].ToString());
                sb.AppendFormat("\"iconCls\":\"{0}\"", dr["iconCls"].ToString());
                sb.Append(TableToEasyUITreeJson(dt, pField, pcv, kField, TextField).TrimEnd(','));
                sb.Append("},");
            }
            if (sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            return sb.ToString();

        }

    /// <summary>
    /// 个人菜单 递归将DataTable转化为适合jquery easy ui 控件tree ,combotree 的 json
    /// 该方法最后还要 将结果稍微处理下,将最前面的,"children" 字符去掉.
    /// </summary>
    /// <param name="dt">要转化的表</param>
    /// <param name="pField">表中的父节点字段</param>
    /// <param name="pValue">表中顶层节点的值,没有 可以输入为0</param>
    /// <param name="kField">关键字字段名称</param>
    /// <param name="TextField">要显示的文本 对应的字段</param>
    /// <returns></returns>
    public static string TableToEasyUITreeJson(DataTable dt, string pField, string pValue, string kField, string TextField, string Menu)
    {
        StringBuilder sb = new StringBuilder();
        string filter = string.Empty;
        if (pValue.ToString() == "")
        {
            filter = string.Format("{0} is null", pField);
        }
        else
        {
            filter = string.Format("{0}='{1}'", pField, pValue);
        }
        //string filter = String.Format(" {0}='{1}' ", pField, pValue);//获取顶级目录.
        DataRow[] drs = dt.Select(filter);
        if (drs.Length < 1)
            return "";
        sb.Append(",\"children\":[");
        foreach (DataRow dr in drs)
        {
            string pcv = dr[kField].ToString();
            sb.Append("{");
            sb.AppendFormat("\"id\":\"{0}\",", dr[kField].ToString());
            sb.AppendFormat("\"text\":\"{0}\",", dr[TextField].ToString());
            sb.AppendFormat("\"attributes\":\"{0}\",", dr["Url"].ToString());
            sb.AppendFormat("\"iconCls\":\"{0}\"", dr["iconCls"].ToString());
            sb.Append(TableToEasyUITreeJson(dt, pField, pcv, kField, TextField, Menu).TrimEnd(','));
            sb.Append("},");
        }
        if (sb.ToString().EndsWith(","))
        {
            sb.Remove(sb.Length - 1, 1);
        }
        sb.Append("]");
        return sb.ToString();
    }


    public static string TableToBootstrapTreeJson(DataTable dt, string pField, string pValue, string kField, string TextField, string Menu)
    {
        StringBuilder sb = new StringBuilder();
        string filter = string.Empty;
        if (pValue.ToString() == "")
        {
            filter = string.Format("{0} is null", pField);
        }
        else
        {
            filter = string.Format("{0}='{1}'", pField, pValue);
        }
        //string filter = String.Format(" {0}='{1}' ", pField, pValue);//获取顶级目录.
        DataRow[] drs = dt.Select(filter);
        if (drs.Length < 1)
            return "";
        sb.Append(",\"nodes\":[");
        foreach (DataRow dr in drs)
        {
            string pcv = dr[kField].ToString();
            sb.Append("{");
            sb.AppendFormat("\"text\":\"{0}\",", dr[TextField].ToString());
            sb.Append(TableToEasyUITreeJson(dt, pField, pcv, kField, TextField, Menu).TrimEnd(','));
            sb.Append("},");
        }
        if (sb.ToString().EndsWith(","))
        {
            sb.Remove(sb.Length - 1, 1);
        }
        sb.Append("]");
        return sb.ToString();
    }

}
