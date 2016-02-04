using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ES_FORMS.Dialogs
{
    partial class ListItemSelWF : Form
    {
     public ListItemSelWF()
        {
            InitializeComponent();
            tableLayoutPanel1.SetRowSpan(checkedListBox1, 3);
            tableLayoutPanel1.SetColumnSpan(checkedListBox1, 2);
            tableLayoutPanel1.SetRowSpan(checkedListBox2, 4);
        }
        public void addFromList(List<string> li)
        {
            checkedListBox1.Items.Clear();
            foreach (string s in li)
            {
                checkedListBox1.Items.Add(s);
            }
        }
        public List<String> getList()
        {
            List<string> li = new List<string>();
            for (int i = 0; i < checkedListBox2.CheckedItems.Count; i++)
            {
                li.Add(checkedListBox2.CheckedItems[i].ToString());
            }
            return li;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //checkedListBox2.Items.Clear();
            for (int i = 0; i < checkedListBox1.CheckedItems.Count;i++ )
            {
                string[] arrstr=checkedListBox1.CheckedItems[i].ToString().Split(';');
                checkedListBox2.Items.Add(arrstr[0],true);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
        private void checkedListBox1_Click(object sender, EventArgs e)
        {
        }
        private void checkedListBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==32)
            {
             //   MessageBox.Show(checkedListBox1.SelectedIndex.ToString());
            }
        }

        private void btPickToLeft_Click(object sender, EventArgs e)
        {

        }
    }
    partial class ListItemSelWF
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.btPickToRight = new System.Windows.Forms.Button();
            this.btSubmit = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btPickToLeft = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.Font = new System.Drawing.Font("MingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 29);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(149, 305);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.Click += new System.EventHandler(this.checkedListBox1_Click);
            this.checkedListBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkedListBox1_KeyPress);
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(429, 29);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(345, 305);
            this.checkedListBox2.TabIndex = 1;
            // 
            // btPickToRight
            // 
            this.btPickToRight.Location = new System.Drawing.Point(352, 29);
            this.btPickToRight.Name = "btPickToRight";
            this.btPickToRight.Size = new System.Drawing.Size(60, 23);
            this.btPickToRight.TabIndex = 2;
            this.btPickToRight.Text = ">>";
            this.btPickToRight.UseVisualStyleBackColor = true;
            this.btPickToRight.Click += new System.EventHandler(this.button1_Click);
            // 
            // btSubmit
            // 
            this.btSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSubmit.Location = new System.Drawing.Point(352, 509);
            this.btSubmit.Name = "btSubmit";
            this.btSubmit.Size = new System.Drawing.Size(60, 23);
            this.btSubmit.TabIndex = 3;
            this.btSubmit.Text = "submit";
            this.btSubmit.UseVisualStyleBackColor = true;
            this.btSubmit.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 20);
            this.button3.TabIndex = 4;
            this.button3.Text = "select All";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(158, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 20);
            this.button4.TabIndex = 5;
            this.button4.Text = "clearSelected";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btPickToLeft
            // 
            this.btPickToLeft.Location = new System.Drawing.Point(352, 340);
            this.btPickToLeft.Name = "btPickToLeft";
            this.btPickToLeft.Size = new System.Drawing.Size(60, 23);
            this.btPickToLeft.TabIndex = 6;
            this.btPickToLeft.Text = "<<";
            this.btPickToLeft.UseVisualStyleBackColor = true;
            this.btPickToLeft.Click += new System.EventHandler(this.btPickToLeft_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.btSubmit, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btPickToLeft, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btPickToRight, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox2, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.937008F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.06299F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 676);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // ListItemSelWF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 676);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ListItemSelWF";
            this.Text = "ListItemSelWF";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.Button btPickToRight;
        private System.Windows.Forms.Button btSubmit;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btPickToLeft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
