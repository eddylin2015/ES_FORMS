using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ES_FORMS
{
    public partial class UserPwdForm : Form
    {
        public UserPwdForm()
        {
            InitializeComponent();
        }
        private int userrole;
        public int getUserRole()
        {
            return userrole;
        }
        public virtual int CheckLoginUserPWD(string user, string pwd, string domain)
        {
            return -1;
        }
        private void loginPorc()
        {
            int r = CheckLoginUserPWD(textBox1.Text, maskedTextBox1.Text, comboBox1.Text);
            if (r > 0)
            {
                userrole = r;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (r == 0)//(int)RoleDefs.err_pass
            {
                MessageBox.Show(Publib.G_ERROR.LoginPassword);
            }
            else if (r == -1) { MessageBox.Show(Publib.G_ERROR.LoginUser); }// (int)RoleDefs.err_user
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {

                errorProvider1.SetError(this.textBox1, " username is a required field");
            }
            else
            {
                errorProvider1.SetError(this.textBox1, null);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                maskedTextBox1.Focus();
            }
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginPorc();
            }
        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                errorProvider1.SetError(maskedTextBox1, " password is a required field");
            }
            else
            {
                errorProvider1.SetError(maskedTextBox1, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginPorc();
        }

    }
}