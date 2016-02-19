using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Xml;


namespace ES_FORMS.SFFORMS
{
    public partial class FormStafInfo : Form
    {
        private List<ListItem> lsworkStatus = new List<ListItem>();
        private List<ListItem> lsstafGrade = new List<ListItem>();
        private void cbJobReasonKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (e.KeyChar == '~' || e.KeyChar == '～')
            {
                // Stop the character from being entered into the control since it is non-numerical.
                ComboBox cbx = (ComboBox)sender;
                refreshcbItems(cbx);
                e.Handled = true;
            }
        }
        private void refreshcbJobReasonItems(ComboBox cbx)
        {
            cbx.DataSource = null;
            OdbcCommand cmd = new OdbcCommand("select v,d from sa_items where f='JobReason'", conn);
            OdbcDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbx.Items.Add( dr[1].ToString());
            }
            dr.Close();
            cbx.Refresh();
        }

        private void cbLeaveJobReasonKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (e.KeyChar == '~' || e.KeyChar == '～')
            {
                // Stop the character from being entered into the control since it is non-numerical.
                ComboBox cbx = (ComboBox)sender;
                refreshcbItems(cbx);
                e.Handled = true;
            }
        }
        private void refreshcbLeaveJobReasonItems(ComboBox cbx)
        {
            cbx.DataSource = null;
            OdbcCommand cmd = new OdbcCommand("select v,d from sa_items where f='LeaveJobReason'", conn);
            OdbcDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbx.Items.Add(dr[1].ToString());
            }
            dr.Close();
            cbx.Refresh();
        }
        private void cbKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (e.KeyChar == '~' || e.KeyChar == '～')
            {
                // Stop the character from being entered into the control since it is non-numerical.
                ComboBox cbx=(ComboBox)sender;
                refreshcbItems(cbx);
                e.Handled = true;
            }
        }
        #region checkbox SchoolSect 學部以1,2,4,8,16表示(1:中學,2:小學,4:072小學,8:幼,16:其他)
        private void SchoolSect_Group_checkedbox_item_Click(Object sender, EventArgs e)
        {
            int[] ar ={ 0xFE, 0xFD, 0xFB, 0xF7,0xEF };
            int[] ar1 ={ 1,2,4,8,16};
            CheckBox cb = (CheckBox)sender;
            string [] s=cb.Name.ToString().Split('m');
            int v = int.Parse(s[1]);
            int o = 0;
            try
            {
                 o = int.Parse(groupBox1.Text);
            }catch
            {
            }         
            if (cb.Checked == true)
            { groupBox1.Text = (o |ar1[ v]).ToString(); }
            else
            { groupBox1.Text = (o & ar[v]).ToString(); }
        }
    
        private void SchoolSect_checkedbox_Format(Object sender, ConvertEventArgs e)
        {
            int[] ar ={ 1, 2, 4, 8,16 };
            int v = int.Parse(e.Value.ToString());
             for(int i=0;i<this.groupBox1.Controls.Count;i++)
             {
                 CheckBox rb = (CheckBox)groupBox1.Controls[i];
                 string[] s = rb.Name.ToString().Split('m');
                 int o = int.Parse(s[1]);
                 if ((v & ar[o]) > 0)
                 {
                     rb.CheckState = CheckState.Checked;
                 }
                 else { rb.CheckState = CheckState.Unchecked; }
             }
         }
         #endregion checkbox SchoolSect
      
        private void cbparse(Object sender, ConvertEventArgs e)
        {
            List<ListItem> list;
            Binding bdg = (Binding)sender;
            if (bdg.BindingMemberInfo.BindingMember.Equals("workStatus"))
            {
                list = lsworkStatus;
            }
            else if (bdg.BindingMemberInfo.BindingMember.Equals("staf_Grade"))
            {
                list = lsstafGrade;
            }
            else
            {
                list = new List<ListItem>();
            }
            bool b = false;
            foreach (ListItem li in list)
            {
                if (e.Value.ToString().Equals(li.Name))
                {
                    e.Value = li.ID;
                    b = true;
                    break;
                }
            }
            if (!b) e.Value = "0";
        }

        private void cbformat(Object sender, ConvertEventArgs e)
        {
            List<ListItem> list;
            Binding bdg=(Binding)sender;
            if (bdg.BindingMemberInfo.BindingMember.Equals("workStatus"))  { list=lsworkStatus; }
            else if (bdg.BindingMemberInfo.BindingMember.Equals("staf_Grade"))  {  list = lsstafGrade;  }
            else{
                list = new List<ListItem>();
            }
            bool b = false;
            foreach (ListItem li in list)
            {
                if (e.Value.ToString().Equals(li.ID))
                {
                    e.Value = li.Name;
                    b = true;
                    break;
                }
            }
            if (!b) e.Value = "未選";
        }

        private void refreshcbItems(ComboBox cbx)
        {
                cbx.DataSource = null;
                OdbcCommand cmd = new OdbcCommand("select v,d from sa_items where f='" + cbx.Name.ToString() + "' order by v", conn);
                OdbcDataReader dr = cmd.ExecuteReader();
                List<ListItem> list;
                if (cbx.Name.ToString().Equals("workStatus")) { 
                    list = lsworkStatus; 
                    list.Clear();
                }
                else if (cbx.Name.ToString().Equals("staf_Grade")) { 
                    list = lsstafGrade; 
                    list.Clear();
                }
                else
                {
                    list = new List<ListItem>();
                    list.Clear();
                }
                while (dr.Read())
                {
                    list.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                }  
                cbx.DataSource = list;
                dr.Close();
                cbx.Refresh();
        }
        private OdbcConnection conn = null;
        public FormStafInfo(OdbcConnection pconn,String staf_ref):base()
        {
            conn = pconn;
            InitializeComponent();
            //this.odbcConnection1 = conn;
            #region workStatus
            this.workStatus.DisplayMember = "Name";
            this.workStatus.ValueMember = "ID";
            Binding workStatusbind = new Binding("Text", this.tableBindingSource, "workStatus");
            workStatusbind.Parse += cbparse;
            workStatusbind.Format += cbformat;
            workStatus.DataBindings.Add(workStatusbind);
            workStatus.KeyPress += cbKeyPress;
            refreshcbItems(workStatus);
            #endregion workStatus

            #region staf_Grade
            this.staf_Grade.DisplayMember = "Name";
            this.staf_Grade.ValueMember = "ID";
            Binding staf_Gradebind = new Binding("Text", this.tableBindingSource, "staf_Grade");
            staf_Gradebind.Parse += cbparse;
            staf_Gradebind.Format += cbformat;
            staf_Grade.DataBindings.Add(staf_Gradebind);
            staf_Grade.KeyPress += cbKeyPress;
            refreshcbItems(staf_Grade);
            #endregion staf_Grade
            #region workDept
            Binding workDeptbind = new Binding("Text", this.tableBindingSource, "workDept");
            
            workDept.DataBindings.Add(workDeptbind);
            workDept.KeyPress += cbKeyPress;
            refreshcbItems(workDept);
            #endregion workDept

            #region Nat
            Binding Natbind = new Binding("Text", this.tableBindingSource, "Nat");
            Nat.DataBindings.Add(Natbind);
            Nat.KeyPress += cbKeyPress;
            refreshcbItems(Nat);
            #endregion Nat

            #region PlaceOfBirth
            Binding PlaceOfBirth_Bind = new Binding("Text", this.tableBindingSource, "PlaceOfBirth");
            PlaceOfBirth.DataBindings.Add(PlaceOfBirth_Bind);
            PlaceOfBirth.KeyPress += cbKeyPress;
            refreshcbItems(PlaceOfBirth);
            #endregion PlaceOfBirth
            
            #region PlaceOfFamily
            Binding PlaceOfFamily_Bind = new Binding("Text", this.tableBindingSource, "PlaceOfFamily");
            PlaceOfFamily.DataBindings.Add(PlaceOfFamily_Bind);
            PlaceOfFamily.KeyPress += cbKeyPress;
            refreshcbItems(PlaceOfFamily);
            #endregion PlaceOfFamily
            #region livePlace
            Binding livePlace_Bind = new Binding("Text", this.tableBindingSource, "livePlace");
            livePlace.DataBindings.Add(livePlace_Bind);
            livePlace.KeyPress += cbKeyPress;
            refreshcbItems(livePlace);
            #endregion livePlace

            #region acctItem
            acctItem.KeyPress += cbKeyPress;
            refreshcbItems(acctItem);
            #endregion acctItem
            #region workType
            workType.KeyPress += cbKeyPress;
            refreshcbItems(workType);
            #endregion workType
            #region workPosi
            workPosi.KeyPress += cbKeyPress;
            refreshcbItems(workPosi);
            #endregion workPosi
            #region MaritalStatus
            MaritalStatus.KeyPress += cbKeyPress;
            refreshcbItems(MaritalStatus);
            #endregion MaritalStatus

            #region DriverLicense
            DriverLicense.KeyPress += cbKeyPress;
            refreshcbItems(DriverLicense);
            #endregion DriverLicense
            #region Religion
            Religion.KeyPress += cbKeyPress;
            refreshcbItems(Religion);
            #endregion Religion
            #region Church
            Church.KeyPress += cbKeyPress;
            refreshcbItems(Church);
            #endregion Church
            #region ID_Type
            ID_Type.KeyPress += cbKeyPress;
            refreshcbItems(ID_Type);
            #endregion ID_Type
            #region NLID_Type
            NLID_Type.KeyPress += cbKeyPress;
            refreshcbItems(NLID_Type);
            #endregion NLID_Type
            #region bank
            bank.KeyPress += cbKeyPress;
            refreshcbItems(bank);
            #endregion bank
            #region job leave reason
            j1r.KeyPress += this.cbJobReasonKeyPress;
            j2r.KeyPress += this.cbJobReasonKeyPress;
            j3r.KeyPress += this.cbJobReasonKeyPress;
            j1l.KeyPress += this.cbLeaveJobReasonKeyPress;
            j2l.KeyPress += this.cbLeaveJobReasonKeyPress;
            j3l.KeyPress += this.cbLeaveJobReasonKeyPress;
            refreshcbJobReasonItems(j1r);
            refreshcbJobReasonItems(j2r);
            refreshcbJobReasonItems(j3r);
            refreshcbLeaveJobReasonItems(j1l);
            refreshcbLeaveJobReasonItems(j2l);
            refreshcbLeaveJobReasonItems(j3l);
            #endregion job leave reason

            cbSEX.Items.Add("男"); cbSEX.Items.Add("女");

            #region chbSchoolSect
            Binding grpbind = new Binding("Text", this.tableBindingSource, "SchoolSect");
            grpbind.Format += this.SchoolSect_checkedbox_Format;
            this.groupBox1.DataBindings.Add(grpbind);
            this.cbxSchoolSectGrpItem0.Click += this.SchoolSect_Group_checkedbox_item_Click;
            this.cbxSchoolSectGrpItem1.Click += this.SchoolSect_Group_checkedbox_item_Click;
            this.cbxSchoolSectGrpItem3.Click += this.SchoolSect_Group_checkedbox_item_Click; 
            this.cbxSchoolSectGrpItem4.Click += this.SchoolSect_Group_checkedbox_item_Click;
            this.cbxSchoolSectGrpItem2.Click += this.SchoolSect_Group_checkedbox_item_Click;
            #endregion chbShoolSect
            this.sp_sdate.Mask="0000/00/00";
            this.edu_sdate.Mask = "0000/00";
            this.tt_sdate.Mask = "0000/00";
            this.cd_date.Mask = "0000/00";
            this.te_ldate.Mask = "0000/00";
            this.te_sdate.Mask = "0000/00";
            this.we_ldate.Mask = "0000/00";
            this.we_sdate.Mask = "0000/00";
            this.sp_moneytype.DataSource = null;
            this.sp_moneytype.DisplayMember = "CURRENCY";
            this.sp_moneytype.ValueMember = "CODE";
            OdbcCommand cmd = new OdbcCommand("select currency,code,ExchRate from currencyBaseMopExch;", conn);
            OdbcDataReader dr = cmd.ExecuteReader();
            List<ListItem1> lis = new List<ListItem1>();
            while (dr.Read())
            {
                lis.Add(new ListItem1(dr[0].ToString(), dr[1].ToString(), (float)dr[2]));
            }
            dr.Close();
            this.sp_moneytype.DataSource = lis;           
            this.dTSaEDUDataGridView.EditingControlShowing += this.dTSaEDUDataGridView_EditingControlShowing;
            this.dTSaTeachTrainDataGridView.EditingControlShowing += this.dTSaTeachTrainDataGridView_EditingControlShowing;
            Fill(staf_ref,"");
        }
        private String Where_Condition = "";
        /// <summary>
        /// Fill 為DATASET填充資料
        /// </summary>
        /// <param name="condi">查詢條件</param>
        /// <param name="wf">查詢條件ＦＯＲＭ接口</param>
        public virtual void Fill(string staf_Ref,String condi)
        {            
            Where_Condition=" where staf_ref ='"+staf_Ref+"';";
            this.odbcDataAdapter1.SelectCommand.CommandText = @"SELECT staf_ref, Age, acctItem,acctitem_no, Address,   
                           bank, bankAcct, bankAcctName, c_name,  Church, DateOfBirth, CR_Date, DateOfBaptism, 
                          DriverLicense, DSEJ_Ref,DSEJ_SREF,DSEJ_REGTYPE, EFF_DATE, EM_PhoneNo, e_name, EM_Name, 
                          EMail,  FSS_NO,  FSS_NO_1,  HXZ_No, HXZ_T,
                          ID_No, ID_Type, j1_lreason, j1_reason, j1_ldate, j1_sdate, j2_ldate, j2_lreason, 
                          j2_reason, j2_sdate, j3_ldate, j3_sdate, j3_lreason, j3_reason, MaritalStatus, 
                          MobileNo, Nat, NLID_No, NLID_Type, PhoneNo, PlaceOfBirth, 
                          PlaceOfFamily, Religion, s1, s1DateOfBirth, s1Sex, s2, s2DataOfBirth, s2Sex, 
                          s3, s3DataOfBirth, s3Sex, s4, s4DataOfBirth, s4Sex, s5, s5DataOfBirth, s5Sex, 
                            SchoolSect, Sex, SP_Age, SP_Comp, SP_Job, 
                          SP_DateOfBirth, SP_EName, SP_Name, staf_Grade, TAX_No,  transAcctNo, 
                          workDept, workPosi, workStatus, workType, 
                           church_years, mother_lang, good_lang, normal_lang, 
                          primer_lang, livePlace, addr0_city, addr0_street, addr0_building_no, 
                          addr0_building, addr0_block, addr0_floor, addr0_room, addr1_city, 
                          addr1_street, addr1_building_no, addr1_building, addr1_block, addr1_floor, 
                          addr1_room, safp_no,NCR_Date,NEFF_DATE,DSEJ_QUALI_YEAR
FROM             sa_stafinfo " + Where_Condition;
            this.odbcDataAdapter1.SelectCommand.Connection = conn;
            this.odbcDataAdapter1.InsertCommand.Connection = conn;
            this.odbcDataAdapter1.UpdateCommand.Connection = conn;
            this.odbcDataAdapter1.DeleteCommand.Connection = conn;
            this.odbcDataAdapter1.Fill(ds1.Table);
            adps[0] = odbcDataAdaptereEDU;
            adps[1] = odbcDataAdapterTT;
            adps[2] = odbcDataAdapterCD;
            adps[3] = odbcDataAdapterTE;
            adps[4] = odbcDataAdapterWE;
            adps[5] = odbcDataAdapterSP;
            adps[6] = odbcDataAdapterJO;
            tbls[0] = ds1.DTSaEDU;
            tbls[1] = ds1.DTSaTeachTrain;
            tbls[2] = ds1.DTSaCD;
            tbls[3] = ds1.DTSaTE;
            tbls[4] = ds1.DTSaWE;
            tbls[5] = ds1.DTSaSP;
            tbls[6] = ds1.DTSaJO;
            cols[0] = ds1.DTSaEDU.eduidColumn;
            cols[1] = ds1.DTSaTeachTrain.ttuidColumn;
            cols[2] = ds1.DTSaCD.cdidColumn;
            cols[3] = ds1.DTSaTE.teidColumn;
            cols[4] = ds1.DTSaWE.weidColumn;
            cols[5] = ds1.DTSaSP.spidColumn;
            cols[6] = ds1.DTSaJO.joidColumn;
            dgv[0] = dTSaEDUDataGridView;
            dgv[1] = dTSaTeachTrainDataGridView;
            dgv[2] = dTSaCDDataGridView;
            dgv[3] = dTSaTEDataGridView;
            dgv[4] = dTSaWEDataGridView;
            dgv[5] = dTSaSPDataGridView;
            dgv[6] = dTSaJODataGridView;
            bds[0] = dTSaEDUBindingSource;
            bds[1] = dTSaTeachTrainBindingSource;
            bds[2] = dTSaCDBindingSource;
            bds[3] = dTSaTEBindingSource;
            bds[4] = dTSaWEBindingSource;
            bds[5] = dTSaSPBindingSource;
            bds[6] = dTSaJOBindingSource;
            for (int i = 0; i < subtble_cnt; i++)
            {
                adps[i].SelectCommand.CommandText += Where_Condition;
                adps[i].SelectCommand.Connection = conn;
                adps[i].InsertCommand.Connection = conn;
                adps[i].UpdateCommand.Connection = conn;
                adps[i].DeleteCommand.Connection = conn;
                Fill_SubTables(i);
            }
            this.bindingNavigator1.BindingSource = this.tableBindingSource;
        }
        private static int subtble_cnt = 7;
        private OdbcDataAdapter[] adps = new OdbcDataAdapter[subtble_cnt];
        private DataTable[] tbls = new DataTable[subtble_cnt];
        private DataColumn[] cols = new DataColumn[subtble_cnt];
        private DataGridView[] dgv = new DataGridView[subtble_cnt];
        private BindingSource[] bds = new BindingSource[subtble_cnt];
        private String[] maxsqls = 
                { 
                    "select max( eduid ) mid,count(eduid)  from sa_edu;",
                    "select max( ttuid ) mid,count(ttuid)  from sa_teachtrain;",
                    "select max( cdid ) mid,count(cdid)  from sa_cd;",
                    "select max( teid ) mid,count(teid)  from sa_te;",
                    "select max( weid ) mid,count(weid)  from sa_we;",
                    "select max( spid ) mid,count(spid)  from sa_sp;",
                    "select max( joid ) mid,count(joid)  from sa_jo;"
                };
        private void Fill_SubTables(int subtable_index)
        {
            if(subtable_index>=0 && subtable_index < subtble_cnt)
            {
                int i = subtable_index;
                adps[i].Fill(tbls[i]);
                using (OdbcDataReader dr = new OdbcCommand(maxsqls[i], conn).ExecuteReader())
                {
                    if (dr.Read())
                    {
                        if (dr[0].ToString() != "0")
                            try
                            {
                                cols[i].AutoIncrementSeed = long.Parse(dr[0].ToString()) + 1;
                            }
                            catch
                            {
                                cols[i].AutoIncrementSeed = 1;
                            }
                    }
                }
            }
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            save_data();
        }
        private void save_data(){
            try
            {
                tableBindingSource.EndEdit();
                int[] modifycnt = new int[subtble_cnt];
                //int sum = this.odbcDataAdapter1.Update((DataTable)ds1.Table);
                int sum = this.odbcDataAdapter1.Update( ds1.Table);
                for (int i = 0; i < subtble_cnt; i++)
                {
                    dgv[i].EndEdit();
                    bds[i].EndEdit();
                    modifycnt[i] = adps[i].Update(tbls[i]);
                    sum += modifycnt[i];
                    if (modifycnt[i] > 0)
                    {
                        tbls[i].Clear();
                        Fill_SubTables(i);
                    }
                }
                if (sum > 0)
                {
                    String s = maskedTextBox1.Text;
                    String msg = String.Format("{0}:staf_ref={0},logd={1}", s, this.Text + sum + "筆");
                    ES_FORMS.Publib.Tools.log(msg);
                   /* String u=Pub.cfg.curr_userinfo.userid+Pub.cfg.curr_userinfo.username;
                    String s=maskedTextBox1.Text;
                    String sql = String.Format("insert log (user,staf_ref,mdt,logd) values('{0}','{1}',now(),'{2}');", u, s, this.Text + sum + "筆" );
                    using(OdbcCommand cmd=new OdbcCommand(sql,Pub.cfg.GetDBConn()))
                    {
                        cmd.ExecuteNonQuery();
                    }*/
                }
                MessageBox.Show(string.Format("更新{0}筆,成功", sum));
            }
            catch(System.Data.Odbc.OdbcException e1){
                int i = this.odbcDataAdapter1.Update(ds1.Table);
                MessageBox.Show("更新" + i.ToString() + "筆"+ e1.Message + e1.GetType().ToString() + e1.ErrorCode.ToString());
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message + e1.GetType().ToString());
            }
        }

        private void dSEJ_RefTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (dSEJ_RefTextBox.Text.Length != 8)
            {
                errorProvider1.SetError(dSEJ_RefTextBox, "教青局教師證編號長度為8位");
            }
            else
            {
                errorProvider1.SetError(dSEJ_RefTextBox, null);
            }
        }
        private void dSEJ_SREFTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (dSEJ_SREFTextBox.Text.Length != 8)
            {
                errorProvider1.SetError(dSEJ_SREFTextBox, "教青局職員證編號長度為8位");
            }
            else
            {
                errorProvider1.SetError(dSEJ_SREFTextBox, null);
            }
        }
        private void SA_STAFWF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否儲存資料?","",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                save_data();
            }
        }
        #region edu_grid_autocomplete
        private void dTSaEDUDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
                e.CellStyle.BackColor = Color.Aquamarine;
                if (e.Control is TextBox)
                {
                    TextBox autoText = e.Control as TextBox;
                    if (autoText != null &&dTSaEDUDataGridView.CurrentCell.ColumnIndex==2 )
                    {
                        autoText.Multiline = false;
                        AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                        addEDUSTAGEItems(DataCollection);
                        autoText.AutoCompleteCustomSource = DataCollection;
                        autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                        autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
        }

        public void addEDUSTAGEItems(AutoCompleteStringCollection col)
        {
            col.Add("01 博士學位");
            col.Add("02 碩士學位");
            col.Add("03 研究生畢業");
            col.Add("04 學士學位/大學本科畢業");
            col.Add("05 專科學位");
            col.Add("06 大專/理工文憑");
            col.Add("07 副學士學位");
            col.Add("08 中專畢業");
            col.Add("09 中學畢業/職中畢業");
            col.Add("10 中學未畢業");
            col.Add("11 小學畢業");
            col.Add("12 小學未畢業");
        }
        private void dTSaTeachTrainDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
            e.CellStyle.BackColor = Color.Aquamarine;
            if (e.Control is TextBox)
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null && dTSaTeachTrainDataGridView.CurrentCell.ColumnIndex == 2)
                {
                    autoText.Multiline = false;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addTTSTAGEItems(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
        }
        public void addTTSTAGEItems(AutoCompleteStringCollection col)
        {
            col.Add("A0 教育博士學位");
            col.Add("A1 教育碩士學位");
            col.Add("B 教育研究生畢業");
            col.Add("C 教育學士學位/大學師範本科畢業");
            col.Add("D 學位後教育證書");
            col.Add("E 學位後教育文憑");
            col.Add("F 大專師範高等文憑");
            col.Add("G 大專師範文憑");
            col.Add("H 中專師範畢業/職中師範畢業");
            col.Add("I 聖若瑟等師範文憑");
            col.Add("J 小學及幼兒教育師範課程");
            col.Add("K 具任教中學資格教學人員的小學教學人員培訓課程");
            col.Add("AA 高等課程後師訓實習");
            col.Add("BB 中學課程後師訓實習");
        }


        #endregion
    }
    public class ListItem
    {
        private String id = string.Empty;
        private String name = string.Empty;
        public ListItem(string sid, string sname)
        {
            id = sid; name = sname;
        }
        public override string ToString()
        {
            return this.name;//return base.ToString();
        }
        public string ID { get { return this.id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
    }
    public class ListItem1
    {
        private String currency = string.Empty;
        private String code = string.Empty;
        private float rate = 0;
        public ListItem1(string acurrency, string acode, float arate)
        {
            currency = acurrency; code = acode; rate = arate;
        }
        public override string ToString()
        {
            return this.currency;//return base.ToString();
        }
        public float ExchangeRate { get { return rate; } set { rate = value; } }
        public string CODE { get { return code; } set { code = value; } }
        public string CURRENCY { get { return currency; } set { currency = value; } }
    }
}