using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web;

namespace ConfigAndElse
{
    /// <summary>
    /// Copyright (C)   Liang
    /// FileName:        ExcelIO.cs       
    /// Author :         gaoyj
    /// CDT:             2013年6月28日22:35:05         
    /// Version:         1.0.0.0                
    /// Depiction:       EXCEL文件操作    
    /// </summary>
    public class ExcelIO
    {
        /// <summary>
        /// 写EXCEL
        /// </summary>
        /// <param name="positionMap"></param>
        /// <param name="tableInfoMap"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string writeCDETableTemplate(Hashtable positionMap, Hashtable tableInfoMap, System.Data.DataTable dataTable)
        {
            string tmpFile = "";
            if (positionMap == null || tableInfoMap == null)
            {
                return "";
            }

            //创建Application对象 
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlsApp == null)
            {
                return "";
            }

            xlsApp.Visible = false;

            try
            {
                //得到WorkBook对象, 打开已有的文件
                Microsoft.Office.Interop.Excel.Workbook xlsBook = xlsApp.Workbooks.Open(positionMap["FILE_PATH"].ToString(), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                //指定要操作的Sheet
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[1];

                //在单元格中写入数据
                Microsoft.Office.Interop.Excel.Range rTableName = xlsSheet.get_Range(positionMap["TABLENAME_POSITION"].ToString(), Type.Missing);
                rTableName.Value2 = tableInfoMap["TABLE_NAME"];

                Microsoft.Office.Interop.Excel.Range rTableLabel = xlsSheet.get_Range(positionMap["TABLELABEL_POSITION"].ToString(), Type.Missing);
                rTableLabel.Value2 = tableInfoMap["TABLE_LABEL"];

                Microsoft.Office.Interop.Excel.Range rTableDescription = xlsSheet.get_Range(positionMap["TABLEDESCRIPTION_POSITION"].ToString(), Type.Missing);
                rTableDescription.Value2 = tableInfoMap["TABLE_DESCRIPTION"];

                Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.get_Range(positionMap["TABLEDATA_START_POSITION"].ToString(), Type.Missing);

                dataRange.get_Resize(dataTable.Rows.Count, 10);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    int j = i + 1;
                    dataRange.Cells[j, 1] = j;
                    dataRange.Cells[j, 2] = dataTable.Rows[i]["LABEL"];
                    dataRange.Cells[j, 3] = dataTable.Rows[i]["NAME"];
                    dataRange.Cells[j, 4] = dataTable.Rows[i]["TYPE"];
                    dataRange.Cells[j, 5] = dataTable.Rows[i]["F_LEN"];
                    dataRange.Cells[j, 6] = dataTable.Rows[i]["CLM_KEY"];
                    dataRange.Cells[j, 7] = dataTable.Rows[i]["IS_NOT_NULL"];
                    //dataRange.Cells[j, 8] = dataTable.Rows[i][""];
                    //dataRange.Cells[j, 9] = dataTable.Rows[i][""];
                    dataRange.Cells[j, 10] = dataTable.Rows[i]["DESCRIPTION"];
                }

                tmpFile = positionMap["FILE_PATH"].ToString() + "tmp.xls";

                //保存，关闭
                if (File.Exists(tmpFile))
                {
                    File.Delete(tmpFile);
                }
                xlsBook.SaveAs(tmpFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlsBook.Close(false, Type.Missing, Type.Missing);

            }
            catch (Exception ex)
            {
               
                SaveNote(ex.ToString());
            }
            finally
            {
                xlsApp.Quit();
                GC.Collect();
            }
            return tmpFile;
        }

        /// <summary>
        /// 读EXCEL文件
        /// </summary>
        /// <param name="positionMap"></param>
        /// <param name="tableInfoMap"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public bool readCDETableFile(Hashtable positionMap, Hashtable tableInfoMap, System.Data.DataTable dataTable)
        {
            if (positionMap == null || tableInfoMap == null)
            {
                return false;
            }

            //创建Application对象 
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlsApp == null)
            {
                return false;
            }

            xlsApp.Visible = false;

            try
            {
                //得到WorkBook对象, 打开已有的文件
                Microsoft.Office.Interop.Excel.Workbook xlsBook = xlsApp.Workbooks.Open(positionMap["FILE_PATH"].ToString(), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                //指定要操作的Sheet
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[1];

                //在单元格中读出数据
                Microsoft.Office.Interop.Excel.Range rTableName = xlsSheet.get_Range(positionMap["TABLENAME_POSITION"].ToString(), Type.Missing);
                tableInfoMap.Add("TABLE_NAME", rTableName.Value2);

                Microsoft.Office.Interop.Excel.Range rTableLabel = xlsSheet.get_Range(positionMap["TABLELABEL_POSITION"].ToString(), Type.Missing);
                tableInfoMap.Add("TABLE_LABEL", rTableLabel.Value2);

                Microsoft.Office.Interop.Excel.Range rTableDescription = xlsSheet.get_Range(positionMap["TABLEDESCRIPTION_POSITION"].ToString(), Type.Missing);
                tableInfoMap.Add("TABLE_DESCRIPTION", rTableDescription.Value2);

                Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.get_Range(positionMap["TABLEDATA_START_POSITION"].ToString(), Type.Missing);
                dataRange.get_Resize(xlsSheet.Rows.Count - 30, 10);

                for (int i = 1; true; i++)
                {
                    if (((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 3]).Value2 == null || String.IsNullOrEmpty(((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 3]).Value2.ToString()))
                    {
                        break;
                    }

                    System.Data.DataRow dr = dataTable.NewRow();

                    dr["LABEL"] = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 2]).Value2;
                    dr["NAME"] = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 3]).Value2;
                    dr["TYPE"] = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 4]).Value2;
                    dr["F_LEN"] = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 5]).Value2;
                    dr["CLM_KEY"] = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 6]).Value2;
                    dr["IS_NOT_NULL"] = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 7]).Value2;
                    //dr[""] = ((Excel.Range)dataRange.Cells[i, 8]).Value2;
                    //dr[""] = ((Excel.Range)dataRange.Cells[i, 9]).Value2;
                    dr["DESCRIPTION"] = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i, 10]).Value2;

                    dataTable.Rows.Add(dr);
                }

                xlsBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {

                SaveNote(ex.ToString());
            }
            finally
            {
                xlsApp.Quit();
                GC.Collect();
            }
            return true;
        }

        /// <summary>
        /// TableData写入Excel
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public bool writeTableDataToExcel_bak(string templateFileName, System.Data.DataTable dataTable)
        {

            if (String.IsNullOrEmpty(templateFileName))
            {
                return false;
            }

            //创建Application对象 
            Microsoft.Office.Interop.Excel.Application xlsApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlsBook = null;
            try
            {
                xlsApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlsApp == null)
                {
                    return false;
                }

                xlsApp.Visible = false;

                //得到WorkBook对象, 打开已有的文件
                xlsBook = xlsApp.Workbooks.Add(Missing.Value);

                while (xlsBook.Sheets.Count > 1)
                {
                    Microsoft.Office.Interop.Excel.Worksheet delSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[xlsBook.Sheets.Count];
                    delSheet.Delete();
                }
                //指定要操作的Sheet
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[1];

                Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.get_Range("A1", Type.Missing);
                //最大65536行
                int rowsCount = dataTable.Rows.Count;
                if (rowsCount > 65536)
                {
                    rowsCount = 65536 - 2;
                }
                dataRange.get_Resize(rowsCount, dataTable.Columns.Count);

                for (int i = 0; i < rowsCount; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[i + 1, j + 1]).NumberFormatLocal = "@";
                        dataRange.Cells[i + 1, j + 1] = dataTable.Rows[i][j].ToString();

                    }
                }

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[1, i + 1]).Interior.Color = Color.LightGray.ToArgb();//单格颜色
                    ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[2, i + 1]).Interior.Color = Color.LightGray.ToArgb();//单格颜色
                    //((Excel.Range)dataRange.Cells[3, i + 1]).Interior.Color = Color.LightGray.ToArgb();//单格颜色

                }
                xlsSheet.get_Range(1 + ":" + 1, Type.Missing).Rows.RowHeight = 0;

                //xlsSheet.get_Range(colName + rowIndex.ToString(), Type.Missing).Rows.RowHeight = 40;
                //xlsSheet.get_Range(colName + rowIndex.ToString(), Type.Missing).Columns.Interior.Color = Color.Blue.ToArgb();//单格颜色



                //保存，关闭
                xlsBook.SaveAs(templateFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlsBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                xlsBook.Close(false, Type.Missing, Type.Missing);

                SaveNote(ex.ToString());
            }
            finally
            {
                xlsApp.Quit();
                GC.Collect();
            }
            if (xlsApp != null) xlsApp.Quit();
            return true;
        }

        /// <summary>
        /// TableData写入Excel 
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public bool writeTableDataToExcel(string templateFileName, System.Data.DataTable dataTable)
        {
            if (String.IsNullOrEmpty(templateFileName))
            {
                return false;
            }

            //创建Application对象 
            Microsoft.Office.Interop.Excel.Application xlsApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlsBook = null;
            try
            {
                xlsApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlsApp == null)
                {
                    return false;
                }

                xlsApp.Visible = false;

                //得到WorkBook对象, 打开已有的文件
                xlsBook = xlsApp.Workbooks.Add(Missing.Value);

                while (xlsBook.Sheets.Count > 1)
                {
                    Microsoft.Office.Interop.Excel.Worksheet delSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[xlsBook.Sheets.Count];
                    delSheet.Delete();
                }
                //指定要操作的Sheet
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[1];

                // 设置标题
                //Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1,1],xlApp.Cells[1,ts.GridColumnStyles.Count]);
                //range.MergeCells = true;
                //xlApp.ActiveCell.FormulaR1C1 = p_ReportName;
                //xlApp.ActiveCell.Font.Size = 20;
                //xlApp.ActiveCell.Font.Bold = true;
                //xlApp.ActiveCell.HorizontalAlignment = Excel.Constants.xlCenter;

                //最大65536行
                int colIndex = 0;
                int RowIndex = 0;
                int RowCount = dataTable.Rows.Count;
                int colCount = dataTable.Columns.Count;
                if (RowCount > 65536)
                {
                    RowCount = 65536;
                }


                // 创建缓存数据
                object[,] objData = new object[RowCount, colCount];

                // 获取数据
                for (RowIndex = 0; RowIndex < RowCount; RowIndex++)
                {
                    for (colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        objData[RowIndex, colIndex] = dataTable.Rows[RowIndex][colIndex];
                    }
                }

                //Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.get_Range(xlsApp.Cells[1, 1], xlsApp.Cells[RowCount, colCount]);
                //在VS2010中调用COM Interop DLL操作Excel通过get_Range去获取Range时，会发生Object does not contain a definition for get_Range的错误。
                //需要修改成以下形式就可以避免错误
                Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.Range[xlsApp.Cells[1, 1], xlsApp.Cells[RowCount, colCount]];

                dataRange.Cells.NumberFormatLocal = "@";
                
                dataRange.Value = objData;
                
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[1, i + 1]).Interior.Color = Color.LightGray.ToArgb();//单格颜色
                    ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[2, i + 1]).Interior.Color = Color.LightGray.ToArgb();//单格颜色

                }
                xlsSheet.get_Range(1 + ":" + 1, Type.Missing).Rows.RowHeight = 0;

                //保存，关闭
                xlsBook.Saved = true;
                xlsBook.SaveCopyAs(templateFileName);
                xlsBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
               
                //xlsBook.Close(false, Type.Missing, Type.Missing);
                //Console.WriteLine(ex.ToString());

                SaveNote(ex.ToString());
            }
            finally
            {
                xlsApp.Quit();
                GC.Collect();
            }
            xlsBook = null;
            if (xlsApp != null) xlsApp.Quit();
            return true;
        }


        /// <summary>
        /// 读Excel文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>DataTable</returns>
        public System.Data.DataTable readCDEDataFile(string filePath)
        {
            System.Data.DataTable dt = null;
            if (String.IsNullOrEmpty(filePath))
            {
                return null;
            }

            //创建Application对象 
            Microsoft.Office.Interop.Excel.Application xlsApp = null;

            try
            {
                xlsApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlsApp == null)
                {
                    return null;
                }

                xlsApp.Visible = false;


                //得到WorkBook对象, 打开已有的文件
                Microsoft.Office.Interop.Excel.Workbook xlsBook = xlsApp.Workbooks.Open(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                //指定要操作的Sheet
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[1];

                //在单元格中读出数据
                Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.get_Range("A1", Type.Missing);
                dataRange.get_Resize(2, 200);

                int cols = 0;
                dt = new System.Data.DataTable();
                while (((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[1, cols + 1]).Value2 != null
                    && !String.IsNullOrEmpty(((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[1, cols + 1]).Value2.ToString()))
                {
                    cols++;
                    dt.Columns.Add(new System.Data.DataColumn());
                }

                dataRange = xlsSheet.get_Range("A1", Type.Missing);
                dataRange.get_Resize(5000, cols);

                for (int rIndex = 1; rIndex < 5000; rIndex++)
                {
                    System.Data.DataRow dr = dt.NewRow();

                    bool flag = false;
                    for (int cIndex = 0; cIndex < cols; cIndex++)
                    {
                        object obj = ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[rIndex, cIndex + 1]).Value2;
                        if (obj != null && !String.IsNullOrEmpty(obj.ToString()))
                        {
                            dr[cIndex] = obj.ToString();
                            flag = true;
                        }
                        else
                        {
                            dr[cIndex] = "";
                        }
                    }
                    if (flag)
                    {
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        break;
                    }
                }
                xlsBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.ToString());

                SaveNote(ex.ToString());
            }
            finally
            {

                xlsApp.Quit();

                GC.Collect();
            }
            if (xlsApp != null) xlsApp.Quit();
            return dt;
        }

        /// <summary>
        /// 读Excel文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>DataTable</returns>
        //public static DataTable readCDEDataFile(string filePath)
        //{
        //    return readCDEDataFile(filePath, "*");
        //}

        /// <summary>
        /// 读Excel文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>DataTable</returns>
        public static DataTable readCDEDataFile(string filePath,string condition)
        {
            DataTable dt = new DataTable();

            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//lauch excel application
            try
            {
                Microsoft.Office.Interop.Excel.Workbook xlsBook = excel.Workbooks.Open(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                if (excel == null)
                {
                    //Response.Write("<script>alert('Can't access excel')</script>");
                }
                else
                {
                    Microsoft.Office.Interop.Excel.Worksheet wsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Worksheets.get_Item(1);

                    string name = wsheet.Name;

                    xlsBook.Close(false, Type.Missing, Type.Missing);

                    excel.Quit();

                    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
                    OleDbConnection oledbConn = new OleDbConnection(strConn);

                    oledbConn.Open();

                    string strExcel = "select " + condition + " from [" + name + "$]";

                    OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);

                    DataSet ds = new DataSet();

                    myCommand.Fill(dt);

                    oledbConn.Close();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());

                //SaveNote(e.ToString());
            }
            finally
            {
                if (excel != null) excel.Quit();

                GC.Collect();
            }

            if (excel != null) excel.Quit();

            return dt;
        } 

        /// <summary>
        /// 查询excel中某一列的重复数
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ExcelColumnName"></param>
        /// <returns></returns>
        public static DataTable DistinctDt(string filePath,string ExcelColumnName)
        {
            DataTable dt = new DataTable();
            
            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//lauch excel application
            try
            {
                Microsoft.Office.Interop.Excel.Workbook xlsBook = excel.Workbooks.Open(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);




                if (excel == null)
                {
                    //Response.Write("<script>alert('Can't access excel')</script>");
                }
                else
                {
                    Microsoft.Office.Interop.Excel.Worksheet wsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Worksheets.get_Item(1);

                    string name = wsheet.Name;

                    excel.Quit();

                    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
                    OleDbConnection oledbConn = new OleDbConnection(strConn);

                    oledbConn.Open();

                    //string strExcel = "select * from [" + name + "$] where" + ExcelColumnName + "=" + ExcelValue;
                    string strExcel = "select " + ExcelColumnName + ",c.num from(select count(*) as num," + ExcelColumnName + " from [" + name + "$] group by " + ExcelColumnName + ") as c where num>=2";

                    OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);

                    DataSet ds = new DataSet();

                    myCommand.Fill(dt);

                    oledbConn.Close();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());

                //SaveNote(e.ToString());
            }
            finally
            {
                if (excel != null) excel.Quit();

                GC.Collect();
            }

            if (excel != null) excel.Quit();

            return dt;
        }

        /// <summary>
        ///  DataTable写入Excel（读取非CDE生成的第一行为列名的Excel）
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public bool writeDtToExcel(string templateFileName, System.Data.DataTable dataTable)
        {
            if (String.IsNullOrEmpty(templateFileName))
            {
                return false;
            }

            //创建Application对象 
            Microsoft.Office.Interop.Excel.Application xlsApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlsBook = null;
            try
            {
                xlsApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlsApp == null)
                {
                    return false;
                }

                xlsApp.Visible = false;

                //得到WorkBook对象, 打开已有的文件
                xlsBook = xlsApp.Workbooks.Add(Missing.Value);

                while (xlsBook.Sheets.Count > 1)
                {
                    Microsoft.Office.Interop.Excel.Worksheet delSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[xlsBook.Sheets.Count];
                    delSheet.Delete();
                }
                //指定要操作的Sheet
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[1];

                // 设置标题
                //Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1,1],xlApp.Cells[1,ts.GridColumnStyles.Count]);
                //range.MergeCells = true;
                //xlApp.ActiveCell.FormulaR1C1 = p_ReportName;
                //xlApp.ActiveCell.Font.Size = 20;
                //xlApp.ActiveCell.Font.Bold = true;
                //xlApp.ActiveCell.HorizontalAlignment = Excel.Constants.xlCenter;

                //最大65536行
                int colIndex = 0;
                int RowIndex = 0;
                int RowCount = dataTable.Rows.Count;
                int colCount = dataTable.Columns.Count;
                if (RowCount > 65536)
                {
                    RowCount = 65536;
                }


                // 创建缓存数据
                object[,] objData = new object[RowCount, colCount];

                // 获取数据
                for (RowIndex = 0; RowIndex < RowCount; RowIndex++)
                {
                    for (colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        objData[RowIndex, colIndex] = dataTable.Rows[RowIndex][colIndex];
                    }
                }


                Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.get_Range(xlsApp.Cells[1, 1], xlsApp.Cells[RowCount, colCount]);

                dataRange.Cells.NumberFormatLocal = "@";

                dataRange.Value2 = objData;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[1, i + 1]).Interior.Color = Color.LightGray.ToArgb();//单格颜色
                    //((Microsoft.Office.Interop.Excel.Range)dataRange.Cells[2, i + 1]).Interior.Color = Color.LightGray.ToArgb();//单格颜色

                }
                //xlsSheet.get_Range(1 + ":" + 1, Type.Missing).Rows.RowHeight = 0;

                //保存，关闭
                xlsBook.Saved = true;
                //xlsBook.SaveCopyAs(templateFileName);
                xlsBook.SaveAs(templateFileName, 56, null, null, null, null, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, null, null, null, null, null);
                xlsBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {

                //xlsBook.Close(false, Type.Missing, Type.Missing);
                //Console.WriteLine(ex.ToString());

                SaveNote(ex.ToString());
            }
            finally
            {
                xlsApp.Quit();
                GC.Collect();
            }
            xlsBook = null;
            if (xlsApp != null) xlsApp.Quit();
            return true;
        }
        /// <summary>
        /// AGENT_SELF模板导出
        /// </summary>
        /// <param name="templateFileName">路径+名称</param>
        /// <param name="dataTable">转换数据</param>
        /// <returns></returns>
        public bool writeTableDataToExcel_Self(string templateFileName, System.Data.DataTable dataTable)
        {
            if (String.IsNullOrEmpty(templateFileName))
            {
                return false;
            }

            //创建Application对象 
            Microsoft.Office.Interop.Excel.Application xlsApp = null;
            try
            {
                xlsApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlsApp == null)
                {
                    return false;
                }

                xlsApp.Visible = false;

                string filepath = @"~/../Web/Downloads/Template/template.xls";
                filepath = System.Web.HttpContext.Current.Server.MapPath(filepath);
                //System.Configuration.ConfigurationSettings.AppSettings["dbPath"] + "template.xls";
                //得到WorkBook对象, 打开已有的文件
                Microsoft.Office.Interop.Excel.Workbook xlsBook = xlsApp.Workbooks.Open(filepath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                while (xlsBook.Sheets.Count > 1)
                {
                    Microsoft.Office.Interop.Excel.Worksheet delSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[xlsBook.Sheets.Count];
                    delSheet.Delete();
                }
                //指定要操作的Sheet
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.Sheets[1];

                string name = "无";
                                
                xlsSheet.Name = name;
                //最大65536行
                int colIndex = 0;
                int RowIndex = 0;
                int RowCount = dataTable.Rows.Count;
                int colCount = dataTable.Columns.Count;
                if (RowCount > 65536)
                {
                    RowCount = 65536;
                }

                // 创建缓存数据
                object[,] objData = new object[RowCount, colCount];

                // 获取数据
                for (RowIndex = 0; RowIndex < RowCount; RowIndex++)
                {
                    for (colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        objData[RowIndex, colIndex] = dataTable.Rows[RowIndex][colIndex];
                    }
                }

                Microsoft.Office.Interop.Excel.Range dataRange = xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[RowCount, colCount]);


                dataRange.Cells.Borders.LineStyle = 1;

                dataRange.Value2 = objData;
                //保存，关闭
                xlsBook.Saved = true;
                xlsBook.SaveAs(templateFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlsBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                xlsApp.Quit();
                GC.Collect();
            }
            if (xlsApp != null) xlsApp.Quit();
            return true;
        
    }
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="note"></param>
        public void SaveNote(string note)
        {
            FileStream stream = new FileStream(GetLogDirectory("Common") + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.Append, FileAccess.Write, FileShare.Delete | FileShare.ReadWrite);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("================================================================");
            writer.WriteLine(string.Format("Note:\t{0}", note));
            writer.WriteLine(string.Format("DateTime:\t{0}\r\n\r\n", DateTime.Now.ToShortTimeString()));
            stream.Flush();
            writer.Close();
            stream.Close();
            stream.Dispose();
            writer.Dispose();
        }


        /// <summary>
        /// 日志目录
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string GetLogDirectory(string category)
        {
            string baseDirectory = string.Empty;
            if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
            {
                baseDirectory = HttpContext.Current.Server.MapPath("~");
            }
            else
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            if ((baseDirectory[baseDirectory.Length - 1] != '/') && (baseDirectory[baseDirectory.Length - 1] != '\\'))
            {
                baseDirectory = baseDirectory + @"\";
            }
            baseDirectory = string.Format(@"{0}Log\{1}\", baseDirectory, category);
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }
            return baseDirectory;
        }   
    }
}
