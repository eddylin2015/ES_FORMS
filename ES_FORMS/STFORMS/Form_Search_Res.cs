using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ES_FORMS.STFORMS
{
    public partial class Form_Search_Res : Form, ES_FORMS.Publib.Forms.iESUIFONT
    {
        public Form_Search_Res()
        {
            InitializeComponent();
        }




        #region iESUIFONT method

        public void SetUIFont(Font f)
        {
            foreach (Control c in this.flowLayoutPanel1.Controls)
            {
                if (c is Button)
                {
                    Button bt = (Button)c;
                    bt.Font = f;
                    
                }
            }
            
           // throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}