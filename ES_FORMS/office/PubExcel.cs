using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
namespace ES_FORMS.office_xxx
{
    /// <summary>
    /// 
    /// </summary>
    public class PubExcel
    {
        protected bool oVisible = false;
        public const int MaxColumnNumber = 230;
        Boolean datetimefield_Str_flag = true;
        protected Microsoft.Office.Interop.Excel.Workbook obook;
        protected Microsoft.Office.Interop.Excel.Worksheet osheet;
        protected Microsoft.Office.Interop.Excel.Application oexcel;
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID);

        private static string convertIndex2ABC(int x, int y)
        {
            if (x > MaxColumnNumber)
                throw new Exception("Excel worksheet 長度超出" + MaxColumnNumber + ",即是column 編號超出IV");
            char c;

            c = (char)(65 + ((x - 1) % 26));
            string res = c.ToString() + y.ToString();
            int preFixNo = (x - 1) / 26;
            if (preFixNo > 0)
            {
                res = ((char)(65 + preFixNo - 1)).ToString() + res;
            }

            return res;
        }
        public string sheetname;
        public string readCell(int x, int y)
        {
            string r = osheet.get_Range("A1", Type.Missing).Value2.ToString();
            return r;
        }
        /*
         * Excel.Range oRng = (Excel.Range)oWS.get_Range("A1", "B4");
         * object[,] data = (object[,])oRng.Value2;
         * for(int i=1; i<=data.GetUpperBound(0); i++)
            {
              for(int j = 1; j <= data.GetUpperBound(1); j++)
              {
                    Console.WriteLine("(" + i + "," + j + ") = " + data[i,j]);
                    data[i,j] = "NEW" + data[i,j];
               }
            }
        */
        public Object[,] readCell(int x, int y, int x1)
        {
            Microsoft.Office.Interop.Excel.Range oRng = (Microsoft.Office.Interop.Excel.Range)osheet.get_Range(convertIndex2ABC(x, y), convertIndex2ABC(x1, y));
            Object[,] data = (Object[,])oRng.Value2;
            return data;

        }
        public Object[,] readCell(int x, int y, int x1, int y1)
        {
            Microsoft.Office.Interop.Excel.Range oRng = (Microsoft.Office.Interop.Excel.Range)osheet.get_Range(convertIndex2ABC(x, y), convertIndex2ABC(x1, y1));
            Object[,] data = (Object[,])oRng.Value2;
            return data;

        }

        /* 1990/01/01 1990/01
         * 在匯出時前加 '
         * 
         */
        public bool VerifyDateStrFormat(string context)
        {
            if (context.Length <= 10 && Regex.Match(context, @"\d+/\d+/\d+").Success)
            {
                return true;
            }
            else if (context.Length <= 7 && Regex.Match(context, @"\d+/\d+").Success)
            {
                return true;
            }
            /*            if (context.Length <= 20 && Regex.Match(context, @"\d+/\d+/\d+ \d+:\d+:\d+").Success)
                        {
                            return true;
                        }
            */
            else
            {
                return false;

            }
        }

        public void writeCell(int x, int y, string context)
        {
            if (VerifyDateStrFormat(context))
            { osheet.get_Range(convertIndex2ABC(x, y), Type.Missing).Value2 = "," + context; }
            else
            { osheet.get_Range(convertIndex2ABC(x, y), Type.Missing).Value2 = context; }

        }
        /*Excel.Range range = wks.get_Range("A1", "C10");
            Object[,] arr = new Object[10,3];
            for (int i = 0; i < 10; i++)
            {
                arr[i,0] = "test";
                arr[i,1] = i;
                arr[i,2] = 5.653;
            }
            range.set_Value(Missing.Value,arr);
         */
        public void writeCell(int x, int y, int x1, int y1, Object[] context)
        {
            // MessageBox.Show(x1+" "+y1+context.ToString());

            osheet.get_Range(convertIndex2ABC(x, y), convertIndex2ABC(x1, y1)).Value2 = context;
        }
        public bool saveTo(string filename, bool alert)
        {
            bool r = false;
            oexcel.DisplayAlerts = alert;
            osheet.SaveAs(filename);
            //osheet.SaveAs(filename,Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel9795,Type.Missing,Type.Missing,Type.Missing,
            //                Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            oexcel.DisplayAlerts = true;
            r = true;
            return r;
        }
        public bool Visible
        {
            get
            {

                return oVisible;
            }

            set
            {
                oexcel.Visible = value;
                oVisible = value;
            }
        }
        public PubExcel()
        {
            newFile();
        }
        public PubExcel(bool DateTimeFlag)
        {
            datetimefield_Str_flag = DateTimeFlag;
            newFile();

        }
        /*
         *同時打開某檔案 
         */
        public PubExcel(string filename, string sheetname)
        {

            this.sheetname = sheetname;
            oexcel = new Microsoft.Office.Interop.Excel.Application();
            obook = oexcel.Workbooks.Open(filename);
            osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets[sheetname];
        }

        public PubExcel(string filename, string sheetname, bool DateTimeFlag)
        {
            datetimefield_Str_flag = DateTimeFlag;
            this.sheetname = sheetname;
            oexcel = new Microsoft.Office.Interop.Excel.Application();
            obook = oexcel.Workbooks.Open(filename,
                       Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets[sheetname];
        }

        ~PubExcel()
        {
        }
        //-------
        //新增XLS檔案
        //-------
        public void newFile()
        {
            //MessageBox.Show("20151011");
            //sheetname = "Sheet1";
            Microsoft.Office.Interop.Excel.XlWBATemplate wba = Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet;
            oexcel = new Microsoft.Office.Interop.Excel.Application();
            obook = oexcel.Workbooks.Add(wba);
            //osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets[sheetname];
            //for(int i=1 ;i<  obook.Sheets.Count+1; i++)
            //{
            //osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets.get_Item(i);
            //MessageBox.Show(osheet.Name);
            //}
            if (obook.Sheets.Count < 1)
            {
                obook.Sheets.Add();
            }
            osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets.get_Item(1);
            sheetname = osheet.Name;
        }
        public void closeExcel()
        {
            oexcel.DisplayAlerts = false;
            oexcel.Quit();
            oexcel.DisplayAlerts = true;
            IntPtr t = new IntPtr(oexcel.Hwnd);
            int k = 0;
            GetWindowThreadProcessId(t, out   k);
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
            p.Kill();

        }
        public void ExportXls(DataTable dt, Hashtable dict)
        {

            String[] a = new String[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dict != null)
                {
                    if (dict.Contains(dt.Columns[i].ColumnName.ToUpper()))
                    {
                        a[i] = dict[dt.Columns[i].ColumnName.ToUpper()].ToString();
                    }
                    else
                    {
                        a[i] = dt.Columns[i].ColumnName.ToString();
                    }
                }
                else { a[i] = dt.Columns[i].ColumnName; }
            }

            writeCell(1, 1, dt.Columns.Count, 1, a);

            Object[] datav = new Object[dt.Columns.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    String dttype = dt.Columns[j].DataType.ToString();
                    if (dt.Columns[j].DataType.Equals(System.Type.GetType("System.DateTime")) && datetimefield_Str_flag)
                    {
                        datav[j] = "'" + dt.Rows[i][j].ToString();
                    }
                    else if (VerifyDateStrFormat(dt.Rows[i][j].ToString()))
                    {
                        datav[j] = "'" + dt.Rows[i][j].ToString();
                    }
                    else if (dttype == "System.Int32"
              || dttype == "System.Int16"
              || dttype == "System.Decimal"
              || dttype == "System.Single"
              || dttype == "System.Double")
                    {
                        datav[j] = dt.Rows[i][j];
                    }
                    else if (dttype == "System.String")
                    {
                        if (Regex.IsMatch(dt.Rows[i][j].ToString(), @"\d+") || Regex.IsMatch(dt.Rows[i][j].ToString(), @"\d+.\d+"))
                        {
                            datav[j] = "'" + dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            datav[j] = dt.Rows[i][j].ToString();
                        }
                    }
                    else
                    {
                        datav[j] = dt.Rows[i][j].ToString();
                    }

                }
                writeCell(1, i + 2, dt.Columns.Count, i + 2, datav);
            }
        }


        public virtual void ImportXls(DataTable dt)
        {
            int rowcnt = 1;
            string keyfield = dt.Columns[0].Caption;
            Object[,] data = readCell(1, ++rowcnt, dt.Columns.Count);
            string tmp = "";
            while (data[1, 1] != null)
            {
                tmp += data[1, 1].ToString() + "{}";

                DataRow[] dr = dt.Select(keyfield + "='" + data[1, 1].ToString() + "'");
                if (dr != null && dr.Length > 0 && (!data[1, 1].ToString().Equals("-1")))
                {
                    dr[0].BeginEdit();
                    for (int i = 0; i < data.GetUpperBound(1); i++)
                    {
                        if (data[1, i + 1] != null)
                        {
                            dr[0][i] = data[1, i + 1];
                        }
                    }

                    dr[0].EndEdit();

                }
                else
                {
                    DataRow r = dt.NewRow();
                    r.BeginEdit();
                    for (int i = 0; i < data.GetUpperBound(1); i++)
                    {

                        if (data[1, i + 1] != null)
                        {

                            if (dt.Columns[i].DataType == typeof(DateTime))
                            {

                                DateTime dati;
                                String s = data[1, i + 1].ToString();
                                // MessageBox.Show(s);
                                if (DateTime.TryParse(s, out dati))
                                {
                                    r[i] = dati;
                                }
                            }
                            else
                            {
                                r[i] = data[1, i + 1];
                            }
                        }
                    }
                    r.EndEdit();
                    dt.Rows.Add(r);

                }
                data = readCell(1, ++rowcnt, dt.Columns.Count);

            }
        }
    }

    public class PubExcel0
    {
        protected bool oVisible = false;
        public const int MaxColumnNumber = 230;
        Boolean datetimefield_Str_flag = true;
        protected Microsoft.Office.Interop.Excel.Workbook obook;
        protected Microsoft.Office.Interop.Excel.Worksheet osheet;
        protected Microsoft.Office.Interop.Excel.Worksheet[] osheets;
        protected Microsoft.Office.Interop.Excel.Application oexcel;
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID);
        private Microsoft.Office.Interop.Excel.Worksheet GetSheet(int index)
        {
            if (index >= 0 && index <= osheets.Length)
            {
                return osheets[index];
            }
            else
            {
                return osheet;
            }
        }
        private static string convertIndex2ABC(int x, int y)
        {
            if (x > MaxColumnNumber)
                throw new Exception("Excel worksheet 長度超出" + MaxColumnNumber + ",即是column 編號超出IV");
            char c;

            c = (char)(65 + ((x - 1) % 26));
            string res = c.ToString() + y.ToString();
            int preFixNo = (x - 1) / 26;
            if (preFixNo > 0)
            {
                res = ((char)(65 + preFixNo - 1)).ToString() + res;
            }

            return res;
        }
        public string sheetname;

        /* 1990/01/01 1990/01
         * 在匯出時前加 '
         * 
         */
        public bool VerifyDateStrFormat(string context)
        {
            if (context.Length <= 10 && Regex.Match(context, @"\d+/\d+/\d+").Success)
            {
                return true;
            }
            else if (context.Length <= 7 && Regex.Match(context, @"\d+/\d+").Success)
            {
                return true;
            }
            /*            if (context.Length <= 20 && Regex.Match(context, @"\d+/\d+/\d+ \d+:\d+:\d+").Success)
                        {
                            return true;
                        }
            */
            else
            {
                return false;

            }
        }

        public void writeCell(int x, int y, string context, int sheetindex)
        {
            if (VerifyDateStrFormat(context))
            { GetSheet(sheetindex).get_Range(convertIndex2ABC(x, y), Type.Missing).Value2 = "'" + context; }
            else
            { GetSheet(sheetindex).get_Range(convertIndex2ABC(x, y), Type.Missing).Value2 = context; }

        }
        /*Excel.Range range = wks.get_Range("A1", "C10");
            Object[,] arr = new Object[10,3];
            for (int i = 0; i < 10; i++)
            {
                arr[i,0] = "test";
                arr[i,1] = i;
                arr[i,2] = 5.653;
            }
            range.set_Value(Missing.Value,arr);
         */
        public void writeCell(int x, int y, int x1, int y1, Object[] context, int sheetindex)
        {
            // MessageBox.Show(x1+" "+y1+context.ToString());

            GetSheet(sheetindex).get_Range(convertIndex2ABC(x, y), convertIndex2ABC(x1, y1)).Value2 = context;
        }
        public bool saveTo(string filename, bool alert)
        {
            bool r = false;
            oexcel.DisplayAlerts = alert;
            obook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel9795, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            oexcel.DisplayAlerts = true;
            r = true;
            return r;
        }
        public bool Visible
        {
            get
            {

                return oVisible;
            }

            set
            {
                oexcel.Visible = value;
                oVisible = value;
            }
        }
        public PubExcel0()
        {
            newFile(null);
        }
        public PubExcel0(bool DateTimeFlag)
        {
            datetimefield_Str_flag = DateTimeFlag;
            newFile(null);
        }
        public PubExcel0(String[] SheetNames, bool DateTimeFlag)
        {
            datetimefield_Str_flag = DateTimeFlag;
            newFile(SheetNames);
        }

        ~PubExcel0()
        {
        }
        //-------
        //新增XLS檔案
        //-------
        public void newFile(String[] SheetNames)
        {
            sheetname = "sheet1";
            Microsoft.Office.Interop.Excel.XlWBATemplate wba = Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet;
            oexcel = new Microsoft.Office.Interop.Excel.Application();
            obook = oexcel.Workbooks.Add(wba);
            osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets[sheetname];
            osheets = new Microsoft.Office.Interop.Excel.Worksheet[SheetNames.Length];
            for (int i = 0; i < SheetNames.Length; i++)
            {
                osheets[i] = (Microsoft.Office.Interop.Excel.Worksheet)obook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                osheets[i].Name = SheetNames[i];
            }

        }
        public void closeExcel()
        {
            oexcel.DisplayAlerts = false;
            oexcel.Quit();
            oexcel.DisplayAlerts = true;
            IntPtr t = new IntPtr(oexcel.Hwnd);
            int k = 0;
            GetWindowThreadProcessId(t, out   k);
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
            p.Kill();

        }
        public void ExportXls(DataTable dt, Hashtable dict, int sheetindex)
        {
            String[] a = new String[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dict != null)
                {
                    if (dict.Contains(dt.Columns[i].ColumnName.ToUpper()))
                    {
                        a[i] = dict[dt.Columns[i].ColumnName.ToUpper()].ToString();
                    }
                    else
                    {
                        a[i] = dt.Columns[i].ColumnName.ToString();
                    }
                }
                else { a[i] = dt.Columns[i].ColumnName; }
            }

            writeCell(1, 1, dt.Columns.Count, 1, a, sheetindex);
            Object[] datav = new object[dt.Columns.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    String dttype = dt.Columns[j].DataType.ToString();
                    if (dt.Columns[j].DataType.Equals(System.Type.GetType("System.DateTime")) && datetimefield_Str_flag)
                    {
                        datav[j] = "'" + dt.Rows[i][j].ToString();
                    }
                    else if (dttype == "System.Int32"
                   || dttype == "System.Int16"
                   || dttype == "System.Decimal"
                   || dttype == "System.Single"
                   || dttype == "System.Double")
                    {
                        datav[j] = dt.Rows[i][j];
                    }
                    else if (VerifyDateStrFormat(dt.Rows[i][j].ToString()))
                    {
                        datav[j] = "'" + dt.Rows[i][j].ToString();
                    }
                    else if (dttype == "System.String")
                    {
                        if (Regex.IsMatch(dt.Rows[i][j].ToString(), @"\d+") || Regex.IsMatch(dt.Rows[i][j].ToString(), @"\d+.\d+"))
                        {
                            datav[j] = "'" + dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            datav[j] = dt.Rows[i][j].ToString();
                        }
                    }
                    else
                    {
                        datav[j] = dt.Rows[i][j].ToString();
                    }
                }
                writeCell(1, i + 2, dt.Columns.Count, i + 2, datav, sheetindex);
            }
        }
    }

    public class PubExcelbyStaf_ref : PubExcel
    {
        public override void ImportXls(DataTable dt)
        {
            int rowcnt = 1;

            Object[,] data = readCell(1, ++rowcnt, dt.Columns.Count);
            string tmp = "";
            while (data[1, 1] != null)
            {
                tmp += data[1, 1].ToString() + "{}";

                DataRow[] dr = dt.Select("staf_ref='" + data[1, 1].ToString() + "'");
                if (dr != null && dr.Length > 0)
                {
                    dr[0].BeginEdit();
                    for (int i = 0; i < data.GetUpperBound(1); i++)
                    {
                        if (data[1, i + 1] != null)
                        {
                            dr[0][i] = data[1, i + 1];
                        }
                    }

                    dr[0].EndEdit();

                }
                else
                {
                    DataRow r = dt.NewRow();
                    r.BeginEdit();
                    for (int i = 0; i < data.GetUpperBound(1); i++)
                    {

                        if (data[1, i + 1] != null)
                        {
                            r[i] = data[1, i + 1];
                        }
                    }
                    r.EndEdit();
                    dt.Rows.Add(r);

                }
                data = readCell(1, ++rowcnt, dt.Columns.Count);

            }
        }
    }
}
