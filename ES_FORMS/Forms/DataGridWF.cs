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
using ES_FORMS.office;
using ES_FORMS.Dialogs;
/*DataGridWindowForm 提供簡化數據表格檢示功能． 
 * 關聯外部es_dblib.esdb.GetInstance("").GetConn();
 *         es_lib.Publib.DataGridViewUtils.Print;
 * 
 * eg. iDataGridWF dg=new iDataGridWF(SQLstr,TableNameStr);
 *     DataGridWF dgWF=new DataGridWF(dg,null);
 *     dgWF.ShowDialog();
 * */
namespace ES_FORMS.Publib.Forms
{
    
    public partial class DataGridWF : Form, iESUIFONT
    {
        Hashtable dict = null;
        iDataGridWF idg;
        public  BindingSource customersBindingSource = new BindingSource();
        public string DefaultFileName = null;
        private bool InitializeFlag = false;
        public DataGridWF(iDataGridWF idg, Hashtable adict, bool pInitializeFlag, BindingListOptions bloption)
        {
            InitializeComponent();
            this.idg = idg;
            this.dict = adict;
            switch (bloption)
            {
                case BindingListOptions.AllowNewNo:
                    this.customersBindingSource.AllowNew = false;
                    break;
                case BindingListOptions.AllowModifyNo:
                    this.customersBindingSource.AllowNew = false;
                    this.dataGridView1.ReadOnly = true;
                    break;
                case BindingListOptions.AllowNewNoAndNoPromptReload:
                    this.customersBindingSource.AllowNew = false;
                    break;
                case BindingListOptions.none:
                    break;
            }
            this.InitializeFlag = pInitializeFlag;
            if (InitializeFlag)
            {
                Initialize();
            }
        }
        public DataGridWF(iDataGridWF idg, Hashtable adict)
            : this(idg, adict, BindingListOptions.none)
        {
        }
        public DataGridWF(iDataGridWF idg, Hashtable adict, BindingListOptions bloption)
            : this(idg, adict, true, bloption)
        {
        }
        public DataGridWF(iDataGridWF idg, Hashtable adict, bool pInitializeFlag)
            : this(idg, adict, pInitializeFlag, BindingListOptions.none)
        {
        }
        public void InitializeAfterConstructor()
        {
            if (!InitializeFlag)
            {
                Initialize();
            }
        }
        public void AlignNum()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                if (dataGridView1.Columns[i].ValueType.ToString() == "System.Int32"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Int16"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Decimal"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Single"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Double")
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

        }
        private void Initialize()
        {
            InitializeFlag = true;
            if(idg.Columns !=null)
                dataGridView1.AutoGenerateColumns = false;
            this.bindingNavigator1.BindingSource = this.customersBindingSource;
            this.customersBindingSource.DataSource = idg.dt;
            this.dataGridView1.DataSource = this.customersBindingSource;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                if (dataGridView1.Columns[i].ValueType.ToString() == "System.Int32"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Int16"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Decimal"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Single"
                    || dataGridView1.Columns[i].ValueType.ToString() == "System.Double")
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            if (this.dataGridView1.AutoGenerateColumns == true)
            {
            }
            else if(dataGridView1.AutoGenerateColumns==false)
            {
                for (int i=0; i<idg.Columns.Count;i++)
                {
                    String fieldname=idg.Columns[i];
                    DataGridViewColumn tb = idg.ColumnsType[i];
                    tb.DataPropertyName = fieldname;
                    tb.Name = fieldname;
                    tb.HeaderText = fieldname;
                    if(!dataGridView1.Columns.Contains(tb))
                    dataGridView1.Columns.Add(tb);                   
                }
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                if (idg.readonly_fieldnames != null && idg.readonly_fieldnames.Contains(dataGridView1.Columns[i].HeaderCell.Value.ToString().ToUpper()))
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if ( dict != null && dict.Contains(dataGridView1.Columns[i].Name.ToUpper()))
                {
                    dataGridView1.Columns[i].HeaderCell.Value = dict[dataGridView1.Columns[i].Name.ToUpper()];
                }               
            }
            PrintToolStripMenuItem.Click += toolSLPrint_Click;
            outXMLToolStripMenuItem.Click += tSLwriteXml_Click;
            inXMLToolStripMenuItem.Click += tSLreadXml_Click;
            HideFieldsToolStripMenuItem.Click += tslHideFields_Click;
            noFilterToolStripMenuItem.Click += noFilterClick;
        }
        private void noFilterClick(Object Sender, EventArgs e)
        {
            customersBindingSource.Filter = "";
        }
        public virtual void AddNewBlankRec()
        {
        }
        public void FrozenLeftColumns(int cnt,bool ReadOnly)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (i < cnt)
                {
                    dataGridView1.Columns[i].Frozen = true;
                    dataGridView1.Columns[i].ReadOnly = ReadOnly;
                }
                else
                {
                    break;
                }
            }
        }
        public void CancelForenColumns()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                    dataGridView1.Columns[i].Frozen = false;
                    dataGridView1.Columns[i].ReadOnly = false;
            }
        }
        private void tslImportXls_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.FileName = "*.XLS";
            if (of.ShowDialog() == DialogResult.OK)
            {
                idg.ImportXls(of.FileName, dict);
            }
        }
        private void tslExportXls_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "*.xls";
            if (DefaultFileName != null) sf.FileName = DefaultFileName;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                  idg.Exportxls(sf.FileName, dict);
                  if (MessageBox.Show("是否打開文件" + sf.FileName, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                  {
                      ShellExec.ShellExecute(IntPtr.Zero, new StringBuilder("Open"), new StringBuilder(sf.FileName), new StringBuilder(""), new StringBuilder(""), 1);
                  }
            }
        }
        private void tslUpdate_Click(object sender, EventArgs e)
        {
            if (!this.dataGridView1.EndEdit())
            {
                MessageBox.Show("Editing Last Cell..!");
            }
            CurrencyManager fcm;
            fcm = (CurrencyManager)this.BindingContext[this.dataGridView1.DataSource];
            fcm.EndCurrentEdit();
            // int i=adapter.Update(ds);          
            int i = idg.submitUpdate();
            MessageBox.Show("更新了" + i.ToString() + "筆資料");
        }
        private void tslHideFields_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("請先儲存現在資料,可能導至現在編號料資遺失?是 先儲存下載伺服器資料;否放棄操作.", "資料重新下載", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                this.tslUpdate_Click(null, null);
                idg.reload_DBAdapter();

                ListItemSelWF lswf = new ListItemSelWF();
                List<string> li = new List<string>();
                for (int i = 0; i < this.idg.dt.Columns.Count; i++)
                {
                    string temp=String.Format("{0,-32}{1}", idg.dt.Columns[i].Caption+";" , dict[idg.dt.Columns[i].Caption]);
                    li.Add(temp);
                }
                lswf.addFromList(li);
                if (lswf.ShowDialog() == DialogResult.OK)
                {
                    idg.getDataWithSelColumn(lswf.getList());
                }
                Initialize();
            }
        }
        private void toolSLPageSetup_Click(object sender, EventArgs e)
        {
        }
        private void toolSLPrint_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(this.dataGridView1);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        public void ShowMDIchild(Form Sender)
        {
            this.MdiParent = Sender;
            this.Show();
        }
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
           DialogResult res= MessageBox.Show("請先儲存現在資料,可能導至現在編號料資遺失?是 先儲存下載伺服器資料;否放棄操作.","資料重新下載",MessageBoxButtons.YesNo);
           if (res == DialogResult.Yes)
           {
               this.tslUpdate_Click(null, null);
               idg.reload_DBAdapter();
               Initialize();
           }
        }
        private void tSLwriteXml_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "*.xml";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                idg.WriteXml(sf.FileName);
            }
        }
        private void tSLreadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.FileName = "*.xml";
            if (of.ShowDialog() == DialogResult.OK)
            {
                idg.ReadXml(of.FileName);
            }
        }
        private void inputBox_Validating(object sender, InputBoxValidatingArgs e) 
        { 
                if (e.Text.Trim().Length == 0 && true ) 
                { 
                    e.Cancel = true; 
                    e.Message = "Required"; 
                } 
        }
        private void 凍結左邊欄位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBoxResult result=InputBox.Show("輸入左邊凍結欄位數目:", "凍結欄位", "0", new InputBoxValidatingHandler(inputBox_Validating));
            if (result.OK) { 
                int cnt = int.Parse( result.Text); 
                FrozenLeftColumns(cnt, true);
            } 
        }
        private void 取消凍結ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelForenColumns();
        }
        private void toolStripLabelAddNew_Click(object sender, EventArgs e)
        {
            AddNewBlankRec();
        }
        public virtual void DataGridWF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (idg.NeedUpdate())
            {
                DialogResult res = MessageBox.Show("請先儲存現在資料,可能導至現在編號料資遺失?是 先儲存下載伺服器資料;否放棄操作.", "關閉", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    this.tslUpdate_Click(null, null);
                }
            }
        }
        private FontDialog fontDlg = new FontDialog();
        private void systemFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Font = new Font("System", 12);
        }
        private void setFontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fontDlg.AllowScriptChange = true;
            fontDlg.AllowSimulations = true;
            fontDlg.AllowVectorFonts = false;
            fontDlg.AllowVerticalFonts = false;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                this.dataGridView1.RowTemplate.DefaultCellStyle.Font = fontDlg.Font;
            }
        }

        public void SetUIFont(Font f)
        {
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = f;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
    }
    [Flags]
    public enum BindingListOptions
    {
        AllowNewNo = 0x01,
        AllowModifyNo = 0x02,
        AllowNewNoAndNoPromptReload=0x03,
        none=0x04
    }
    [Flags]
    public enum KeyFieldAotuIDOptions
    {
        AutoUPdateIDNo = 0x01,
        AutoUPdateIDYes = 0x02
    }
    /// <summary>
    /// iDataGridWF
    /// </summary>
    public class iDataGridWF
    {
        public OdbcDataAdapter adapter;
        public OdbcCommandBuilder cmdb;
        public DataSet ds;
        public DataTable dt;
        public string submitUpdateError = null;
        public List<String> readonly_fieldnames = null;
        public List<String> Columns = null;
        public List<DataGridViewColumn> ColumnsType = null;
        protected string c_sql;
        protected string tablename;
        protected int preDBRecCNT = 0;
        protected List<string> param = null;
        private OdbcCommand delete_cmd=null;
        protected OdbcConnection _conn = null;
        public iDataGridWF(string c_sql, string tablename,OdbcConnection conn)
        {
            this.c_sql = c_sql;
            this.tablename = tablename;
            _conn = conn;
            Initialize();
        }
        public iDataGridWF(string c_sql, string tablename,OdbcCommand delcmd,OdbcConnection conn)
        {
            this.c_sql = c_sql;
            this.tablename = tablename;
            _conn = conn;
            delete_cmd=delcmd;
            Initialize();
        }
        public iDataGridWF(List<string> c_sql, string tablename,int flag,OdbcConnection conn)
        {
            param = c_sql;
            this.tablename = tablename;
            _conn = conn;
            Initialize();
        }
        ~iDataGridWF()
        {
            if (param != null) { param.Clear(); }
        }
        public OdbcConnection conn
        {
            set
            {
                _conn = value;
            }
            get
            {
                return null;
            }
        }
        protected virtual void Initialize()
        {
            adapter = new OdbcDataAdapter(c_sql, _conn);
            cmdb = new OdbcCommandBuilder(adapter);
            ds = new DataSet();
            adapter.TableMappings.Add("Table", tablename);
            adapter.Fill(ds);
            if (delete_cmd != null) adapter.DeleteCommand = delete_cmd;
            dt = ds.Tables[tablename];
            preDBRecCNT = dt.Rows.Count;
        }
        public virtual void reload_DBAdapter()
        {
            //FillAdd();Initialize(); 131210 modify by cool;
            ds =new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[tablename];
            preDBRecCNT = dt.Rows.Count;
        }
        public virtual void Fill_AddLastRow(string addnewrowsql)
        {
            OdbcDataAdapter adp = new OdbcDataAdapter();
            adp.SelectCommand = new OdbcCommand(addnewrowsql, _conn);
            adp.TableMappings.Add("Table", tablename);
            adp.Fill(dt);
            preDBRecCNT = dt.Rows.Count;           
        }
        public void getDataWithSelColumn(List<string> liColumns){
            string[] delmetStrs={"From","from","FROM"};
            string[] arrStr = c_sql.Split(delmetStrs,2,StringSplitOptions.None);
            if(arrStr.Length<2 || liColumns.Count==0) return;
            string selectItem="";
            selectItem = liColumns[0].ToString();
            for(int i=1;i<liColumns.Count;i++)
            {
                selectItem +=  "," + liColumns[i].ToString() ;   
            }
            string select_sql=" select "+selectItem+ " from "+arrStr[1];
            MessageBox.Show(select_sql);
            adapter = new OdbcDataAdapter(select_sql,_conn);
            cmdb = new OdbcCommandBuilder(adapter);
            ds = new DataSet();
            adapter.TableMappings.Add("Table", tablename);
            adapter.Fill(ds);
            dt = ds.Tables[tablename];
            return;
        }
        public virtual void submitAddRow(DataRow aRow)
        {
            DataRow[] drA ={ aRow };
            adapter.Update(drA);
        }
        public virtual void submitModifyRow(DataRow aRow)
        {
            try
            {
                DataRow[] drA ={ aRow };
                adapter.Update(drA);
            }
            catch (Exception e1)
            {
               submitUpdateError+=string.Format("{0,10}{1}\n",aRow[0].ToString(),e1.Message);
            }
        }
        public virtual void submitDeleteRow(DataRow aRow)
        {
            DataRow[] drA ={ aRow };
            try
            {
                adapter.Update(drA);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        public virtual int submitUpdate()
        {
            submitUpdateError = null;
            int cnt = 0;
            int updatecnt = 0;
            //dt.AcceptChanges();
            //IEnumerator rowEnum = dt.Rows.GetEnumerator();
            List<DataRow> delRows = new List<DataRow>();
            foreach(DataRow currRow in dt.Rows)
            {
                //while (rowEnum.MoveNext())
                //  DataRow currRow = (DataRow)rowEnum.Current;
                switch (currRow.RowState)
                {
                    case DataRowState.Unchanged: break;
                    case DataRowState.Added: submitAddRow(currRow); cnt++; updatecnt++; break;
                    case DataRowState.Modified: submitModifyRow(currRow); cnt++; break;
                    case DataRowState.Deleted: delRows.Add(currRow); cnt++; break;
                    default:
                        try
                        {
                            DataRow[] drA ={ currRow };
                            adapter.Update(drA);
                            cnt++;
                            currRow.AcceptChanges();
                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(e1.Message);
                        }
                        cnt++;
                        break;
                }
            }
            foreach (DataRow currRow in delRows)
            {
                submitDeleteRow(currRow); cnt++;
            }
            if (submitUpdateError != null)
            {
                PubInfoBox info = new PubInfoBox();
                info.memo.Text = submitUpdateError;
                info.ShowDialog();
            }
            else
            {
            }
            return cnt;
        }
        public virtual bool NeedUpdate()
        {
            foreach (DataRow currRow in dt.Rows)
            {
                switch (currRow.RowState)
                {
                    case DataRowState.Unchanged: break;
                    case DataRowState.Added: return true; 
                    case DataRowState.Modified: return true; 
                    case DataRowState.Deleted: return true; 
                    default:
                        break;
                }

            }
            return false;
        }
        public virtual void ImportXls(string filename,Hashtable dict)
        {
            PubExcel el = new PubExcel(filename, "sheet1");
            el.Visible = true;
            el.ImportXls(dt);
            el.closeExcel();
        }
        public virtual void Exportxls(string filename,Hashtable dict)
        {
            if (dt.Columns.Count > 127)
            {
                MessageBox.Show("欄位過長,超過127,現在欄位為"+dt.Columns.Count);
                return;
            }
            bool datetimefield_Str_flag = true;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                {
                    if (MessageBox.Show("日期欄位匯出時文字表示,是; 否則用數字表示", "日期欄位", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        datetimefield_Str_flag = false;
                    }
                }
            }
            PubExcel el = new PubExcel(datetimefield_Str_flag);
            el.Visible = true;
            el.ExportXls(dt, dict);
            el.saveTo(filename, true);
            el.closeExcel();
        }
        public virtual void WriteXml(string filename)
        {
            dt.WriteXml(filename);
            MessageBox.Show(filename);
            dt.WriteXmlSchema(filename + ".schema", true);
            MessageBox.Show(filename);
        }
        public virtual void ReadXml(string filename)
        {
            dt.ReadXml(filename);
        }
    }
    public class iCMDDataGridWF:iDataGridWF
    {
        public iCMDDataGridWF(OdbcCommand selcmd, OdbcCommand updcmd)
            : base(null, null,null)
        {
            this.adapter=new OdbcDataAdapter();
            adapter.SelectCommand = selcmd;
            adapter.UpdateCommand = updcmd;
            ds = new DataSet();
            adapter.TableMappings.Add("Table", "tablename");
            adapter.Fill(ds);
            dt = ds.Tables["tablename"];
            preDBRecCNT = dt.Rows.Count;
        }
        public iCMDDataGridWF(OdbcCommand selcmd, OdbcCommand updcmd,OdbcCommand delcmd)
            : base(null, null,null)
        {
            this.adapter = new OdbcDataAdapter();
            adapter.SelectCommand = selcmd;
            adapter.UpdateCommand = updcmd;
            adapter.DeleteCommand = delcmd;
            ds = new DataSet();
            adapter.TableMappings.Add("Table", "tablename");
            adapter.Fill(ds);
            dt = ds.Tables["tablename"];
            preDBRecCNT = dt.Rows.Count;
        }


        protected override void Initialize()
        {
        }
        public override void submitAddRow(DataRow aRow)
        {
            MessageBox.Show("唯讀!");
        }
        public override void submitModifyRow(DataRow aRow)
        {
            //base.submitAddRow(aRow);
            base.submitModifyRow(aRow);

        }
        public override void submitDeleteRow(DataRow aRow)
        {
            if (adapter.DeleteCommand == null)
            { MessageBox.Show("唯讀!"); }
            else
            {
                base.submitDeleteRow(aRow);
            }
        }
    }
    public class iClientDataTableGridWF:iDataGridWF
    {
        public iClientDataTableGridWF(DataTable adt)
            : base(null, null,null)
        {
            dt = adt;
        }
        protected override void Initialize()
        {
        }

        public override void submitAddRow(DataRow aRow)
        {
            MessageBox.Show("唯讀!");
        }
        public override void submitModifyRow(DataRow aRow)
        {
            MessageBox.Show("唯讀!");
        }
        public override void submitDeleteRow(DataRow aRow)
        {
            MessageBox.Show("唯讀!");
        }
    }
    #region xmlfileDataGrid
    public class xmlfileDGWF : DataGridWF
    {
        public xmlfileDGWF(iDataGridWF idg, Hashtable adict):base(idg,adict)
        {
                this.tslUpdate.Visible = false;
                this.toolStripLabel2.Visible = false;
        }
    }
    public class ixmlfileDGWF : iDataGridWF
    {
        public ixmlfileDGWF(string c_sql, string tablename):base( c_sql,  tablename,null)
        {
        }
        protected override void Initialize()
        {
            dt = new DataTable();
            dt.ReadXmlSchema( c_sql);
            if (tablename != null)
            {
                dt.ReadXml(tablename);
            }
        }
    }
    #endregion xmlfileDataGrid

    public class es_userDGWF : DataGridWF
    {
        public es_userDGWF(es_userDataGridWF idg, Hashtable adict)
            : base(idg, adict)
        {
            this.Menu=new MainMenu();
            this.Menu.MenuItems.Add(new MenuItem("功能&FUNCTION",idg.Click));
        }
    }
    public class es_userDataGridWF : iDataGridWF
    {
        public es_userDataGridWF(string c_sql, string tablename,OdbcConnection conn)
            : base(c_sql, tablename,conn)
        {
        }
        public void Click(Object sender,EventArgs e) 
        {
            MessageBox.Show("hello");
        }
        public override void submitDeleteRow(DataRow aRow)
        {
//          base.submitDeletdRow(aRow);
            OdbcCommand cmd = new OdbcCommand("delete from es_user where userid=?;", _conn);
            cmd.Parameters.Add("@userid", OdbcType.Int,11,"userid");
            cmd.Parameters["@userid"].Value =  aRow["userid",DataRowVersion.Original];
            cmd.ExecuteNonQuery();
        }
    }
   
    public interface iESUIFONT
    {
        void SetUIFont(System.Drawing.Font f);
    }
}