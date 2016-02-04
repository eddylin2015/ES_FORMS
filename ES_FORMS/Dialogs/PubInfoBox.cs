using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ES_FORMS
{
    public partial class PubInfoBox : Form
    {
        public RichTextBox memo;
        private Button btn;
        private Button prtBTN;
        private void btnClick(Object sender, EventArgs e)
        {
            Close();
        }
        private void prtbtnClick(Object sender, EventArgs e)
        {
            PubPrintMemo pt = new PubPrintMemo(memo);
            pt.ShowPageSetup();
            pt.ShowPrintDialog();
        }
        public PubInfoBox()
        {
            this.Size = new System.Drawing.Size(600, 800);
            this.Text = "Info";
            memo = new RichTextBox();
            memo.Multiline = true;
            memo.Dock = DockStyle.Fill;
            memo.Font = new System.Drawing.Font("�ө���_HKSCS", 10);
            prtBTN = new Button();
            prtBTN.Text = "���L";
            prtBTN.Dock = DockStyle.Bottom;
            prtBTN.Click += prtbtnClick;
            btn = new Button();
            btn.Dock = DockStyle.Bottom;
            btn.Text = "�h�X";
            btn.Click += btnClick;
            this.Controls.AddRange(new System.Windows.Forms.Control[] { memo, btn, prtBTN });
        }
        public PubInfoBox(Form parentForm)
            : this()
        {

            this.MdiParent = parentForm;

        }
    }
}