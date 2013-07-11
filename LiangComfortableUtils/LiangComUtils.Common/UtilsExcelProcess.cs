using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Data.OleDb;

namespace LiangComUtils.Common
{
    public class UtilsExcelProcess
    {
        public static void ToExcel(System.Web.UI.WebControls.DataGrid DataGrid2Excel,string FileName,string Title)
		{
			ToExcel(DataGrid2Excel, FileName, Title, "");
		}

		/// <summary>
		/// Renders the html text before the datagrid.
		/// </summary>
		/// <param name="writer">A HtmlTextWriter to write html to output stream</param>
		private static void FrontDecorator(HtmlTextWriter writer)
		{
			writer.WriteFullBeginTag("HTML");
			writer.WriteFullBeginTag("Head");

			writer.WriteEndTag("Head");
			writer.WriteFullBeginTag("Body");
		}

		/// <summary>
		/// Renders the html text after the datagrid.
		/// </summary>
		/// <param name="writer">A HtmlTextWriter to write html to output stream</param>
		private static void RearDecorator(HtmlTextWriter writer)
		{
			writer.WriteEndTag("Body");
			writer.WriteEndTag("HTML");
		}

		public static void ToExcel(System.Web.UI.WebControls.DataGrid DataGrid2Excel,string FileName,string Title, string Head)
		{
			System.IO.StringWriter sw = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

			FrontDecorator(hw);
			if ( Title != "")
				hw.Write(Title + "<br>");
			if ( Head != "")
				hw.Write(Head + "<br>");
			
			DataGrid2Excel.EnableViewState = false;
			DataGrid2Excel.RenderControl(hw);

			RearDecorator(hw);
   
			System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
			response.Clear();
			response.Buffer = true;
            response.ContentEncoding = System.Text.Encoding.Default;
			response.ContentType ="application/Excel";
			response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
			response.Charset = "gb2312";
			response.Write(sw.ToString());
			response.End();
		}

        public static void ToExcel(DataTable dt , string FileName)
        {
            System.Web.UI.WebControls.DataGrid dgTemp = new System.Web.UI.WebControls.DataGrid();
            dgTemp.DataSource = dt;
            dgTemp.DataBind();
            ToExcel(dgTemp, FileName, "", "");            
        }

        public static string DataTableToExcel(DataTable dt, string excelName, string savePhysicPath)
        {
            if (dt == null)
            {
                return "DataTable不能为空";
            }

            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            StringBuilder sb;

            if (rows == 0)
            {
                return "没有数据";
            }

            sb = new StringBuilder();

            //string physicPath = AppConfig.ErrorLogFolder;
            string physicPath = @"C:\Temp\";
            physicPath = savePhysicPath;

            if (System.IO.File.Exists(physicPath + excelName))
                System.IO.File.Delete(physicPath + excelName);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + physicPath + excelName + ";Extended Properties=Excel 8.0;";


            //生成创建表的脚本
            sb.Append("CREATE TABLE ");
            sb.Append(dt.TableName + " ( ");

            for (int i = 0; i < cols; i++)
            {
                string oleColumnType = dt.Columns[i].DataType.Name;

                if (dt.Columns[i].DataType == System.Type.GetType("System.Decimal"))
                    oleColumnType = "decimal";
                else if (dt.Columns[i].DataType == System.Type.GetType("System.Int32")
                    || dt.Columns[i].DataType == System.Type.GetType("System.Int")
                    )

                    oleColumnType = "int";

                if (i < cols - 1)
                    sb.Append(string.Format("{0} " + oleColumnType + ",", dt.Columns[i].ColumnName));
                else
                    sb.Append(string.Format("{0} " + oleColumnType + ")", dt.Columns[i].ColumnName));
            }

            using (OleDbConnection objConn = new OleDbConnection(connString))
            {
                OleDbCommand objCmd = new OleDbCommand();
                objCmd.Connection = objConn;

                objCmd.CommandText = sb.ToString();

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return "在Excel中创建表失败，错误信息：" + e.Message;
                }

                #region 生成插入数据脚本
                sb.Remove(0, sb.Length);
                sb.Append("INSERT INTO ");
                sb.Append(dt.TableName + " ( ");

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append(dt.Columns[i].ColumnName + ",");
                    else
                        sb.Append(dt.Columns[i].ColumnName + ") values (");
                }

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append("@" + dt.Columns[i].ColumnName + ",");
                    else
                        sb.Append("@" + dt.Columns[i].ColumnName + ")");
                }
                #endregion


                //建立插入动作的Command
                objCmd.CommandText = sb.ToString();
                OleDbParameterCollection param = objCmd.Parameters;

                for (int i = 0; i < cols; i++)
                {
                    param.Add(new OleDbParameter("@" + dt.Columns[i].ColumnName, OleDbType.VarChar));
                }

                //遍历DataTable将数据插入新建的Excel文件中
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        param[i].Value = row[i];
                    }

                    objCmd.ExecuteNonQuery();
                }

                return "数据已成功导入Excel";
            }//end using
        }
    }
}
