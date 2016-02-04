namespace ES_FORMS.Publib.Forms
{
    partial class DataGridWF
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridWF));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslUpdate = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslExportXls = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tslImportXls = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelAddNew = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.凍結左邊欄位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消凍結ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字體ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFontToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.outXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HideFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(787, 497);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.toolStripSeparator3,
            this.tslUpdate,
            this.toolStripSeparator2,
            this.tslExportXls,
            this.toolStripSeparator4,
            this.tslImportXls,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripLabelAddNew});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 24);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(787, 25);
            this.bindingNavigator1.TabIndex = 2;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新添";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(34, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 22);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tslUpdate
            // 
            this.tslUpdate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tslUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslUpdate.Font = new System.Drawing.Font("MingLiU", 9F);
            this.tslUpdate.ImageTransparentColor = System.Drawing.Color.White;
            this.tslUpdate.Name = "tslUpdate";
            this.tslUpdate.Size = new System.Drawing.Size(41, 22);
            this.tslUpdate.Text = "[儲存]";
            this.tslUpdate.Click += new System.EventHandler(this.tslUpdate_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslExportXls
            // 
            this.tslExportXls.Name = "tslExportXls";
            this.tslExportXls.Size = new System.Drawing.Size(58, 22);
            this.tslExportXls.Text = "[匯出XLS]";
            this.tslExportXls.Click += new System.EventHandler(this.tslExportXls_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tslImportXls
            // 
            this.tslImportXls.Name = "tslImportXls";
            this.tslImportXls.Size = new System.Drawing.Size(58, 22);
            this.tslImportXls.Text = "[匯入XLS]";
            this.tslImportXls.Click += new System.EventHandler(this.tslImportXls_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel2.Text = "重載資料";
            this.toolStripLabel2.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // toolStripLabelAddNew
            // 
            this.toolStripLabelAddNew.Name = "toolStripLabelAddNew";
            this.toolStripLabelAddNew.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabelAddNew.Text = "新增几筆";
            this.toolStripLabelAddNew.Click += new System.EventHandler(this.toolStripLabelAddNew_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(787, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.凍結左邊欄位ToolStripMenuItem,
            this.取消凍結ToolStripMenuItem,
            this.字體ToolStripMenuItem,
            this.outXMLToolStripMenuItem,
            this.inXMLToolStripMenuItem,
            this.HideFieldsToolStripMenuItem,
            this.PrintToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.noFilterToolStripMenuItem});
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.functionsToolStripMenuItem.Text = "功能";
            // 
            // 凍結左邊欄位ToolStripMenuItem
            // 
            this.凍結左邊欄位ToolStripMenuItem.Name = "凍結左邊欄位ToolStripMenuItem";
            this.凍結左邊欄位ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.凍結左邊欄位ToolStripMenuItem.Text = "凍結左邊欄位";
            this.凍結左邊欄位ToolStripMenuItem.Click += new System.EventHandler(this.凍結左邊欄位ToolStripMenuItem_Click);
            // 
            // 取消凍結ToolStripMenuItem
            // 
            this.取消凍結ToolStripMenuItem.Name = "取消凍結ToolStripMenuItem";
            this.取消凍結ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.取消凍結ToolStripMenuItem.Text = "取消凍結";
            this.取消凍結ToolStripMenuItem.Click += new System.EventHandler(this.取消凍結ToolStripMenuItem_Click);
            // 
            // 字體ToolStripMenuItem
            // 
            this.字體ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemFontToolStripMenuItem,
            this.setFontToolStripMenuItem1});
            this.字體ToolStripMenuItem.Name = "字體ToolStripMenuItem";
            this.字體ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.字體ToolStripMenuItem.Text = "字體";
            // 
            // systemFontToolStripMenuItem
            // 
            this.systemFontToolStripMenuItem.Name = "systemFontToolStripMenuItem";
            this.systemFontToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.systemFontToolStripMenuItem.Text = "System Font";
            this.systemFontToolStripMenuItem.Click += new System.EventHandler(this.systemFontToolStripMenuItem_Click);
            // 
            // setFontToolStripMenuItem1
            // 
            this.setFontToolStripMenuItem1.Name = "setFontToolStripMenuItem1";
            this.setFontToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.setFontToolStripMenuItem1.Text = "Set Font";
            this.setFontToolStripMenuItem1.Click += new System.EventHandler(this.setFontToolStripMenuItem1_Click);
            // 
            // outXMLToolStripMenuItem
            // 
            this.outXMLToolStripMenuItem.Name = "outXMLToolStripMenuItem";
            this.outXMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.outXMLToolStripMenuItem.Text = "out XML";
            // 
            // inXMLToolStripMenuItem
            // 
            this.inXMLToolStripMenuItem.Name = "inXMLToolStripMenuItem";
            this.inXMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.inXMLToolStripMenuItem.Text = "in XML";
            // 
            // HideFieldsToolStripMenuItem
            // 
            this.HideFieldsToolStripMenuItem.Name = "HideFieldsToolStripMenuItem";
            this.HideFieldsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.HideFieldsToolStripMenuItem.Text = "隐藏欄位";
            // 
            // PrintToolStripMenuItem
            // 
            this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            this.PrintToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PrintToolStripMenuItem.Text = "打印";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // noFilterToolStripMenuItem
            // 
            this.noFilterToolStripMenuItem.Name = "noFilterToolStripMenuItem";
            this.noFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.noFilterToolStripMenuItem.Text = "No Filter";
            // 
            // DataGridWF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 549);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridView1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DataGridWF";
            this.Text = "DataGridWF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataGridWF_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel tslExportXls;
        protected System.Windows.Forms.ToolStripLabel tslUpdate;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 凍結左邊欄位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消凍結ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem 字體ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFontToolStripMenuItem1;
        protected System.Windows.Forms.BindingNavigator bindingNavigator1;
        protected System.Windows.Forms.ToolStripLabel tslImportXls;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem outXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HideFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintToolStripMenuItem;
        public System.Windows.Forms.ToolStripLabel toolStripLabelAddNew;
        public System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noFilterToolStripMenuItem;

    }
}