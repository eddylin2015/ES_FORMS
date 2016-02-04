using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ES_FORMS.Publib.Forms
{
    public partial class InputSQLWF : Form
    {
        public InputSQLWF(Form Sender,OdbcConnection conn):base()
        {
            InitializeComponent();
            this.MdiParent = Sender;
            _conn = conn;
        }
        private OdbcConnection _conn = null;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                iDataGridWF idg = new iDataGridWF(richTextBox1.Text, "TempTable",_conn);
                DataGridWF dg = new DataGridWF(idg, null);
                dg.ShowMDIchild(this.MdiParent);
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message+ '\n' + er.Source);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(richTextBox1.Text,_conn);
                int n=cmd.ExecuteNonQuery();
                MessageBox.Show("§ï°Ê" + n.ToString() + "µ§°O¿ý");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + '\n' + er.Source);
            }

        }
    }
}