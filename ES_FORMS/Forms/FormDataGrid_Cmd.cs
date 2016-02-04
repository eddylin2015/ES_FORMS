using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace ES_FORMS
{
    public partial class FormDataGrid_Cmd : Form
    {
        protected Hashtable dict = null;
        protected iFormGrid idg;
        public BindingSource customersBindingSource = new BindingSource();
        public string DefaultFileName = null;
        private bool InitializeFlag = false;
        public FormDataGrid_Cmd(iFormGrid idg, Hashtable adict, BindingListOptions bloption)
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
                case BindingListOptions.AllPri:
                    break;
            }
            Initialize();
            pasteGrid.Click += new EventHandler(gridpaste_Click);
            copyGrid.Click += new EventHandler(copyGrid_Click);
            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(m_Grid_CellMouseClick);
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
        }

        void copyGrid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            if (this.dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(
                        this.dataGridView1.GetClipboardContent());

                    // Replace the text box contents with the clipboard text.
                    //this.TextBox1.Text = Clipboard.GetText();
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                }
            }
        }
        public void InitializeAfterConstructor()
        {
            if (!InitializeFlag)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            InitializeFlag = true;
            this.bindingNavigator1.BindingSource = this.customersBindingSource;
            this.customersBindingSource.DataSource = idg.GetDT();
            this.dataGridView1.DataSource = this.customersBindingSource;
            if (dict == null) return;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dict.Contains(dataGridView1.Columns[i].Name.ToLower()))
                {
                    dataGridView1.Columns[i].HeaderCell.Value = dict[dataGridView1.Columns[i].Name.ToLower()];
                }
            }
        }
         
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ReadOnly = !this.dataGridView1.ReadOnly;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CurrencyManager fcm;
            fcm = (CurrencyManager)this.BindingContext[this.dataGridView1.DataSource];
            fcm.EndCurrentEdit();
            // int i=adapter.Update(ds);
            this.dataGridView1.ReadOnly = true;
            int i = idg.submitUpdate();
            this.dataGridView1.ReadOnly = false;
            this.dataGridView1.Focus();
            // PubDialog.ShowUpdateCnt(i);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "*.xls";
            if (DefaultFileName != null) sf.FileName = DefaultFileName;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                idg.Exportxls(sf.FileName, dict);
                if (MessageBox.Show("�O�_���}���" + sf.FileName, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ShellExec.ShellExecute(IntPtr.Zero, new StringBuilder("Open"), new StringBuilder(sf.FileName), new StringBuilder(""), new StringBuilder(""), 1);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        void m_Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Visible = true;

            }
        }

        void gridpaste_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            PasteClipboard();

        }
        private void PasteClipboard()
        {
            try
            {
                string s = Clipboard.GetText();
                MessageBox.Show(s);

                string[] lines = s.Split('\n');
                int iFail = 0, iRow = dataGridView1.CurrentCell.RowIndex;
                int iCol = dataGridView1.CurrentCell.ColumnIndex;
                DataGridViewCell oCell;
                foreach (string line in lines)
                {
                    if (iRow < dataGridView1.RowCount && line.Length > 0)
                    {
                        string[] sCells = line.Split('\t');
                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i < this.dataGridView1.ColumnCount)
                            {
                                oCell = dataGridView1[iCol + i, iRow];
                                //MessageBox.Show(oCell.Value.ToString());
                                if (!oCell.ReadOnly)
                                {
                                    //if (oCell.Value.ToString() != sCells[i])
                                    //{
                                    oCell.Value = Convert.ChangeType(sCells[i],
                                                          oCell.ValueType);
                                    oCell.Style.BackColor = Color.Tomato;
                                    //}
                                    //else
                                    //  iFail++;
                                    //only traps a fail if the data has changed 
                                    //and you are pasting into a read only cell
                                }
                            }
                            else
                            { break; }
                        }
                        iRow++;
                    }
                    else
                    { break; }
                    if (iFail > 0)
                        MessageBox.Show(string.Format("{0} updates failed due" +
                                        " to read only column setting", iFail));
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
    }
    public class FormDataGrid_Cmd_Importxls:FormDataGrid_Cmd
    {
        public FormDataGrid_Cmd_Importxls(iFormGrid idg, Hashtable adict, BindingListOptions bloption)
            : base(idg, adict, bloption)
        {
            bindingNavigator1.Items.Add("importxls", null, tslImportXls_Click);
        }

        private void tslImportXls_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog of = new OpenFileDialog();
            of.FileName = "*.XLS";
            if (of.ShowDialog() == DialogResult.OK)
            {
                idg.Importxls(of.FileName, dict);
            }
        }
    }
    [Flags]
    public enum BindingListOptions
    {
        AllowNewNo = 0x01,
        AllowModifyNo = 0x02,
        AllPri = 0x03
    }
    public interface iFormGrid
    {
        void submitAddRow(DataRow aRow);
        void submitDeleteRow(DataRow aRow);
        void submitModifyRow(DataRow aRow);
        int submitUpdate();
        void Exportxls(String filename, Hashtable dict);
        void Importxls(String filename, Hashtable dict);
        DataTable GetDT();
        void FillDT();
        bool GetRowChanging();
    }
    public class iDTFromGrid : iFormGrid
    {
        public DbDataAdapter adapter;
        public DataSet ds;
        public DataTable dt;
        public string submitUpdateError = null;
        protected string c_sql;
        protected string tablename;
        protected int preDBRecCNT = 0;
        private bool rowchanging = false;
        public iDTFromGrid(DataTable pdt)
        {
            dt = pdt;

        }
        public void submitAddRow(DataRow aRow) { }
        public void submitDeleteRow(DataRow aRow) { }
        public void submitModifyRow(DataRow aRow) { }
        public int submitUpdate() { return 0; }
        public void Exportxls(String filename, Hashtable dict)
        {
            if (dt.Columns.Count > 127)
            {
                MessageBox.Show("���L��,�W�L127,�{�b��쬰" + dt.Columns.Count);
                return;
            }
            bool datetimefield_Str_flag = true;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                {
                    if (MessageBox.Show("������ץX�ɤ�r���,�O; �_�h�μƦr���", "������", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        datetimefield_Str_flag = false;
                    }
                }
            }
            office.PubExcel el = new office.PubExcel(datetimefield_Str_flag);
            el.Visible = true;
            el.ExportXls(dt, dict);
            el.saveTo(filename, true);
            el.closeExcel();
        }
        public void Importxls(String filename, Hashtable dict) { }
        public DataTable GetDT() { return dt; }
        public void FillDT() { }
        public bool GetRowChanging() { return false; }

    }
    public class iSqlCmdFormGrid : iFormGrid
    {
        public DbDataAdapter adapter;
        public DataSet ds;
        public DataTable dt;
        public string submitUpdateError = null;
        protected string c_sql;
        protected string tablename;
        protected int preDBRecCNT = 0;
        private bool rowchanging = false;
        public iSqlCmdFormGrid(DbDataAdapter pAdapter,DbCommand selcmd, DbCommand updcmd, DbCommand insertcmd, DbCommand delcmd)
        {
            this.adapter = pAdapter;
            if(selcmd!=null) adapter.SelectCommand = selcmd;
            if(updcmd!=null) adapter.UpdateCommand = updcmd;
            if(insertcmd!=null) adapter.InsertCommand = insertcmd;
            if(delcmd!=null)adapter.DeleteCommand = delcmd;
            adapter.TableMappings.Add("Table", "tablename");
            dt = new DataTable();
            adapter.Fill(dt);
            preDBRecCNT = dt.Rows.Count;
            this.dt.RowChanging += new DataRowChangeEventHandler(dt_RowChanging);
        }

        void dt_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            rowchanging = true;
        }
        public bool GetRowChanging()
        {
            return rowchanging;
        }
        public void FillDT()
        {
            adapter.Fill(dt);
            
        }
        public iSqlCmdFormGrid(DbDataAdapter pAdapter):this(pAdapter,null,null,null,null)
        {
        }
        public DataTable GetDT() { return dt; }
        public void submitAddRow(DataRow aRow)
        {
            DataRow[] drA ={ aRow };
            try
            {
                adapter.Update(drA);
                aRow.AcceptChanges();
            }
            catch (Exception e1)
            {
                submitUpdateError += e1.Message;
            }
        }
        public void submitModifyRow(DataRow aRow)
        {
            DataRow[] drA ={ aRow };
            try
            {
                adapter.Update(drA);
                aRow.AcceptChanges();
            }
            catch (Exception e1)
            {
                submitUpdateError += e1.Message;
            }
        }
        public void submitDeleteRow(DataRow aRow)
        {
            DataRow[] drA ={ aRow };
            try
            {
                adapter.Update(drA);
                aRow.AcceptChanges();
            }
            catch (Exception e1)
            {
                if (e1.Message.Equals( "Cannot perform this operation on a row not in the table.")||
                    e1.Message.Equals("�L�k�藍�s�b���ƪ�����ƦC�W����o���@�~�C"))
                {
                }
                else
                {
                    submitUpdateError += e1.Message;
                }
            }
        }
        public int submitUpdate()
        {
            submitUpdateError = null;
            int cnt = 0;
            int updatecnt = 0;

            //dt.AcceptChanges();
            //IEnumerator rowEnum = dt.Rows.GetEnumerator();

            List<DataRow> delRows = new List<DataRow>();
            foreach (DataRow currRow in dt.Rows)            //while (rowEnum.MoveNext())
            {
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
            /*
           else
           {
               DialogResult reply = MessageBox.Show("��s����, �O�_�q���A���s���J���?",
                           "Yes or No ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
               if (reply == DialogResult.Yes && updatecnt > 0)
               {
                   //preDBRecCNT
                   //      adapter.Fill(ds, preDBRecCNT, cnt, tablename);
               }
               else
               {
                   //MessageBox.Show("No");
               }
            
           }*/
            //dt.AcceptChanges();
            return cnt;
        }
        public void Exportxls(String filename, Hashtable dict)
        {
            if (dt.Columns.Count > 127)
            {
                MessageBox.Show("���L��,�W�L127,�{�b��쬰" + dt.Columns.Count);
                return;
            }
            bool datetimefield_Str_flag = true;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].DataType.Equals(System.Type.GetType("System.DateTime")))
                {
                    if (MessageBox.Show("������ץX�ɤ�r���,�O; �_�h�μƦr���", "������", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        datetimefield_Str_flag = false;
                    }
                }
            }
            office.PubExcel el = new office.PubExcel(datetimefield_Str_flag);
            el.Visible = true;
            el.ExportXls(dt, dict);
            el.saveTo(filename, true);
            el.closeExcel();
        }
        public virtual void Importxls(string filename, Hashtable dict)
        {
            office.PubExcel el = new office.PubExcel(filename, "sheet1");
            el.Visible = true;
            el.ImportXls(dt);
            el.closeExcel();
        }
    }
    public class ShellExec
    {
        [DllImport("shell32.dll")]
        public static extern int ShellExecute(IntPtr hwnd, StringBuilder lpszOp, StringBuilder lpszFile, StringBuilder lpszParams, StringBuilder lpszDir, int FsShowCmd);
    }
}