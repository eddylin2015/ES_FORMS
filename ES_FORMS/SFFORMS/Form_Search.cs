using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ES_FORMS.SFFORMS
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
        }
        protected OdbcConnection conn = null;
        protected Form _parentForm = null;
        void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_act();
            }
        }
        private string  format_btntext(OdbcDataReader dr) 
        {
          return  String.Format("{0}:{1}:{2}:{3}", dr["staf_ref"], dr["c_name"], dr["e_name"], dr["id_no"]);
        }
        private int AddRESBTN(String sql_t0, Form_Search_Res s_res_frm)
        {
            Font btnfont = new Font("新細明體", 12.0f);
            System.Drawing.Size btnsize = new System.Drawing.Size(300, 60);
            int res_count = 0;
            OdbcDataReader dr = new OdbcCommand(sql_t0, conn).ExecuteReader();
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
            dr.Close();
            dr.Dispose();
            return res_count++;
        }
        protected virtual void button_act()
        {
            String sql_t0 = "select staf_ref,c_name,e_name,id_no from sa_stafinfo ";
            Regex rgx_stafref = new Regex(@"^[0-9][0-9][0-9][0-9]-[a-zA-Z0-9][0-9][0-9]$");
            Regex rgx_dsejref = new Regex(@"^[0-9][0-9][0-9][0-9][0-9][0-9][0-9]-[a-zA-Z0-9]$");
            Regex rgx_ename = new Regex(@"[a-zA-Z]");
            Form_Search_Res s_res_frm = new Form_Search_Res();
            s_res_frm.MdiParent = _parentForm;
            int res_count = 0;
            if (textBox1.Text.Length > 1)
            {
                if (rgx_stafref.IsMatch(textBox1.Text))
                {
                    sql_t0 += "where staf_ref='"+textBox1.Text+"' limit 30;";
                }
                else if (rgx_dsejref.IsMatch(textBox1.Text))
                {
                    sql_t0 += "where dsej_ref='" + textBox1.Text + "' or dsej_sref='" + textBox1.Text + "' limit 30;";
                }
                else if (rgx_ename.IsMatch(textBox1.Text))
                {
                    sql_t0 += "where e_name like '%" + textBox1.Text + "%' limit 30;";
                }
                else
                {
                    sql_t0 += "where C_name like '%" + textBox1.Text + "%' limit 30;";
                }
                res_count+=AddRESBTN(sql_t0, s_res_frm);
            }
            if (textBox2.Text.Length > 0)
            {
                sql_t0 += "where id_no ='" + textBox2.Text + "' limit 30;";
                res_count += AddRESBTN(sql_t0, s_res_frm);
            }
            if (textBox3.Text.Length > 0)
            {
                sql_t0 += "where nlid_no ='" + textBox2.Text + "' limit 30;";
                res_count += AddRESBTN(sql_t0, s_res_frm);
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
            MessageBox.Show("請使用4.2");
        }
    }
    public class Form_Search_Stafinfo : Form_Search
    {
        public Form_Search_Stafinfo(OdbcConnection pconn, Form _pform) : base(pconn, _pform) { }
        public override void btn_Click(Object Sender, EventArgs e)
        {
            Control c = (Control)Sender;
            String staf_ref = c.Text.Split(':')[0];
            try
            {
                FormStafInfo fsi = new FormStafInfo(conn, staf_ref);
                fsi.Fill(staf_ref,"");
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