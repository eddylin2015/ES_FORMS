using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ES_FORMS.STFORMS
{
    public partial class Form_Search : Form
    {
       
        public Form_Search(OdbcConnection pconn,Form _pform)
        {
            InitializeComponent();
            conn = pconn;
            _parentForm = _pform;
            this.MdiParent = _parentForm;
            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            textBox2.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            textBox3.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            textBox4.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            textBox5.KeyDown += new KeyEventHandler(textBox1_KeyDown);
        }
        protected OdbcConnection conn = null;
        protected Form _parentForm = null;
        void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_act();
             //   throw new Exception("The method or operation is not implemented.");

            }
        }

   
        private string  format_btntext(OdbcDataReader dr) 
        {
          return  String.Format("{0,-8}:{6}:{1,-7}{2,-8} {3}{4}{5}", dr["stud_ref"], dr["code"], dr["name_c"], dr["grade"], dr["class"],dr["c_no"],dr["year"]);
        }
        protected virtual void button_act()
        {
            Form_Search_Res s_res_frm = new Form_Search_Res();
            s_res_frm.MdiParent = _parentForm;
            Font btnfont = new Font("新細明體", 12.0f);
            System.Drawing.Size btnsize = new System.Drawing.Size(300, 60);
            String YEAR = "";
            string year_sql = "select curr_year from year;";
            int res_count = 0;
            OdbcDataReader year_dr = new OdbcCommand(year_sql, conn).ExecuteReader();
            while (year_dr.Read())
            {
                YEAR = year_dr[0].ToString();
            }


            if (textBox1.Text.Length > 2)
            {
                String sql_t0 = "select a.stud_ref,a.code,a.name_c,b.grade,b.class,b.c_no,b.year from studmain a left join studtran b on a.stud_ref=b.stud_ref where a.stud_ref like '{0}' ;";
                String sql0 = String.Format(sql_t0, textBox1.Text.ToUpper());
                OdbcDataReader dr = new OdbcCommand(sql0, conn).ExecuteReader();
                
                while (dr.Read())
                {
                    string btntxt = format_btntext(dr);
                    Button btn = new Button();
                    btn.Text = btntxt;
                    btn.Size = btnsize;
                    btn.Font = btnfont   ;
                    btn.Click += btn_Click;
                    s_res_frm.flowLayoutPanel1.Controls.Add(btn);
                    res_count++;
                }
            }
            if (textBox2.Text.Length > 0)
            {
                String sql_t0 = "select a.stud_ref,a.code,a.name_c,b.grade,b.class,b.c_no,b.year from studmain a left join studtran b on a.stud_ref=b.stud_ref where a.code like '{0}' ;";
                String sql0 = String.Format(sql_t0, textBox2.Text.ToUpper());
                OdbcDataReader dr = new OdbcCommand(sql0, conn).ExecuteReader();
                while (dr.Read())
                {
                    string btntxt = format_btntext(dr);
                    Button btn = new Button();
                    btn.Text = btntxt;
                    btn.Size = btnsize;
                    btn.Font = btnfont;
                    btn.Click += btn_Click;
                    s_res_frm.flowLayoutPanel1.Controls.Add(btn);
                    res_count++;
                }
            }
            if (textBox3.Text.Length > 0)
            {
                String sql_t0 = "select a.stud_ref,a.code,a.name_c,b.grade,b.class,b.c_no,b.year from studmain a left join studtran b on a.stud_ref=b.stud_ref where a.name_c like '{0}' ;";
                String sql0 = String.Format(sql_t0, textBox3.Text);
                OdbcDataReader dr = new OdbcCommand(sql0, conn).ExecuteReader();
                while (dr.Read())
                {
                    string btntxt = format_btntext(dr);
                    Button btn = new Button();
                    btn.Text = btntxt;
                    btn.Size = btnsize;
                    btn.Font = btnfont;
                    btn.Click += btn_Click;
                    s_res_frm.flowLayoutPanel1.Controls.Add(btn);
                    res_count++;
                }
            }
            if (textBox4.Text.Length > 0)
            {
                String sql_t0 = "select a.stud_ref,a.code,a.name_c,b.grade,b.class,b.c_no,b.year from studmain a left join studtran b on a.stud_ref=b.stud_ref where a.id_no like '{0}' ;";
                String sql0 = String.Format(sql_t0, textBox4.Text);
                OdbcDataReader dr = new OdbcCommand(sql0, conn).ExecuteReader();
                while (dr.Read())
                {
                    string btntxt = format_btntext(dr);
                    Button btn = new Button();
                    btn.Text = btntxt;
                    //btn.AutoSize = true;
                    btn.Size = btnsize;
                    btn.Font = btnfont;
                    btn.Click += btn_Click;
                    s_res_frm.flowLayoutPanel1.Controls.Add(btn);
                    res_count++;
                }
            }


            if (textBox5.Text.Length > 2)
            {
                String sql_t1 = "select a.stud_ref,a.code,a.name_c,b.grade,b.class,b.c_no,b.year from studmain a left join studtran b on a.stud_ref=b.stud_ref where b.grade='{0}' and b.class='{1}' and b.year='{2}' order by b.grade,b.class,b.c_no ;";
                String grade = textBox5.Text.Substring(0, textBox5.Text.Length - 1).ToUpper();
                String classno = textBox5.Text[textBox5.Text.Length - 1].ToString().ToUpper();
                String sql1 = String.Format(sql_t1, grade, classno, YEAR);
                OdbcDataReader dr = new OdbcCommand(sql1, conn).ExecuteReader();
                while (dr.Read())
                {
                    string btntxt = format_btntext(dr);
                    Button btn = new Button();
                    btn.Text = btntxt;
                    btn.Size = btnsize;
                    btn.Font = btnfont;
                    btn.Click += btn_Click;
                    s_res_frm.flowLayoutPanel1.Controls.Add(btn);
                    res_count++;
                }
            }
            if (res_count > 0)
            {
                s_res_frm.Show();
            }
            else { s_res_frm.Dispose(); }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            button_act();
        }


        public virtual void btn_Click(Object Sender, EventArgs e)
        {
            /*
            Control c = (Control)Sender;
            String stud_ref = c.Text.Split(':')[0];
            try
            {
                FormStudInfo fsi = new FormStudInfo(stud_ref,null);
                fsi.MdiParent = es_lib.Publib.Pub.cfg.curr_userinfo.MainForm;
                fsi.Show();
            }
            catch (Exception excep)
            {
                MessageBox.Show("重啟系統!\n" + excep.Message);

            }*/
            MessageBox.Show("請使用4.2");
        }




    }
    
    public class Form_Search_Studinfo : Form_Search
    {
        public Form_Search_Studinfo(OdbcConnection pconn, Form _pform) : base(pconn, _pform) { }
        public override void btn_Click(Object Sender, EventArgs e)
        {
            Control c = (Control)Sender;
            String stud_ref = c.Text.Split(':')[0];
            try
            {
                StudMain_Data_Schema.conn = conn;
                FormStudInfo_002 fsi = new FormStudInfo_002(stud_ref, null, conn);
                fsi.MdiParent = _parentForm;
                fsi.Show();
            }
            catch (Exception excep)
            {
                MessageBox.Show("重啟系統!\n" + excep.Message);
            }
        }
    }
}