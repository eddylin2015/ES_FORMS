using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace ES_FORMS
{
    public interface iTableForm_ACT
    {
        int Get_Item_Cnt(string si_id, OdbcConnection conn);
        void Set_TablePanelStyle(System.Windows.Forms.TableLayoutPanel tablePanel);
        void Set_TablePanelColumnHead(System.Windows.Forms.TableLayoutPanel tablePanel, string[] s_ar);
        void tablePanel_KeyUp(System.Windows.Forms.TableLayoutPanel tablePanel, object sender, System.Windows.Forms.KeyEventArgs e);
        void Set_TablePanel_TextBox_ForData(TableLayoutPanel tablePanel, string feildnames, string si_id, OdbcConnection conn);
        void tablePanel_TextChanged(TableLayoutPanel tablePanel, OdbcConnection conn, object sender, EventArgs e);
    }
    class KClassTableFormAct : iTableForm_ACT
    {

        public int Get_Item_Cnt(string si_id, OdbcConnection conn)
        {
            int row_cnt = 0;
            using (OdbcDataReader dr = new OdbcCommand("select count(*) from KClass ;", conn).ExecuteReader())
            {
                if (dr.Read())
                    row_cnt = int.Parse(dr[0].ToString());
            }
            return row_cnt;
        }
        public void Set_TablePanelStyle(System.Windows.Forms.TableLayoutPanel tablePanel)
        {
            tablePanel.ColumnStyles.Clear();
            tablePanel.RowStyles.Clear();
            for (int i = 0; i < tablePanel.ColumnCount; i++)
            {
                tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            }
            for (int i = 0; i < tablePanel.RowCount; i++)
            {
                tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            }
        }
        public void Set_TablePanelColumnHead(System.Windows.Forms.TableLayoutPanel tablePanel, string[] s_ar)
        {
            for (int c_i = 0; c_i < tablePanel.ColumnCount; c_i++)
            {
                System.Windows.Forms.Label lb = new System.Windows.Forms.Label();
                lb.Text = s_ar[c_i];
                tablePanel.Controls.Add(lb, c_i, 0);
            }
        }
        public void tablePanel_KeyUp(System.Windows.Forms.TableLayoutPanel tablePanel, object sender, System.Windows.Forms.KeyEventArgs e)
        {
        /*    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                TextBox tb = (TextBox)sender;
                string name_head = tb.Name.Split('_')[0];
                bool flag = false;
                foreach (Control c in tablePanel.Controls)
                {
                    if (flag && c.Name.Contains(name_head)) { c.Focus(); break; }
                    if (c.Name == tb.Name) flag = true;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                TextBox tb = (TextBox)sender;
                string name_head = tb.Name.Split('_')[0];
                Control p_ctr = null;
                foreach (Control c in tablePanel.Controls)
                {
                    if (p_ctr == null && c.Name.Contains(name_head)) p_ctr = c;
                    if (c.Name == tb.Name) { p_ctr.Focus(); break; }
                    if (c.Name.Contains(name_head)) { p_ctr = c; }
                }
            }*/
        }
        public void tablePanel_TextChanged(TableLayoutPanel tablePanel, OdbcConnection conn, object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string[] s_ar = tb.Name.Split('_');
            string rc_id = s_ar[s_ar.Length - 1];
            string field_name = s_ar[0];
            for (int i = 1; i < s_ar.Length - 1; i++)
            {
                field_name += "_" + s_ar[i];
            }
            string value = tb.Text;
            string sql = string.Format("update sport_rc set {0}=? where rc_id={1};", field_name, rc_id);
            using (OdbcCommand cmd = new OdbcCommand(sql, conn))
            {
                cmd.Parameters.Add("@" + field_name, OdbcType.VarChar, 32, field_name);
                cmd.Parameters["@" + field_name].Value = value;
                cmd.ExecuteNonQuery();
            }
            //throw new NotImplementedException();
        }

        public void Set_TablePanel_TextBox_ForData(TableLayoutPanel tablePanel, string feildnames, string si_id, OdbcConnection conn)
        {
            string sql = string.Format("select rc_id,{0} from sport_rc where si_id={1} order by group_id,road;", feildnames, si_id);
            int r_i = 1;
            Font fnt = new Font("¼Ð·¢Åé", 10);
            using (OdbcDataReader dr = new OdbcCommand(sql, conn).ExecuteReader())
            {
                while (dr.Read())
                {
                    string rc_id = dr[0].ToString();
                    int min_int = tablePanel.ColumnCount;

                    if (dr.FieldCount - 3 < min_int) min_int = dr.FieldCount - 3;
                    for (int i = 0; i < min_int; i++)
                    {

                        if (dr.GetName(i + 3) == "rank" || dr.GetName(i + 3) == "rc" || dr.GetName(i + 3) == "grk" || dr.GetName(i + 3) == "note")
                        {
                            TextBox tb = new TextBox();
                            tb.TextAlign = HorizontalAlignment.Right;
                            tb.Font = fnt;
                            tb.Name = String.Format("{0}_{1}", dr.GetName(i + 3), rc_id);
                            tb.Text = dr[i + 3].ToString();
                            tablePanel.Controls.Add(tb, i, r_i);
                        }
                        else
                        {
                            TextBox lb = new TextBox(); lb.ReadOnly = true; lb.Text = dr[i + 3].ToString(); tablePanel.Controls.Add(lb, i, r_i);
                        }
                    }
                    r_i++;
                }
            }
        }
    }
}
