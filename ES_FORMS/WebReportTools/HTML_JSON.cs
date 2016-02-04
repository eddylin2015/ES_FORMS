using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace ES_FORMS
{
    public class HTML_JSON
    {
        public static void save_Template_HTML_Grid(String file_name, String _datajsfilename, String _cssfilename, String templatfilename, DataTable dt, String js, string jscmd)
        {
            String fileContents = System.IO.File.ReadAllText(templatfilename, Encoding.UTF8);
            StreamWriter sw = new StreamWriter(file_name, false, Encoding.UTF8);
            sw.WriteLine(
@"<!DOCTYPE html>
<html>
<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf8"">");
            sw.WriteLine("<script src=\"{0}\"></script>", _datajsfilename);
            sw.WriteLine("<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\" />", _cssfilename);
            sw.WriteLine("</head>\n<body>");
            sw.WriteLine("<table>");
            int rcnt = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (i > 0) sw.WriteLine("<div style=\"page-break-after: always;font-size:1px;\"></div>");
                if (i % 3 == 0) { 
                     rcnt++;
                     if (rcnt>1 && (rcnt-1) % 3 == 0) { 
                         sw.WriteLine("</tr></table>");
                         sw.WriteLine("<div style=\"page-break-after: always;font-size:1px;\">&nbsp;</div>");
                         sw.WriteLine("<table>"); 
                     }
                     if (i == 0) { sw.WriteLine("<tr>"); }
                     else { sw.WriteLine("</tr><tr>"); }
                }
                sw.WriteLine("<td>");
                sw.WriteLine(fileContents.Replace("keyid", i.ToString()));
                sw.WriteLine("</td>");
                
            }
            sw.WriteLine("</table>");
            sw.WriteLine("</body></html>");
            sw.WriteLine("<script src=\"{0}\"></script>", js);
            sw.WriteLine("<script>");
            sw.WriteLine(jscmd);
            sw.WriteLine("</script>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
      
        public static void save_Template_HTML(String file_name,String _datajsfilename,String _cssfilename,String  templatfilename, int cnt,String js,string jscmd)
        {
            String fileContents = System.IO.File.ReadAllText(templatfilename, Encoding.UTF8);
            StreamWriter sw = new StreamWriter(file_name, false, Encoding.UTF8);
            sw.WriteLine(
@"<!DOCTYPE html>
<html>
<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf8"">
    <script src=""jquery-1.7.2.js""></script>");
   sw.WriteLine("<script src=\"{0}\"></script>", _datajsfilename);
   sw.WriteLine("<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\" />", _cssfilename);
            sw.WriteLine("</head>\n<body>");
            for (int i = 0; i < cnt; i++)
            {
                if (i > 0) sw.WriteLine("<div style=\"page-break-after: always;font-size:1px;\"></div>");
                sw.WriteLine(fileContents.Replace("keyid", i.ToString()));
            }
            sw.WriteLine("</body></html>");
            sw.WriteLine("<script src=\"{0}\"></script>",js);
            sw.WriteLine("<script>");
            sw.WriteLine(jscmd);
            sw.WriteLine("</script>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        public static void save_Template_HTML_B(String file_name, String _datajsfilename, String _cssfilename, String templatfilename, DataTable dt, String js, string jscmd)
        {
            String fileContents = System.IO.File.ReadAllText(templatfilename, Encoding.UTF8);
            StreamWriter sw = new StreamWriter(file_name, false, Encoding.UTF8);
            sw.WriteLine(
@"<!DOCTYPE html>
<html>
<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf8"">");
            sw.WriteLine("<script src=\"{0}\"></script>", _datajsfilename);
            sw.WriteLine("<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\" />", _cssfilename);
            sw.WriteLine("</head>\n<body>");
            if(!js.Contains("_replace.js"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0) sw.WriteLine("<div style=\"page-break-after: always;font-size:1px;\">&nbsp;</div>");
                    sw.WriteLine(fileContents.Replace("keyid", i.ToString()));
                }
            }
            else
            {
                int cnt = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    String tempstr = fileContents;
                    if (cnt > 0) sw.WriteLine("<div style=\"page-break-after: always;font-size:1px;\">&nbsp;</div>");
                    
                    foreach (DataColumn dc in dt.Columns)
                    {
                        tempstr = tempstr.Replace("<span id=" + dc.ColumnName + "_keyid></span>", dr[dc.ColumnName].ToString());
                    }
                    sw.WriteLine(tempstr.Replace("keyid",cnt.ToString()));
                    cnt++;
                }
            }
            sw.WriteLine("</body></html>");
            sw.WriteLine("<script src=\"{0}\"></script>", js);
            sw.WriteLine("<script>");
            sw.WriteLine(jscmd);
            sw.WriteLine("</script>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        public static void save_jsondata_js_file(string datafile, DataTable dt)
        {
            StreamWriter salayformjs = new StreamWriter(datafile, false, Encoding.UTF8);
            salayformjs.Write("var jsondata=[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) { salayformjs.WriteLine(","); }
                salayformjs.WriteLine("{{KEYID:{0}", i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    salayformjs.Write(",{0}:\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j].ToString().Replace('{', '_').Replace('\n', '_').Replace('"', '_'));
                }
                salayformjs.WriteLine("}");
            }
            salayformjs.WriteLine("];");
            salayformjs.Flush();
            salayformjs.Close();
            salayformjs.Dispose();
        }
        public static void save_jsondata_js_file(string datafile,DataSet ds)
        {
            StreamWriter salayformjs = new StreamWriter(datafile, false, Encoding.UTF8);
            salayformjs.Write("var jsondata=[");
            DataTable dt = ds.Tables["Table"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) { salayformjs.WriteLine(","); }
                salayformjs.WriteLine("{{KEYID:{0}", i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    salayformjs.Write(",{0}:\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                }
                foreach (DataTable sdt in ds.Tables)
                {
                    if (sdt.TableName.Equals("Table")) continue;
                    int childcnt = 0;
                    salayformjs.WriteLine(",{0}:[", "sr_" + sdt.TableName);
                    foreach (DataRow childRow in dt.Rows[i].GetChildRows("sr_" + sdt.TableName))
                    {
                        if (childcnt > 0) salayformjs.WriteLine(",");
                        salayformjs.WriteLine("{");
                        for (int j = 0; j < childRow.Table.Columns.Count; j++)
                        {
                            if (j > 0) salayformjs.Write(",");
                            salayformjs.Write("{0}:\"{1}\"", childRow.Table.Columns[j].ColumnName, childRow[j]);
                        }
                        salayformjs.WriteLine("}");
                        childcnt++;
                    }
                    salayformjs.WriteLine("]");
                }        
                salayformjs.WriteLine("}");
            }
            salayformjs.WriteLine("];");
            salayformjs.Flush();
            salayformjs.Close();
            salayformjs.Dispose();
        }
        public static void save_jsondata_js_file_studcard(string datafile, DataTable dt)
        {
            StreamWriter salayformjs = new StreamWriter(datafile, false, Encoding.UTF8);
            salayformjs.Write("var jsondata=[");
            
            String[] fn = { "STUD_ID", "CODE", "NAME_C", "NAME_P", "GRADE", "CLASS", "C_NO","YEAR" };
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) { salayformjs.WriteLine(","); }
                salayformjs.WriteLine("{{KEYID:{0}", i);
                //for (int j = 0; j < dt.Columns.Count; j++)
                foreach (String s in fn) 
                {
                    salayformjs.Write(",{0}:\"{1}\"", s, dt.Rows[i][s]);
                   // salayformjs.Write(",{0}:\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                }
               
                salayformjs.WriteLine("}");
            }
            salayformjs.WriteLine("];");
            salayformjs.Flush();
            salayformjs.Close();
            salayformjs.Dispose();
        }
    }
}

