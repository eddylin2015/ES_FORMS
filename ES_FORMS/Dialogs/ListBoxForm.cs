using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace ES_FORMS
{
    /// <summary>
    /// 列表表單
    /// </summary>
    public class ListBoxForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// CheckedListBox
        /// </summary>
        public System.Windows.Forms.ListBox lb = new System.Windows.Forms.ListBox();
        private System.Windows.Forms.Button SelAllbtn = new System.Windows.Forms.Button();

        protected System.Windows.Forms.Button OKbtn = new System.Windows.Forms.Button();
        private System.Windows.Forms.TableLayoutPanel tblp = new System.Windows.Forms.TableLayoutPanel();
        /// <summary>
        /// 構造
        /// </summary>
        public ListBoxForm()
        {
            OKbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            OKbtn.Text = "ok";
            SelAllbtn.Text = "Select All";
            SelAllbtn.Click += selall_click;
            lb.Dock = System.Windows.Forms.DockStyle.Fill;
            lb.Width = 230;
            tblp.ColumnCount = 2;
            tblp.RowCount = 2;
            tblp.Controls.Add(lb);
            tblp.SetRowSpan(lb, 2);
            tblp.Controls.Add(OKbtn);
            tblp.Controls.Add(SelAllbtn);
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblp.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(tblp);
        }
        private void selall_click(Object sender, EventArgs e)
        {
           // for (int i = 0; i < lb.Items.Count; i++)
           //     lb.SetItemChecked(i, true);
        }
    }
    public class ListBoxFilterForm : System.Windows.Forms.Form
    {
        public System.Windows.Forms.ListBox lb = new System.Windows.Forms.ListBox();
        private System.Windows.Forms.Button SelAllbtn = new System.Windows.Forms.Button();
        public System.Windows.Forms.Button OKbtn = new System.Windows.Forms.Button();
        public System.Windows.Forms.TextBox filterTB = new TextBox();
        private System.Windows.Forms.TableLayoutPanel tblp = new System.Windows.Forms.TableLayoutPanel();
        private List<String> items_bk = null;
        public ListBoxFilterForm()
        {
            OKbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            OKbtn.Text = "ok";
            SelAllbtn.Text = "Select All";
            SelAllbtn.Click += selall_click;
            filterTB.TextChanged += new EventHandler(filterTB_TextChanged);
            lb.Dock = System.Windows.Forms.DockStyle.Fill;
            lb.Width = 230;
            tblp.ColumnCount = 2;
            tblp.RowCount = 3;
            tblp.Controls.Add(lb);
            tblp.SetRowSpan(lb, 3);
            tblp.Controls.Add(OKbtn);
            tblp.Controls.Add(SelAllbtn);
            tblp.Controls.Add(filterTB);
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblp.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(tblp);
        }
        void filterTB_TextChanged(object sender, EventArgs e)
        {
            if (items_bk == null)
            {
                items_bk = new List<string>();
                for (int i = 0; i < lb.Items.Count; i++)
                {
                    items_bk.Add(lb.Items[i].ToString());
                }
            }
            lb.Items.Clear();
            for (int i = 0; i < items_bk.Count; i++)
            {
                if (items_bk[i].ToLower().Contains(filterTB.Text.ToLower()))
                {
                        lb.Items.Add(items_bk[i]);
                }
            }
        }
        private void selall_click(Object sender, EventArgs e)
        {
        }
    }
    /// <summary>
    /// 列表表單
    /// </summary>
    public class CheckListBoxForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// CheckedListBox
        /// </summary>
        public System.Windows.Forms.CheckedListBox lb = new System.Windows.Forms.CheckedListBox();
        private System.Windows.Forms.Button SelAllbtn = new System.Windows.Forms.Button();
        public System.Windows.Forms.Button OKbtn = new System.Windows.Forms.Button();
        private System.Windows.Forms.TableLayoutPanel tblp = new System.Windows.Forms.TableLayoutPanel();
        /// <summary>
        /// 構造
        /// </summary>
        public CheckListBoxForm()
        {
            OKbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            OKbtn.Text = "ok";
            SelAllbtn.Text = "Select All";
            SelAllbtn.Click += selall_click;
            lb.Dock = System.Windows.Forms.DockStyle.Fill;
            lb.Width = 230;
            tblp.ColumnCount = 2;
            tblp.RowCount = 2;
            tblp.Controls.Add(lb);
            tblp.SetRowSpan(lb, 2);
            tblp.Controls.Add(OKbtn);
            tblp.Controls.Add(SelAllbtn);
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblp.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(tblp);
        }
        private void selall_click(Object sender, EventArgs e)
        {
            for (int i = 0; i < lb.Items.Count; i++)
                lb.SetItemChecked(i, true);
        }
    }
    /// <summary>
    /// 列表表單
    /// </summary>
    public class CheckListBoxFilterForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// CheckedListBox
        /// </summary>
        public System.Windows.Forms.CheckedListBox lb = new System.Windows.Forms.CheckedListBox();
        private System.Windows.Forms.Button SelAllbtn = new System.Windows.Forms.Button();
        public System.Windows.Forms.Button OKbtn = new System.Windows.Forms.Button();
        public System.Windows.Forms.TextBox filterTB = new TextBox();
        private System.Windows.Forms.TableLayoutPanel tblp = new System.Windows.Forms.TableLayoutPanel();
        private List<String> items_bk =null;
        private List<Boolean> items_c_bk = null;
        /// <summary>
        /// 構造
        /// </summary>
        public CheckListBoxFilterForm()
        {
            OKbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            OKbtn.Text = "ok";
            SelAllbtn.Text = "Select All";
            SelAllbtn.Click += selall_click;
            filterTB.TextChanged += new EventHandler(filterTB_TextChanged);
            lb.Dock = System.Windows.Forms.DockStyle.Fill;
            lb.Width = 230;
            tblp.ColumnCount = 2;
            tblp.RowCount = 3;
            tblp.Controls.Add(lb);
            tblp.SetRowSpan(lb, 3);
            tblp.Controls.Add(OKbtn);
            tblp.Controls.Add(SelAllbtn);
            tblp.Controls.Add(filterTB);
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblp.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(tblp);
        }
        
        void filterTB_TextChanged(object sender, EventArgs e)
        {
            if (items_bk == null)
            {
                items_bk = new List<string>();
                items_c_bk = new List<bool>();
                for (int i = 0; i < lb.Items.Count; i++)
                {
                    items_bk.Add(lb.Items[i].ToString());
                    items_c_bk.Add(lb.GetItemChecked(i));
                }
            }
            for (int i = 0; i < lb.Items.Count; i++)
            {
                for (int j = 0; j < items_bk.Count; j++)
                {
                    if (lb.Items[i].ToString() == items_bk[j])
                    {
                        items_c_bk[j] = lb.GetItemChecked(i);
                        break;
                    }
                }
            }
            lb.Items.Clear();
            for (int i = 0; i < items_bk.Count; i++)
            {
                if (items_bk[i].ToLower().Contains(filterTB.Text.ToLower()))
                {
                    if (items_c_bk[i])
                    {
                        lb.Items.Add(items_bk[i],true);
                    }
                    else
                    {
                        lb.Items.Add(items_bk[i]);
                    }
                }
            }
        }
        private void selall_click(Object sender, EventArgs e)
        {
            for (int i = 0; i < lb.Items.Count; i++)
                lb.SetItemChecked(i, true);
        }

    }

}
