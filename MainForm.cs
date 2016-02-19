using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studmain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string cnst5_1DriverConnStrFormat = "Driver={{MySQL ODBC 5.1 Driver}};Server={0};Database={1};UID={2};PWD={3};OPTION=67108867";//optoin=3
            String _conn_txt = null;
            _conn_txt = string.Format(cnst5_1DriverConnStrFormat, "127.0.0.1", "db", "u", "p");
            conn = new OdbcConnection(_conn_txt);
            conn.Open();
            ES_FORMS.STFORMS.Form_Search fs = new ES_FORMS.STFORMS.Form_Search_Studinfo(conn, this);
            fs.MdiParent = this;
            fs.Show();
        }
        private OdbcConnection conn = null;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
            conn.Dispose();
        }
    }
}

