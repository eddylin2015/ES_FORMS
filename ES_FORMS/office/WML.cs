
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Collections;


namespace ES_FORMS
{
    class WML
    {
    }
    public interface iDB2WML
    {
        void makeDoc(String destfileName);
    }
    public interface iParseWMLitem
    {
        string getFielddata(String Key, String fontSize, DataRow aDR);
        string getText(String Key);
        string getMemo(String Key, String fontSize, bool alignCenter, bool justleft);
        string parseData(String Key);
    }
    public class TDB2WML : iDB2WML
    {
        protected DataTable data_table;
        private TParseWMLitem iPitem;
        protected TempletFile_Struct templet_s = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipWML"></param>
        /// <param name="sourceDataTable"></param>
        /// <param name="templetFileName"></param>
        public TDB2WML(TParseWMLitem ipWML, DataTable sourceDataTable, string templetFileName)
        {
            data_table = sourceDataTable;
            iPitem = ipWML;
            templet_s = new TempletFile_Struct(templetFileName);
        }
        protected void ADD_WXSect(int recno, TempletFile_Struct templet_s, StreamWriter sw, DataRow aDR)
        {
            if (recno > 1) { templet_s.posi = templet_s.SectBegin; sw.Write("<wx:sect>"); }
            bool procTag = false;
            bool procField = false;
            bool procPictTag = false;
            string tempfield = "";
            string temptag = "";
            while (templet_s.posi < templet_s.lines.Length)
            {
                char ch = templet_s.lines[templet_s.posi];
                if (!procField && ch != '|' && ch != '#' && !(procPictTag && iPitem.pictflag)) { sw.Write(ch.ToString()); }
                switch (ch)
                {
                    case '<': temptag = ""; procTag = true; break;
                    case '>': procTag = false;
                        if (temptag == "/wx:sect") return;
                        if (temptag == "w:pict" && iPitem.pictflag) { procPictTag = true; }
                        if (temptag == "/w:pict" && iPitem.pictflag) { procPictTag = false; sw.Write(iPitem.getPICTCODE()); sw.Write("</w:pict>"); }
                        break;
                    case '#': if (!procTag) { procField = true; tempfield = ""; } break;
                    case '|': if (!procTag) { procField = false; sw.Write(iPitem.getFielddata(tempfield, "28", aDR)); }
                        break;
                    default:
                        if (!procTag && procField) tempfield += ch;
                        if (procTag) temptag += ch;
                        break;
                }
                templet_s.posi++;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destfileName"></param>
        public  virtual void makeDoc(String destfileName)
        {
            StreamWriter sw = new StreamWriter(destfileName);
            StreamReader sr = new StreamReader(templet_s.filename);

            templet_s.lines = sr.ReadToEnd();
            templet_s.posi = 0;
            string temptag = "";
            int recno = 0;
            int TempPageNO = 0;

            while (templet_s.posi < templet_s.lines.Length)
            {
                char c = templet_s.lines[templet_s.posi];
                sw.Write(c);
                switch (c)
                {
                    case '<': temptag = ""; break;
                    case '>':
                        if (temptag == "wx:sect" && TempPageNO == 0)
                        {
                            templet_s.SectBegin = templet_s.posi;
                            foreach (DataRow aDR in data_table.Rows)
                            {
                                recno++;
                                ADD_WXSect(recno, templet_s, sw, aDR);
                                }
                        }
                        else if (temptag == "/wx:sect" && TempPageNO == 0)
                        {
                            TempPageNO++; templet_s.SectEnd = templet_s.posi;
                        }
                        break;
                    default:
                        temptag += c;
                        break;
                }
                templet_s.posi++;
            }
            sw.Flush();
            sw.Dispose();
            sr.Dispose();
        }
    }
    public class TempletFile_Struct
    {
        public string filename = "";
        public int posi = 0;
        public int SectBegin = 0;
        public int SectEnd = 0;
        public string lines=null;
        public int TempPageNO = 0;
        public TempletFile_Struct(string afilename)
        {
            filename=afilename;
        }
        public void ReadTemplet2Lines()
        {
            StreamReader sr = new StreamReader(filename);
            lines = sr.ReadToEnd();
            posi = 0;
            string temptag = "";
            while (posi < lines.Length)
            {
                char c =lines[posi];
                switch (c)
                {
                    case '<': temptag = ""; break;
                    case '>':
                        if (temptag == "wx:sect" && TempPageNO == 0)
                        {
                            SectBegin = posi;
                        }
                        else if (temptag == "/wx:sect" && TempPageNO == 0)
                        {
                            TempPageNO++; SectEnd = posi;
                        }
                        break;
                    default:
                        temptag += c;
                        break;
                }
                posi++;
            }
            sr.Dispose();
        }
    }
    public class TDB2WMLforKid : TDB2WML
    {
        private TempletFile_Struct templet_s001 = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipWML"></param>
        /// <param name="sourceDataTable"></param>
        /// <param name="templetFileName"></param>
        public TDB2WMLforKid(TParseWMLitem ipWML, DataTable sourceDataTable, string templetFileName0,string tempfilename1):base(ipWML,sourceDataTable,templetFileName0)
        {
            templet_s001 = new TempletFile_Struct(tempfilename1);
            templet_s001.ReadTemplet2Lines();//
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destfileName"></param>
        public override void makeDoc(String destfileName)
        {
            StreamWriter sw = new StreamWriter(destfileName);
            StreamReader sr = new StreamReader(templet_s.filename);

            templet_s.lines = sr.ReadToEnd();
            templet_s.posi = 0;
            string temptag = "";
            int recno = 0;
            int TempPageNO = 0;

            DataRow adr0=null;

            while (templet_s.posi < templet_s.lines.Length)
            {
                char c = templet_s.lines[templet_s.posi];
                sw.Write(c);
                switch (c)
                {
                    case '<': temptag = ""; break;
                    case '>':
                        if (temptag == "wx:sect" && TempPageNO == 0)
                        {
                            templet_s.SectBegin = templet_s.posi;
                            foreach (DataRow aDR in data_table.Rows)
                            {
                                recno++;
                                ADD_WXSect(recno,templet_s, sw, aDR);
                                if (recno % 2 == 1) adr0 = aDR;
                                if (recno % 2 == 0 && adr0!=null)
                                {
                                    ADD_WXSect(recno, templet_s001, sw, aDR);
                                    ADD_WXSect(recno, templet_s001, sw, adr0);
                                   
                                }
                            }
                        }
                        else if (temptag == "/wx:sect" && TempPageNO == 0)
                        {
                            TempPageNO++; templet_s.SectEnd = templet_s.posi;
                        }
                        break;
                    default:
                        temptag += c;
                        break;
                }
                templet_s.posi++;
            }
            sw.Flush();
            sw.Dispose();
            sr.Dispose();
        }
    }
    public class TDB2WMLforTowPage : TDB2WML
    {
       private TempletFile_Struct templet_s001 = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipWML"></param>
        /// <param name="sourceDataTable"></param>
        /// <param name="templetFileName"></param>
        public TDB2WMLforTowPage(TParseWMLitem ipWML, DataTable sourceDataTable, string templetFileName0,string tempfilename1):base(ipWML,sourceDataTable,templetFileName0)
        {
            templet_s001 = new TempletFile_Struct(tempfilename1);
            templet_s001.ReadTemplet2Lines();//
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destfileName"></param>
        public override void makeDoc(String destfileName)
        {
            //System.Windows.Forms.MessageBox.Show(destfileName);
            StreamWriter sw = new StreamWriter(destfileName);
            //System.Windows.Forms.MessageBox.Show(templet_s.filename);
            StreamReader sr = new StreamReader(templet_s.filename);
            //System.Windows.Forms.MessageBox.Show("a");
            templet_s.lines = sr.ReadToEnd();
            templet_s.posi = 0;
            string temptag = "";
            int recno = 0;
            int TempPageNO = 0;

            while (templet_s.posi < templet_s.lines.Length)
            {
                char c = templet_s.lines[templet_s.posi];
                sw.Write(c);
                switch (c)
                {
                    case '<': temptag = ""; break;
                    case '>':
                        if (temptag == "wx:sect" && TempPageNO == 0)
                        {
                            templet_s.SectBegin = templet_s.posi;
                            foreach (DataRow aDR in data_table.Rows)
                            {
                                recno++;
                                ADD_WXSect(recno,templet_s, sw, aDR);
                                recno++;
                                ADD_WXSect(recno, templet_s001, sw, aDR);
                                
                            }
                        }
                        else if (temptag == "/wx:sect" && TempPageNO == 0)
                        {
                            TempPageNO++; templet_s.SectEnd = templet_s.posi;
                        }
                        break;
                    default:
                        temptag += c;
                        break;
                }
                templet_s.posi++;
            }
            sw.Flush();
            sw.Dispose();
            sr.Dispose();
        }
    }

    public class TParseWMLitem : iParseWMLitem
    {
        private DataRow a_datarow;
        private Hashtable fieldname_fontname_dict;
        /// <summary>
        /// 
        /// </summary>
        public bool pictflag;
        /// <summary>
        /// 
        /// </summary>
        public TParseWMLitem() { pictflag = false; fieldname_fontname_dict = null; }
        public TParseWMLitem(Hashtable field_font_dict) { pictflag = false; fieldname_fontname_dict = field_font_dict; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public virtual String getText(String Key)
        {
            return  parseData(Key);
        }
        private string GetMemoItem(bool center,bool whitecolor,bool justleft,string fontsize,string content,string fontname)
        {
            string itemFormat="<w:p><w:pPr>{0}<w:adjustRightInd w:val=\"off\" /><w:snapToGrid w:val=\"off\" /><w:ind w:right-chars=\"-46\" />{1}</w:pPr>"+
                  "<w:r><w:rPr><w:rFonts w:ascii=\"{5}\" w:fareast=\"{5}\" w:h-ansi=\"{5}\" w:hint=\"fareast\" />"+
                  "<wx:font wx:val=\"{5}\" /><w:noProof />{2}<w:sz w:val=\"{3}\"/></w:rPr><w:t>{4}</w:t></w:r></w:p>";
            if (fontname == "Wingdings") return GetMemoItemWingdingsFont(center, whitecolor, justleft, fontsize, content);
            fontname = "²Ó©úÅé";
            string  centerflag="<w:jc w:val=\"center\" />";
            string  whitecolorflag="<w:color w:val=\"FFFFFF\" />";
            string  JustLeft50="<w:ind w:left-chars=\"-50\" />";
            string t1="";
            string t2="";
            string t3="";
            if(justleft) t1=JustLeft50;
            if(center) t2=centerflag;
            if (whitecolor) t3 = whitecolorflag;
            return String.Format(itemFormat,t1,t2,t3,fontsize,content,fontname);
        }
        private string GetMemoItemWingdingsFont(bool center, bool whitecolor, bool justleft, string fontsize, string content)
        {
            string itemFormat = "<w:p><w:pPr>{0}<w:adjustRightInd w:val=\"off\" /><w:snapToGrid w:val=\"off\" /><w:ind w:right-chars=\"-46\" />{1}</w:pPr>" +
      "<w:r><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/>"+
"<wx:sym wx:font=\"Wingdings\" wx:char=\"F0FC\"/><w:kern w:val=\"0\"/><w:lang w:val=\"ZH-TW\"/><w:noProof />{2}<w:sz w:val=\"{3}\"/></w:rPr><w:t>{4}</w:t></w:r></w:p>";
            string fontname = "Wingdings";
            string centerflag = "<w:jc w:val=\"center\" />";
            string whitecolorflag = "<w:color w:val=\"FFFFFF\" />";
            string JustLeft50 = "<w:ind w:left-chars=\"-50\" />";
            string t1 = "";
            string t2 = "";
            string t3 = "";
            if (justleft) t1 = JustLeft50;
            if (center) t2 = centerflag;
            if (whitecolor) t3 = whitecolorflag;
            return String.Format(itemFormat, t1, t2, t3, "28", content, fontname);
        }
        public virtual String getMemo(String Key, String fontSize, bool alignCenter, bool justleft)
        {
            string fieldname = Key.Substring(2, Key.Length - 2);
            string fontname = "²Ó©úÅé";
            if (fieldname_fontname_dict != null && fieldname_fontname_dict.ContainsKey(fieldname)) fontname = fieldname_fontname_dict[fieldname].ToString();
            string temp=parseData(Key);
            string r="";
            if(temp.Trim()=="") return r;
            int posi=0;
           string subs;
            int i=0;
            for(i=0 ; i<temp.Length;i++)
            {
                if(temp[i]=='\r' ) posi++;
                if (temp[i] == '\n')
                {
                    subs = temp.Substring(posi, i - posi);
                    if (subs == "") { }
                    else { 
                        if (subs[0] == '@') 
                        { r += GetMemoItem(alignCenter, true, justleft, fontSize, subs,fontname); } 
                        else
                        { r += GetMemoItem(alignCenter, false, justleft, fontSize, subs,fontname); } 
                    }
                    posi = i + 1;
                }
                
            }
            if(posi<i)
            {
                subs=temp.Substring(posi,i-posi);
                if(! (subs.Trim()=="")){
                      if (subs[0] == '@') 
                        { r += GetMemoItem(alignCenter, true, justleft, fontSize, subs,fontname); } 
                       else
                        { r += GetMemoItem(alignCenter, false, justleft, fontSize, subs,fontname); } 
                }
            }
            if(r.Length==0) return "<w:p></w:p>";
            return r;      
        }

        public virtual String getMemoPCounduct(String Key) { return null; }
        public virtual String getMemoSex(String Key)
        {
            string temp = parseData(Key);
            //String str_fmt = "<w:p wsp:rsidR=\"00F64FB2\" wsp:rsidRDefault=\"00A73CF1\"><w:pPr><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><w:kern w:val=\"0\"/><w:lang w:val=\"ZH-TW\"/></w:rPr></w:pPr><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¨k </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¤k</w:t></w:r></w:p>";
            String str_fmt = "<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¨k </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¤k</w:t></w:r>";
            if (temp == "M")
            {
                return string.Format(str_fmt,Windings_Checked,Windings_Null);
                }
                else if (temp == "F")
                {
                    return string.Format(str_fmt, Windings_Null, Windings_Checked);
                
            }
            else{
                return string.Format(str_fmt, Windings_Null, Windings_Null);
            }
        }
        public virtual String getMemoPLACE(String Key)
        {
            String temp=parseData(Key);
            String str_fmt = "<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¿Dªù </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¤º¦a </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{2}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¨ä¥L(½Ð«ü©ú)</w:t></w:r>";
            if(temp=="1"){
                return String.Format(str_fmt,  Windings_Checked, Windings_Null,Windings_Null);
            }else
            if(temp=="2"){
                return String.Format(str_fmt, Windings_Null,Windings_Checked, Windings_Null);
            }
            else
                if (temp == "")
                {
                    return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Null);
                }
                else
                {
                    return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Checked);
                }
        }
        public virtual String getMemoIDTYPE(String Key)
        {
            string temp = parseData(Key);
            string str_fmt="<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¥Ã¤[©~¥Á¨­¥÷ÃÒ  </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>«D¥Ã¤[©~¥Á¨­¥÷ÃÒ </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{2}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¨ä¥L(½Ð«ü©ú)</w:t></w:r>";
            if (temp == "BIRP")
            {
                return String.Format(str_fmt, Windings_Checked, Windings_Null, Windings_Null);
            }
            else if (temp == "BIRNP")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Checked, Windings_Null);
            }
            else if (temp == "")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Null);
            }
            else
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Checked);
            }
            
        }
        public virtual String getMemoRAREA(String Key)
        {
            string temp = parseData(Key);
            string str_fmt = "<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¿Dªù/œ®¥J/¸ôÀô </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¤º¦a </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{2}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"18\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¨ä¥L(½Ð«ü©ú)</w:t></w:r>";
            if(temp=="M")
            {
                return String.Format(str_fmt, Windings_Checked, Windings_Null, Windings_Null);
            }
            else if (temp == "C")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Checked, Windings_Null);
            }
            else if (temp == "")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Null);
            }
            else
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Checked);
            }
            
            
        }
        String Windings_Null = "„Y";
        String Windings_Checked = "„Ñ";
            
        public virtual String getMemoAREA(String Key)
        {

            string temp = parseData(Key);
            string str_fmt = "<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"18\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¿Dªù </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>œ®¥J </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{2}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¸ôÀô</w:t></w:r>";
            if (temp == "M")
            {
                return String.Format(str_fmt, Windings_Checked, Windings_Null, Windings_Null);
            }
            else if (temp == "T")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Checked, Windings_Null);
            }
            else if (temp == "C")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Checked);
            }
            else
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Null);
            }
        }
        public virtual String getMemoL7AREA(String Key)
        {
            string temp = parseData(Key);
            string str_fmt = "<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"18\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¿Dªù </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¸ôÀô </w:t></w:r>";
            if (temp == "M")
            {
                return String.Format(str_fmt, Windings_Checked, Windings_Null);
            }
            else if (temp == "C")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Checked);
            }
            else
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null);
            }
        }
        public virtual String getMemoL8AREA(String Key)
        {
            string temp = parseData(Key);
            string str_fmt = "<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"18\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>œ®¥J </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¤º¦a(½Ð¶ñ¼g¶l¬F½s¸¹) </w:t></w:r>";
            if (temp == "T")
            {
                return String.Format(str_fmt, Windings_Checked, Windings_Null);
            }
            else if (temp == "L")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Checked);
            }
            else
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null);
            }
        }
        public virtual String getMemoGUARD(String Key)
        {
            string temp = parseData(Key);
            string str_fmt = "<w:r></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0A8\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{0}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¤÷¿Ë  </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{1}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¥À¿Ë </w:t></w:r><w:r wsp:rsidRPr=\"00A73CF1\"><w:rPr><w:rFonts w:ascii=\"Wingdings\" w:h-ansi=\"Wingdings\" w:cs=\"Wingdings\"/><wx:font wx:val=\"Wingdings\"/><wx:sym wx:font=\"Wingdings\" wx:char=\"F0FE\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>{2}</w:t></w:r><w:r wsp:rsidR=\"00F64FB2\" wsp:rsidRPr=\"00F64FB2\"><w:rPr><w:rFonts w:ascii=\"²Ó©úÅé\" w:fareast=\"²Ó©úÅé\" w:h-ansi=\"²Ó©úÅé\" w:cs=\"Wingdings\" w:hint=\"fareast\"/><wx:font wx:val=\"²Ó©úÅé\"/><w:kern w:val=\"0\"/><w:sz w:val=\"16\"/><w:lang w:val=\"ZH-TW\"/></w:rPr><w:t>¨ä¥L(½Ð«ü©ú)</w:t></w:r>";
            if (temp == "F")
            {
                return String.Format(str_fmt, Windings_Checked, Windings_Null, Windings_Null);
            }
            else if (temp == "M")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Checked, Windings_Null);
            }
            else if (temp == "O")
            {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Checked);
            }
            else {
                return String.Format(str_fmt, Windings_Null, Windings_Null, Windings_Null);
            }
        }
        public virtual String getPict(String Key) { return null; }
        public virtual String getPICTCODE() { return ""; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="fontSize"></param>
        /// <param name="aDR"></param>
        /// <returns></returns>
        public virtual String getFielddata(String Key, String fontSize,DataRow aDR)
        {
            a_datarow = aDR;
            char t = Key[0];
            switch (t)
            {
                case 'L':
                    if (Key[1] == 'C') { return getMemo(Key, fontSize, true, false); }
                    else if (Key[1] == 'J') { return getMemo(Key, fontSize, true, true); }
                    else if (Key[1] == 'M') { return getMemoPCounduct(Key); }
                    else if (Key[1] == '1') { return getMemoSex(Key); }
                    else if (Key[1] == '2') { return getMemoPLACE(Key); }
                    else if (Key[1] == '3') { return getMemoIDTYPE(Key); }
                    else if (Key[1] == '4') { return getMemoRAREA(Key); }
                    else if (Key[1] == '5') { return getMemoAREA(Key); }
                    else if (Key[1] == '6') { return getMemoGUARD(Key); }
                    else if (Key[1] == '7') { return getMemoL7AREA(Key); }
                    else if (Key[1] == '8') { return getMemoL8AREA(Key); }
                    else return getMemo(Key, fontSize, false, false);
                case 'T': return getText(Key);
                case 'P': return getPict(Key);
                default:
                    return "ERROR" + Key;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public virtual String parseData(String Key)
        {
            string res = "";
            string feildvalue="";
            string fieldname = Key.Substring(2, Key.Length - 2);
            try{
                feildvalue = a_datarow[fieldname].ToString();
            }catch{
                feildvalue="ERR_FIELD"+Key;
            }
            foreach (char c in feildvalue)
            {
                if(c=='>') {res+="&gt;";}
                else if(c=='<') {res+="&lt";}
                else res+=c;
            }
            return res;
        }
    }
}
