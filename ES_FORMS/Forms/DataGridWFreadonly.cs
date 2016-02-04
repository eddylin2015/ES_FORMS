using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.Odbc;
using ES_FORMS.Publib.DataGridViewUtils.Print;
using ES_FORMS.Publib.Forms;
namespace ES_FORMS.Publib.Forms
{
    public class DataGridWFreadonly : DataGridWF
    {
        public DataGridWFreadonly(iDataGridWF idg, Hashtable adict, BindingListOptions bloption):base(idg,adict,bloption)
        {
            this.tslUpdate.Visible = false;
            this.toolStripLabel2.Visible = false;
            this.tslImportXls.Visible = false;
        }
        public override void DataGridWF_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}
