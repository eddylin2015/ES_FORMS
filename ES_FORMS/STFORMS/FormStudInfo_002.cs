using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

namespace ES_FORMS.STFORMS
{
    public partial class FormStudInfo_002 : Form
    {
        FormStud_Act_Interface act = null;
        OdbcConnection conn = null;
        public FormStudInfo_002()
        {
            InitializeComponent();
            this.FormClosing += FormStudInfo_002_FormClosing;
            this.button1.Click += this.button1_Click;
            this.button2.Click += this.button2_Click;
            this.button3.Click += this.button3_Click;
        }
        public FormStudInfo_002(String pStudRef,String oldyear,OdbcConnection pconn):this()
        {
            conn=pconn;
            string sql = string.Format("select a.stud_ref from studmain a left join studtran b on a.stud_ref=b.stud_ref where a.stud_ref='{0}' and b.year='{1}' ;", pStudRef, Get_Year());
            OdbcDataReader dr = new OdbcCommand(sql, conn).ExecuteReader();
            if (dr.Read())
            {
                act = new FormStudInfo_act00(this, pStudRef, errorProvider1,conn);
            }
            else
            {
                MessageBox.Show("非本學年資料.唯讀!");
                act = new FormStudInfo_act00_readonly(this, pStudRef, errorProvider1, oldyear,conn);
            }
            act.txtboxReadOnly();
        }
        public void SetUIFont(System.Drawing.Font f)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = (TextBox)c;
                    c.Font = f;
                }
            }
            return;
        }
        private void FormStudInfo_002_FormClosing(object sender, FormClosingEventArgs e)
        {
            act.log();
            if (act.change_cnt == 0) { }

            else if (act.change_cnt > 0 && MessageBox.Show("儲存並離開?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                act.Save();
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            act.Save();
            MessageBox.Show(String.Format("修改{0}次資料!", act.change_cnt));
            act.change_cnt = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (act.change_cnt > 0 && MessageBox.Show("是否放棄修改恢復返原始狀態?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                act.fill_control_text();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            act.txtboxEditMode();
        }
        private String Get_Year()
        {
            string rYear = null;
            string year_sql = "select curr_year from year;";
            OdbcDataReader year_dr = new OdbcCommand(year_sql, conn).ExecuteReader();
            while (year_dr.Read())
            {
                rYear = year_dr[0].ToString();
            }
            return rYear;
        }
    }
}
