using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Collections;
using System.Data;
namespace ES_FORMS.STFORMS
{
    public class FormStud_Ctr_ITEM
    {

        public FormStud_Ctr_ITEM(String cn, string fn)
        {
            Crt_Name = cn;
            Field_Name = fn;
            _Data = "";
            ori_Data = "";
            ctr = null;
        }

        public FormStud_Ctr_ITEM(String cn, string fn, string da, Control c)
        {
            Crt_Name = cn;
            Field_Name = fn;
            _Data = da;
            ori_Data = da;
            ctr = c;
        }
        public String Crt_Name;
        public String Field_Name;
        public String Data { get { return _Data; } set { _Data = value; } }
        public String ori_Data;
        public Control ctr = null;
        private String _Data;
    }
    public class FormStud_Ctr_ITEMS : IEnumerable<FormStud_Ctr_ITEM>
    {
        List<FormStud_Ctr_ITEM> _elements;
        public FormStud_Ctr_ITEMS(FormStud_Ctr_ITEM[] array)
        {
            this._elements = new List<FormStud_Ctr_ITEM>(array);
        }
        IEnumerator<FormStud_Ctr_ITEM> IEnumerable<FormStud_Ctr_ITEM>.GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }
    }
    interface FormStud_Act_Interface
    {
        int create_guard_rc(String STUD_REF);

        void Save();
        void Set_Data_Contr();
        void data_backup(string tablename, OdbcDataReader dr);

        void fill_control_text();

        void control_textbox_change(Object sender, EventArgs e);

        void control_combobox_change(Object sender, EventArgs e);

        void txtboxReadOnly();

        void txtboxEditMode();

        void log();
        int change_cnt { get; set; }
    }
    public abstract class FormStud_Act_Interface_abstract : FormStud_Act_Interface
    {
        public int create_guard_rc(String STUD_REF)
        {
            return 0;
        }
        public void Save() { }
        public void Set_Data_Contr() { }
        public void data_backup(string tablename, OdbcDataReader dr) { }

        public void fill_control_text() { }

        public void control_textbox_change(Object sender, EventArgs e) { }

        public void control_combobox_change(Object sender, EventArgs e) { }

        public void txtboxReadOnly() { }

        public void txtboxEditMode() { }

        public void log() { }
        int _change_cnt = 0;
        public int change_cnt { get { return _change_cnt; } set { _change_cnt = value; } }

    }
    public class FormStudInfo_act00 : FormStud_Act_Interface
    {
        private Label label1 = null;
        private ComboBox comboBox1 = null;
        private ComboBox comboBox2 = null;
        private ComboBox comboBox3 = null;
        private ComboBox comboBox4 = null;
        private ComboBox comboBox5 = null;
        private ComboBox comboBox6 = null;
        private ComboBox comboBox7 = null;
        private ComboBox comboBox8 = null;
        private ComboBox comboBox9 = null;
        private ComboBox comboBox10 = null;
        private ComboBox comboBox11 = null;
        private ComboBox comboBox12 = null;
        private ComboBox comboBox13 = null;
        private ComboBox comboBox14 = null;
        private Hashtable MFieldList = new Hashtable();
        private ErrorProvider errorProvider1 = null;
        /// <summary>
        /// 重要動作:FormStudInfo_ACT
        /// </summary>
        /// <param name="form"></param>
        /// <param name="pStudRef"></param>
        /// <param name="pErrorProvider"></param>
        /// <param name="OdbcConnection"></param>
        public FormStudInfo_act00(Form form, String pStudRef, ErrorProvider pErrorProvider, OdbcConnection pconn)
        {
            conn = pconn;
            actForm = form;
            label1 = (Label)actForm.Controls["label1"];
            comboBox1 = (ComboBox)actForm.Controls["comboBox1"];
            comboBox2 = (ComboBox)actForm.Controls["comboBox2"];
            comboBox3 = (ComboBox)actForm.Controls["comboBox3"];
            comboBox4 = (ComboBox)actForm.Controls["comboBox4"];
            comboBox5 = (ComboBox)actForm.Controls["comboBox5"];
            comboBox6 = (ComboBox)actForm.Controls["comboBox6"];
            comboBox7 = (ComboBox)actForm.Controls["comboBox7"];
            comboBox8 = (ComboBox)actForm.Controls["comboBox8"];
            comboBox9 = (ComboBox)actForm.Controls["comboBox9"];
            comboBox10 = (ComboBox)actForm.Controls["comboBox10"];
            comboBox11 = (ComboBox)actForm.Controls["comboBox11"];
            comboBox12 = (ComboBox)actForm.Controls["comboBox12"];
            comboBox13 = (ComboBox)actForm.Controls["comboBox13"];
            comboBox14 = (ComboBox)actForm.Controls["comboBox14"];
            errorProvider1 = pErrorProvider;
            Set_Data_Contr();
            this.STUD_REF = pStudRef;
            actForm.Text = String.Format("校內編號:{0}", STUD_REF);
            string year_sql = "select curr_year from year;";
            OdbcDataReader year_dr = new OdbcCommand(year_sql, conn).ExecuteReader();
            while (year_dr.Read())
            {
                YEAR = year_dr[0].ToString();
            }
            label1.Text = YEAR;
            foreach (String s in StudMain_Data_Schema.GetInst.PLACE_List)
            {
                comboBox1.Items.Add(s);
                comboBox3.Items.Add(s);
            }
            comboBox1.MaxDropDownItems = 50;
            comboBox3.MaxDropDownItems = 50;
            foreach (string s in StudMain_Data_Schema.GetInst.ID_List)
            {
                comboBox2.Items.Add(s);
            }
            comboBox2.MaxDropDownItems = 50;
            foreach (string s in StudMain_Data_Schema.GetInst.NATION_List)
            {
                comboBox4.Items.Add(s);
            }
            comboBox4.MaxDropDownItems = 20;
            comboBox5.Items.Clear();
            foreach (string s in StudMain_Data_Schema.GetInst.NIGHT_ARE_List)
            {
                comboBox5.Items.Add(s);
            }
            comboBox6.Items.Clear(); comboBox9.Items.Clear(); comboBox10.Items.Clear();
            foreach (string s in StudMain_Data_Schema.GetInst.AREA_List)
            {
                comboBox6.Items.Add(s); comboBox9.Items.Add(s); comboBox10.Items.Add(s);
            }
            comboBox7.Items.Clear();
            foreach (string s in StudMain_Data_Schema.GetInst.RELA_List)
            {
                comboBox7.Items.Add(s);
            }
            comboBox8.Items.Clear(); comboBox8.Items.Add("1"); comboBox8.Items.Add("0");
            comboBox11.Items.Clear();
            foreach (string s in StudMain_Data_Schema.GetInst.ST_STATUS_List)
            {
                comboBox11.Items.Add(s);
            }

            comboBox12.Items.Clear();
            foreach (string s in StudMain_Data_Schema.GetInst.S6_List)
            {
                comboBox12.Items.Add(s);
            }

            comboBox13.Items.Clear(); comboBox13.Items.Add("M=男"); comboBox13.Items.Add("F=女");

            string studmain_sql = string.Format("select * from studmain where stud_Ref='{0}';", STUD_REF);
            OdbcDataReader studmain_dr = new OdbcCommand(studmain_sql, conn).ExecuteReader();
            if (studmain_dr.HasRows)
            {
                studmain_dr.Read();
                data_backup("studmain", studmain_dr);
                string studtran_sql = string.Format("select * from studtran where stud_Ref='{0}' and year='{1}';", STUD_REF, YEAR);
                OdbcDataReader studtran_dr = new OdbcCommand(studtran_sql, conn).ExecuteReader();
                if (studtran_dr.HasRows)
                {
                    studtran_dr.Read();
                    data_backup("studtran", studtran_dr);
                }
                else
                {
                    /*  OdbcCommand addstudtran_cmd = new OdbcCommand(string.Format("insert into studtran (stud_ref,year) values('{0}','{1}');", STUD_REF, YEAR), conn);
                      int cnt = addstudtran_cmd.ExecuteNonQuery();
                      MessageBox.Show("新增STUDTRAN " + cnt + "筆記錄");
                     * */
                    data_backup("studtran", studtran_dr);
                }
                string Guard_sql = string.Format("select * from guard where stud_Ref='{0}';", STUD_REF);
                OdbcDataReader Guard_dr = new OdbcCommand(Guard_sql, conn).ExecuteReader();
                if (Guard_dr.HasRows)
                {
                    Guard_dr.Read();
                    data_backup("guard", Guard_dr);
                }
                else
                {
                    MessageBox.Show("新增GUARD " + create_guard_rc(STUD_REF) + "筆記錄");
                    data_backup("guard", Guard_dr);
                }
            }
            else
            {
                throw new Exception("STUDMAIN " + STUD_REF + " 沒有記錄!");
            }
            fill_control_text();
        }
        public int change_cnt
        {
            get
            {
                _change_cnt = 0;
                foreach (FormStud_Ctr_ITEM ci in fCtrItems)
                {
                    if (ci.Data != ci.ori_Data) { _change_cnt++; }
                }
                return this._change_cnt;
            }
            set { this._change_cnt = value; }
        }
        public virtual int create_guard_rc(String STUD_REF)
        {
            OdbcCommand addguard_cmd = new OdbcCommand(string.Format("insert into guard (stud_ref) values('{0}');", STUD_REF), conn);
            return addguard_cmd.ExecuteNonQuery();

        }

        private Form actForm = null;
        public string STUD_REF = null;
        public string YEAR = "";
        public OdbcConnection conn = null;

        private Hashtable Nation_htb = StudMain_Data_Schema.GetInst.NATION_HTB;
        private Hashtable area_htb = StudMain_Data_Schema.GetInst.AREA_HTB;
        private Hashtable night_area_htb = StudMain_Data_Schema.GetInst.NIGHT_AREA_HTB;
        private Hashtable rela_htb = StudMain_Data_Schema.GetInst.RELA_HTB;
        private Hashtable s6_htb = StudMain_Data_Schema.GetInst.S6_HTB;
        private Hashtable id_htb = StudMain_Data_Schema.GetInst.ID_HTB;
        private Hashtable place_htb = StudMain_Data_Schema.GetInst.PLACE_HTB;
        private Hashtable st_status_htb = StudMain_Data_Schema.GetInst.ST_STATUS_HTB;

        private Hashtable data_value = new Hashtable();
        // protected static Hashtable data_control = null;
        protected static Hashtable data_type = null;
        protected static Hashtable data_columnsize = null;
        protected static FormStud_Ctr_ITEMS fCtrItems = null;

        public void Set_Data_Contr()
        {
            if (fCtrItems == null)
            {
                FormStud_Ctr_ITEM[] ci_ar = { 
                new  FormStud_Ctr_ITEM("textBox1", "studmain.CODE"),
                new  FormStud_Ctr_ITEM("textBox2", "studmain.CHECKDIGIT"),
                new  FormStud_Ctr_ITEM("textBox8", "studmain.NAME_C"),
                new  FormStud_Ctr_ITEM("textBox9", "studmain.NAME_P"),
                new  FormStud_Ctr_ITEM("textBox11", "studmain.B_DATE"),
                new  FormStud_Ctr_ITEM("textBox12", "studmain.ID_NO"),
                new  FormStud_Ctr_ITEM("textBox13", "studmain.I_DATE"),
                new  FormStud_Ctr_ITEM("textBox14", "studmain.V_DATE"),
                new  FormStud_Ctr_ITEM("textBox15", "studmain.S6_IDATE"),
                new  FormStud_Ctr_ITEM("textBox16", "studmain.S6_VDATE"),
                new  FormStud_Ctr_ITEM("textBox17", "studmain.ORIGIN"),
                new  FormStud_Ctr_ITEM("textBox18", "studmain.RA_DESC"),
                new  FormStud_Ctr_ITEM("textBox19", "studmain.ROAD"),
                new  FormStud_Ctr_ITEM("textBox20", "studmain.ADDRESS"),
                new  FormStud_Ctr_ITEM("textBox21", "studmain.TEL"),
                new  FormStud_Ctr_ITEM("textBox22", "studmain.FATHER"),
                new  FormStud_Ctr_ITEM("textBox24", "studmain.MOTHER"),
                new  FormStud_Ctr_ITEM("textBox23", "studmain.F_PROF"),
                new  FormStud_Ctr_ITEM("textBox25", "studmain.M_PROF"),
                new  FormStud_Ctr_ITEM("textBox27", "studmain.GUARDMOBIL"),
                new  FormStud_Ctr_ITEM("textBox33", "studmain.EC_NAME"),
                new  FormStud_Ctr_ITEM("textBox34", "studmain.EC_REL"),
                new  FormStud_Ctr_ITEM("textBox35", "studmain.EC_ROAD"),
                new  FormStud_Ctr_ITEM("textBox36", "studmain.EC_ADDRESS"),
                new  FormStud_Ctr_ITEM("textBox37", "studmain.EC_TEL"),
                new  FormStud_Ctr_ITEM("textBox39", "studmain.F_tel1"),
                new  FormStud_Ctr_ITEM("textBox40", "studmain.F_tel2"),
                new  FormStud_Ctr_ITEM("textBox41", "studmain.M_tel1"),
                new  FormStud_Ctr_ITEM("textBox42", "studmain.M_tel2"),
                new  FormStud_Ctr_ITEM("textBox43", "studmain.G_tel1"),
                new  FormStud_Ctr_ITEM("textBox44", "studmain.G_tel2"),
                new  FormStud_Ctr_ITEM("textBox45", "studmain.Parent_sms"),
                new  FormStud_Ctr_ITEM("textBox46", "studmain.Stud_sms"),
                new  FormStud_Ctr_ITEM("textBox47", "studmain.Reg_in_date"),
                new  FormStud_Ctr_ITEM("textBox48", "studmain.Reg_in_Class"),
                new  FormStud_Ctr_ITEM("textBox49", "studmain.Leave_date"),
                new  FormStud_Ctr_ITEM("textBox50", "studmain.Leave_Class"),
                new  FormStud_Ctr_ITEM("textBox51", "studmain.Leave_reason"),
                new  FormStud_Ctr_ITEM("textBox52", "studmain.Religion"),
                new  FormStud_Ctr_ITEM("textBox3", "studtran.S_CODE"),
                new  FormStud_Ctr_ITEM("textBox4", "studtran.GRADE"),
                new  FormStud_Ctr_ITEM("textBox5", "studtran.CLASS"),
                new  FormStud_Ctr_ITEM("textBox6", "studtran.C_NO"),
                new  FormStud_Ctr_ITEM("textBox7", "studtran.DATE_IN"),
                new  FormStud_Ctr_ITEM("textBox28", "guard.NAME"),
                new  FormStud_Ctr_ITEM("textBox29", "guard.PROFESSION"),
                new  FormStud_Ctr_ITEM("textBox30", "guard.ROAD"),
                new  FormStud_Ctr_ITEM("textBox31", "guard.ADDRESS"),
                new  FormStud_Ctr_ITEM("textBox32", "guard.TEL"),
                new  FormStud_Ctr_ITEM("textBox10", "guard.RELATION"),
                new  FormStud_Ctr_ITEM("comboBox10", "studmain.EC_AREA"),
                new  FormStud_Ctr_ITEM("comboBox11", "studmain.St_status"),
                new  FormStud_Ctr_ITEM("comboBox12", "studmain.S6_TYPE"),
                new  FormStud_Ctr_ITEM("comboBox13", "studmain.SEX"),
                new  FormStud_Ctr_ITEM("comboBox2", "studmain.ID_TYPE"),
                new  FormStud_Ctr_ITEM("comboBox4", "studmain.NATION"),
                new  FormStud_Ctr_ITEM("comboBox5", "studmain.R_AREA"),
                new  FormStud_Ctr_ITEM("comboBox6", "studmain.AREA"),
                new  FormStud_Ctr_ITEM("comboBox7", "studmain.GUARD"),
                new  FormStud_Ctr_ITEM("comboBox8", "studmain.LIVE_SAME"),
                new  FormStud_Ctr_ITEM("comboBox9", "guard.AREA"),
                new  FormStud_Ctr_ITEM("comboBox1", "studmain.B_PLACE"),
                new  FormStud_Ctr_ITEM("comboBox3", "studmain.I_PLACE"),
                new  FormStud_Ctr_ITEM("comboBox14", "studmain.MBC_STUD"),
                new  FormStud_Ctr_ITEM("textBox26", "studmain.K_CLASS"),
                new  FormStud_Ctr_ITEM("textBox38", "studmain.K_SCHOOL"),
                new  FormStud_Ctr_ITEM("textBox53", "studmain.K_EDU"),
                new  FormStud_Ctr_ITEM("textBox54", "studmain.P_CLASS"),
                new  FormStud_Ctr_ITEM("textBox55", "studmain.P_SCHOOL"),
                new  FormStud_Ctr_ITEM("textBox56", "studmain.P_EDU"),
                new  FormStud_Ctr_ITEM("textBox57", "studmain.S_CLASS"),
                new  FormStud_Ctr_ITEM("textBox58", "studmain.S_SCHOOL"),
                new  FormStud_Ctr_ITEM("textBox59", "studmain.S_EDU"),
                new  FormStud_Ctr_ITEM("textBox60", "studmain.note"),
                new  FormStud_Ctr_ITEM("textBox61", "studmain.POSTAL_CODE"),
                new  FormStud_Ctr_ITEM("textBox62", "guard.POSTAL_CODE"),
                new  FormStud_Ctr_ITEM("textBox63", "studmain.EC_POSTAL_CODE"),
                new  FormStud_Ctr_ITEM("textBox64", "studmain.MOBILE")
                };
                fCtrItems = new FormStud_Ctr_ITEMS(ci_ar);
            }
        }
        public void data_backup(string tablename, OdbcDataReader dr)
        {
            if (data_type == null) data_type = new Hashtable();
            if (data_columnsize == null) data_columnsize = new Hashtable();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                string key = tablename + "." + dr.GetName(i);
                if (!data_type.ContainsKey(key))
                {
                    data_type.Add(key, dr.GetDataTypeName(i));
                }
                if (!data_columnsize.ContainsKey(key))
                {
                    DataRow[] rows = dr.GetSchemaTable().Select(string.Format("ColumnName='{0}'", dr.GetName(i)));
                    data_columnsize.Add(key, rows[0]["ColumnSize"].ToString());
                }
                if (data_type[key].ToString() == "date")
                {
                    if (dr.HasRows)
                    {
                        data_value.Add(key, string.Format("{0:yyyy/MM/dd}", dr[i]));
                    }
                    else
                    {
                        data_value.Add(key, "");
                    }
                }
                else
                {
                    if (dr.HasRows)
                    {
                        data_value.Add(key, dr[i].ToString());
                    }
                    else
                    {
                        data_value.Add(key, "");
                    }
                }
            }
        }
        public void fill_control_text()
        {
            foreach (FormStud_Ctr_ITEM ci in fCtrItems)
            {
                if (actForm.Controls.ContainsKey(ci.Crt_Name))
                {
                    ci.ctr = actForm.Controls[ci.Crt_Name];
                    if (ci.Crt_Name.StartsWith("textBox"))
                    {
                        TextBox tb = (TextBox)actForm.Controls[ci.Crt_Name];
                        if (data_value.ContainsKey(ci.Field_Name))
                        {
                            ci.Data = data_value[ci.Field_Name].ToString();
                            ci.ori_Data = ci.Data;
                            Binding b = new Binding("Text", ci, "Data");
                            tb.DataBindings.Add(b);
                        }
                    }
                    else if (ci.Crt_Name.StartsWith("comboBox"))
                    {
                        string dvalue = data_value[ci.Field_Name].ToString().Trim();
                        ComboBox tb = (ComboBox)actForm.Controls[ci.Crt_Name];
                        if (ci.Field_Name == "studmain.B_PLACE" || ci.Field_Name == "studmain.I_PLACE")
                        {
                            if (place_htb.ContainsKey(dvalue)) ci.Data = place_htb[dvalue].ToString();
                        }
                        else if (ci.Field_Name == "studmain.NATION")
                        {
                            if (Nation_htb.ContainsKey(dvalue)) ci.Data = Nation_htb[dvalue].ToString();
                        }
                        else if (ci.Field_Name == "studmain.R_AREA")
                        {
                            if (night_area_htb.ContainsKey(dvalue)) ci.Data = night_area_htb[dvalue].ToString();
                        }
                        else if (ci.Field_Name == "studmain.AREA" || ci.Field_Name.ToString() == "studmain.EC_AREA" || ci.Field_Name == "guard.AREA")
                        {
                            if (area_htb.ContainsKey(dvalue)) ci.Data = area_htb[dvalue].ToString();
                        }
                        else if (ci.Field_Name == "studmain.GUARD")
                        {
                            if (rela_htb.ContainsKey(dvalue)) ci.Data = rela_htb[dvalue].ToString();
                        }
                        else if (ci.Field_Name == "studmain.S6_TYPE")
                        {
                            if (s6_htb.ContainsKey(dvalue)) s6_htb[dvalue].ToString();
                        }
                        else if (ci.Field_Name == "studmain.ID_TYPE")
                        {
                            if (id_htb.ContainsKey(dvalue)) ci.Data = id_htb[dvalue].ToString();
                        }
                        else if (ci.Field_Name == "studmain.St_status")
                        {
                            if (st_status_htb.ContainsKey(dvalue)) ci.Data = st_status_htb[dvalue].ToString();
                        }
                        else
                        {
                            ci.Data = dvalue;
                        }
                        ci.ori_Data = ci.Data;
                        Binding b = new Binding("Text", ci, "Data");
                        tb.DataBindings.Add(b);
                    }
                }
            }
        }
        public int _change_cnt = 0;
        public void Save()
        {
            foreach (FormStud_Ctr_ITEM ci in fCtrItems)
            {
                if (ci.Data != ci.ori_Data)
                {
                    if (ci.Crt_Name.StartsWith("textBox"))
                    {
                        string table_field = ci.Field_Name;
                        string[] temp = table_field.Split('.');
                        string tablename = temp[0];
                        string f_name = temp[1];
                        int colmnsize = int.Parse(data_columnsize[table_field].ToString());
                        string f_value = ci.Data.Replace('\\', ' ');
                        if (f_name.Equals("CHECKDIGIT")) f_value = f_value.Trim();
                        if (data_type[table_field].ToString() == "date")
                        {
                            DateTime Test;
                            if (ci.Data == "")
                            {
                                this.errorProvider1.SetError(ci.ctr, "");
                            }
                            else if (DateTime.TryParseExact(ci.Data, "yyyy/M/d", null, System.Globalization.DateTimeStyles.None, out Test) == false)
                            {
                                this.errorProvider1.SetError(ci.ctr, "數據錯誤，請輸入日期（yyyy/MM/dd）！"); return;
                            }
                            else
                                this.errorProvider1.SetError(ci.ctr, "");
                        }
                        else if (ci.Data.Length > colmnsize) f_value = ci.Data.Substring(0, colmnsize);
                        //   if (data_value[data_control[tb.Name].ToString()].ToString()!= tb.Text)
                        //{
                        if (!MFieldList.Contains(table_field)) MFieldList.Add(table_field, data_value[table_field].ToString());
                        string sql = "";
                        if (tablename == "studtran")
                        {
                            sql = string.Format("update {0} set {1}='{2}' where stud_Ref ='{3}' and Year='{4}' ;", tablename, f_name, f_value, STUD_REF, YEAR);
                        }
                        else
                        {
                            sql = string.Format("update {0} set {1}='{2}' where stud_Ref ='{3}' ;", tablename, f_name, f_value, STUD_REF);
                        }
                        using (OdbcCommand cmd = new OdbcCommand(sql, conn))
                        {
                            change_cnt += cmd.ExecuteNonQuery();
                        }
                    }
                    else if (ci.Crt_Name.StartsWith("comboBox"))
                    {
                        string table_field = ci.Field_Name.ToString();
                        string[] temp = table_field.Split('.');
                        string tablename = temp[0];
                        string f_name = temp[1];
                        int colmnsize = int.Parse(data_columnsize[table_field].ToString());
                        string f_value = ci.Data;
                        if (data_type[table_field].ToString() == "varchar")
                        {
                            if (ci.Data.Length > colmnsize) f_value = ci.Data.Substring(0, colmnsize).Trim();
                        }
                        else if (data_type[table_field].ToString() == "int")
                        {
                            int o_int;
                            f_value = ci.Data.Split('=')[0].Trim();
                            if (int.TryParse(f_value, out o_int) == false)
                            {
                                this.errorProvider1.SetError(ci.ctr, "數據錯誤，請輸入數字！");
                            }
                            else
                                this.errorProvider1.SetError(ci.ctr, "");
                        }

                        if (!MFieldList.Contains(table_field)) MFieldList.Add(table_field, data_value[table_field].ToString());

                        string sql = "";
                        if (tablename == "studtran")
                        {
                            sql = string.Format("update {0} set {1}='{2}' where stud_Ref ='{3}' and Year='{4}' ;", tablename, f_name, f_value, STUD_REF, YEAR);
                        }
                        else
                        {
                            sql = string.Format("update {0} set {1}='{2}' where stud_Ref ='{3}' ;", tablename, f_name, f_value, STUD_REF);
                        }
                        using (OdbcCommand cmd = new OdbcCommand(sql, conn))
                        {
                            change_cnt += cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
        }
        public virtual void control_textbox_change(Object sender, EventArgs e)
        {
        }
        public virtual void control_combobox_change(Object sender, EventArgs e)
        {
        }
        public void txtboxReadOnly()
        {
            foreach (Control c in actForm.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = (TextBox)c;
                    tb.ReadOnly = true;
                }
                else if (c is ComboBox)
                {
                    ComboBox tb = (ComboBox)c;
                    tb.Enabled = false;
                }
            }
        }
        public virtual void txtboxEditMode()
        {
            foreach (Control c in actForm.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = (TextBox)c;
                    tb.ReadOnly = !tb.ReadOnly;
                }
                else if (c is ComboBox)
                {
                    ComboBox tb = (ComboBox)c;
                    tb.Enabled = !tb.Enabled;
                }
            }
        }
        public void log()
        {
            String f = "";
            String v = "";
            foreach (DictionaryEntry e in MFieldList)
            {
                if (f.Length < 500) f += e.Key + "_";
                if (v.Length < 250) v += e.Value.ToString().Replace('\'', ' ').Replace(';', ' ').Replace('\n', ' ') + "_";
            }
            if (f == "") return;
            /*
            String f_sql = String.Format("insert into log (user,stud_ref,mdt,logd)values('{0}','{1}',now(),'{2}');", es_lib.Publib.Pub.cfg.curr_userinfo.userid, STUD_REF, f);
            using (OdbcCommand cmd = new OdbcCommand(f_sql, conn))
            {
                int change_cnt = cmd.ExecuteNonQuery();
            }*/
            //String v_sql = String.Format("insert into log (user,stud_ref,mdt,logd)values('{0}','{1}',datetime('now'),'{2}');", es_lib.Publib.Pub.cfg.curr_userinfo.userid, STUD_REF, v);
            //KD_DBF_TOOLS.Log2SQLITE(v_sql);
        }
    }
    public class FormStudInfo_act00_readonly : FormStudInfo_act00
    {
        public FormStudInfo_act00_readonly(Form form, String pStudRef, ErrorProvider pErrorProvider, String oldyear, OdbcConnection pconn)
            : base(form, pStudRef, pErrorProvider, pconn)
        {
        }
    }
    public class StudMain_Data_Schema
    {
        private static OdbcConnection _conn = null;
        public static OdbcConnection conn
        {
            set { _conn = value; }
        }
        private StudMain_Data_Schema()
        {
            STUD_MAIN_FIELDNAME_LIST.Add("a.NAME_C");
            STUD_MAIN_FIELDNAME_LIST.Add("a.NAME_P");
            STUD_MAIN_FIELDNAME_LIST.Add("a.SEX");
            STUD_MAIN_FIELDNAME_LIST.Add("a.B_DATE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.B_PLACE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.ID_TYPE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.ID_NO");
            STUD_MAIN_FIELDNAME_LIST.Add("a.I_PLACE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.I_DATE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.V_DATE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.S6_TYPE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.S6_IDATE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.S6_VDATE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.NATION");
            STUD_MAIN_FIELDNAME_LIST.Add("a.ORIGIN");
            STUD_MAIN_FIELDNAME_LIST.Add("a.R_AREA");
            STUD_MAIN_FIELDNAME_LIST.Add("a.RA_DESC");
            STUD_MAIN_FIELDNAME_LIST.Add("a.AREA");
            STUD_MAIN_FIELDNAME_LIST.Add("a.POSTAL_CODE");//modify by cool
            STUD_MAIN_FIELDNAME_LIST.Add("a.ROAD");
            STUD_MAIN_FIELDNAME_LIST.Add("a.ADDRESS");
            STUD_MAIN_FIELDNAME_LIST.Add("a.TEL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.MOBILE");
            STUD_MAIN_FIELDNAME_LIST.Add("a.FATHER");
            STUD_MAIN_FIELDNAME_LIST.Add("a.MOTHER");
            STUD_MAIN_FIELDNAME_LIST.Add("a.F_PROF");
            STUD_MAIN_FIELDNAME_LIST.Add("a.M_PROF");
            STUD_MAIN_FIELDNAME_LIST.Add("a.GUARD");
            STUD_MAIN_FIELDNAME_LIST.Add("a.LIVE_SAME");
            STUD_MAIN_FIELDNAME_LIST.Add("a.EC_NAME");
            STUD_MAIN_FIELDNAME_LIST.Add("a.EC_REL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.EC_TEL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.EC_AREA");
            STUD_MAIN_FIELDNAME_LIST.Add("a.EC_POSTAL_CODE");//modify by cool
            STUD_MAIN_FIELDNAME_LIST.Add("a.EC_ROAD");
            STUD_MAIN_FIELDNAME_LIST.Add("a.EC_ADDRESS");
            STUD_MAIN_FIELDNAME_LIST.Add("b.S_CODE");
            STUD_MAIN_FIELDNAME_LIST.Add("b.GRADE");
            STUD_MAIN_FIELDNAME_LIST.Add("b.CLASS");
            STUD_MAIN_FIELDNAME_LIST.Add("b.C_NO");
            STUD_MAIN_FIELDNAME_LIST.Add("c.NAME G_NAME");
            STUD_MAIN_FIELDNAME_LIST.Add("c.RELATION G_RELATION");
            STUD_MAIN_FIELDNAME_LIST.Add("c.PROFESSION G_PROFESSION");
            STUD_MAIN_FIELDNAME_LIST.Add("c.AREA G_AREA");
            STUD_MAIN_FIELDNAME_LIST.Add("c.POSTAL_CODE G_POSTAL_CODE");
            STUD_MAIN_FIELDNAME_LIST.Add("c.ROAD G_ROAD");
            STUD_MAIN_FIELDNAME_LIST.Add("c.ADDRESS G_ADDRESS");
            STUD_MAIN_FIELDNAME_LIST.Add("c.TEL G_TEL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.GUARDMOBIL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.F_tel1");
            STUD_MAIN_FIELDNAME_LIST.Add("a.F_tel2");
            STUD_MAIN_FIELDNAME_LIST.Add("a.M_tel1");
            STUD_MAIN_FIELDNAME_LIST.Add("a.M_tel2");
            STUD_MAIN_FIELDNAME_LIST.Add("a.G_tel1");
            STUD_MAIN_FIELDNAME_LIST.Add("a.G_tel2");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Parent_sms");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Stud_sms");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Reg_in_date");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Reg_in_Class");
            STUD_MAIN_FIELDNAME_LIST.Add("a.St_status");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Leave_date");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Leave_Class");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Leave_reason");
            STUD_MAIN_FIELDNAME_LIST.Add("a.Religion");
            STUD_MAIN_FIELDNAME_LIST.Add("a.MBC_STUD");
            STUD_MAIN_FIELDNAME_LIST.Add("a.K_CLASS");
            STUD_MAIN_FIELDNAME_LIST.Add("a.K_SCHOOL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.K_EDU");
            STUD_MAIN_FIELDNAME_LIST.Add("a.P_CLASS");
            STUD_MAIN_FIELDNAME_LIST.Add("a.P_SCHOOL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.P_EDU");
            STUD_MAIN_FIELDNAME_LIST.Add("a.S_CLASS");
            STUD_MAIN_FIELDNAME_LIST.Add("a.S_SCHOOL");
            STUD_MAIN_FIELDNAME_LIST.Add("a.S_EDU");
            STUD_MAIN_FIELDNAME_LIST.Add("a.note");
            STUD_MAIN_FIELDNAME_LIST.Add("a.last_class");
            /////////////////////////////////////////////////////////////
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("NAME_C", 12);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("NAME_P", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("SEX", 1);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("B_DATE", 10);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("B_PLACE", 4);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("ID_TYPE", 5);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("ID_NO", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("I_PLACE", 5);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("I_DATE", 10);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("V_DATE", 10);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("S6_TYPE", 4);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("S6_IDATE", 10);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("S6_VDATE", 10);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("NATION", 4);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("ORIGIN", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("R_AREA", 1);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("RA_DESC", 40);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("AREA", 1);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("POST_CODE", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("ROAD", 50);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("ADDRESS", 50);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("TEL", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("FATHER", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("MOTHER", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("F_PROF", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("M_PROF", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("GUARD", 1);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("LIVE_SAME", 1);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("EC_NAME", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("EC_REL", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("EC_TEL", 32);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("EC_AREA", 1);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("EC_POSTAL_CODE", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("EC_ROAD", 50);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("EC_ADDRESS", 50);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("S_CODE", 3);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("GRADE", 3);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("CLASS", 2);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("C_NO", 4);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_NAME", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_RELATION", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_PROFESSION", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_AREA", 1);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_POSTAL_CODE", 20);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_ROAD", 50);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_ADDRESS", 100);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_TEL", 40);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("GUARDMOBIL", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("note", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("K_SCHOOL", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("P_SCHOOL", 60);
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("S_SCHOOL", 60);
            ///////////////////////////////////////////////////
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("B_PLACE", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("ID_TYPE", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("I_PLACE", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("S6_TYPE", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("NATION", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("R_AREA", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("AREA", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("EC_AREA", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("G_AREA", 32);
            STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB.Add("St_status", 16);
            //////////////////////////////////////////////////////
            STUD_MAIN_FIELDNAME_SIZE_HTB.Add("G_tel1", 40);
            /////////////////////////////////////////////////////
            AREA_HTB.Add("M", "M=澳門");
            AREA_HTB.Add("T", "T=氹仔");
            AREA_HTB.Add("C", "C=路環");
            AREA_HTB.Add("L", "L=內地");
            AREA_HTB.Add("O", "O=其他");
            AREA_List.Add("M=澳門"); AREA_List.Add("T=氹仔"); AREA_List.Add("C=路環"); AREA_List.Add("L=內地"); AREA_List.Add("O=其他");
            NIGHT_AREA_HTB.Add("M", "M=澳門");
            NIGHT_AREA_HTB.Add("C", "C=中國");
            NIGHT_AREA_HTB.Add("0", "O=其他");
            NIGHT_ARE_List.Add("M=澳門"); NIGHT_ARE_List.Add("C=中國"); NIGHT_ARE_List.Add("O=其他");
            RELA_HTB.Add("M", "M=母親");
            RELA_HTB.Add("F", "F=父親");
            RELA_HTB.Add("O", "O=其他");
            RELA_List.Add("M=母親"); RELA_List.Add("F=父親"); RELA_List.Add("O=其他");
            S6_HTB.Add("1", "1=永久S6");
            S6_HTB.Add("2", "2=有限期S6");
            S6_HTB.Add("3", "3=其他逗留許可");
            S6_List.Add("1=永久S6"); S6_List.Add("2=有限期S6"); S6_List.Add("3=其他逗留許可");
            ST_STATUS_HTB.Add("1", "1=新生");
            ST_STATUS_HTB.Add("2", "2=舊生");
            ST_STATUS_HTB.Add("3", "3=插班");
            ST_STATUS_HTB.Add("4", "4=留班");
            ST_STATUS_HTB.Add("5", "5=停學");
            ST_STATUS_HTB.Add("6", "6=退學");
            ST_STATUS_HTB.Add("7", "7=轉校");
            ST_STATUS_HTB.Add("8", "8=畢業");
            ST_STATUS_HTB.Add("9", "9=修業");
            ST_STATUS_List.Add("1=新生"); ST_STATUS_List.Add("2=舊生"); ST_STATUS_List.Add("3=插班"); ST_STATUS_List.Add("4=留班");
            ST_STATUS_List.Add("5=停學"); ST_STATUS_List.Add("6=退學"); ST_STATUS_List.Add("7=轉校"); ST_STATUS_List.Add("8=畢業");
            ST_STATUS_List.Add("9=修業");
            OdbcDataReader place_dr = new OdbcCommand("select PLACE,NAME_C from place ", _conn).ExecuteReader();
            while (place_dr.Read())
            {
                PLACE_HTB.Add(place_dr["PLACE"].ToString(), String.Format("{0,-3}={1}", place_dr["PLACE"].ToString(), place_dr["NAME_C"].ToString()));
                PLACE_List.Add(String.Format("{0,-3}={1}", place_dr["PLACE"].ToString(), place_dr["NAME_C"].ToString()));
            }
            OdbcDataReader idlist_dr = new OdbcCommand("select ID_TYPE,NAME_C from IDLIST ", _conn).ExecuteReader();
            while (idlist_dr.Read())
            {
                ID_HTB.Add(idlist_dr["ID_TYPE"].ToString().Trim(), String.Format("{0,-5}={1}", idlist_dr["ID_TYPE"], idlist_dr["NAME_C"]));
                ID_List.Add(String.Format("{0,-5}={1}", idlist_dr["ID_TYPE"], idlist_dr["NAME_C"]));
            }
            OdbcDataReader nation_dr = new OdbcCommand("select NATION,NAME_C from nation ", _conn).ExecuteReader();
            while (nation_dr.Read())
            {
                NATION_HTB.Add(nation_dr["NATION"].ToString(), String.Format("{0,-2}={1}", nation_dr["NATION"], nation_dr["NAME_C"]));
                NATION_List.Add(String.Format("{0,-2}={1}", nation_dr["NATION"], nation_dr["NAME_C"]));
            }
        }
        private static StudMain_Data_Schema instance = null;
        public static StudMain_Data_Schema GetInst
        {
            get
            {
                if (instance == null)
                    instance = new StudMain_Data_Schema();
                return instance;
            }
        }
        public List<String> STUD_MAIN_FIELDNAME_LIST = new List<string>();
        public Hashtable STUD_MAIN_FIELDNAME_SIZE_HTB = new Hashtable();
        public Hashtable STUD_MAIN_FIELDNAME_SIZE_DESCIRP_HTB = new Hashtable();
        public String GetYEAR()
        {
            string year = "";
            OdbcDataReader dr1 = new OdbcCommand("select Curr_Year from year", _conn).ExecuteReader();
            while (dr1.Read())
            {
                year = dr1[0].ToString();
            }
            return year;
        }
        public Hashtable GetHTBbyName(String FieldName)
        {
            return null;
        }
        public Hashtable AREA_HTB = new Hashtable(); public List<string> AREA_List = new List<string>();

        public Hashtable NIGHT_AREA_HTB = new Hashtable(); public List<string> NIGHT_ARE_List = new List<string>();

        public Hashtable RELA_HTB = new Hashtable(); public List<string> RELA_List = new List<string>();

        public Hashtable S6_HTB = new Hashtable(); public List<string> S6_List = new List<string>();

        public Hashtable PLACE_HTB = new Hashtable(); public List<string> PLACE_List = new List<string>();

        public Hashtable ID_HTB = new Hashtable(); public List<string> ID_List = new List<string>();

        public Hashtable NATION_HTB = new Hashtable(); public List<string> NATION_List = new List<string>();

        public Hashtable ST_STATUS_HTB = new Hashtable(); public List<string> ST_STATUS_List = new List<string>();

    }
}
