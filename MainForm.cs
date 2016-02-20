using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ES_FORMS.MainFormUI;
namespace studmain
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Splitter splitter1;
        public Form1()
        {
            InitializeComponent();
            InitializeComp();
            string cnst5_1DriverConnStrFormat = "Driver={{MySQL ODBC 5.1 Driver}};Server={0};Database={1};UID={2};PWD={3};OPTION=67108867";//optoin=3
            String _conn_txt = null;
            _conn_txt = string.Format(cnst5_1DriverConnStrFormat, "192.168.102.135", "es_studinfo", "studmain", "963852741");
            conn = new OdbcConnection(_conn_txt);
            conn.Open();
            MainMenu fmainMenu = new MainMenu();
            sidebar.Visible = false;
            MenuItem sidebar_mu = fmainMenu.MenuItems.Add("三");
            sidebar_mu.Click += (sender, e) =>
            {
                sidebar.Visible = !sidebar.Visible;
            };
            MenuItem msys = fmainMenu.MenuItems.Add("系統&SYS");
            msys.MenuItems.Add(new MenuItem("登入&LOGIN", this.msyslogin_click, Shortcut.CtrlL));
            msys.MenuItems.Add(new MenuItem("登出&LOGOUT", this.msyslogout_click));
            msys.MenuItems.Add(new MenuItem("退出&EXIT", this.msysexit_click, Shortcut.CtrlE));
            msys = fmainMenu.MenuItems.Add("窗口");
            msys.MenuItems.Add(new MenuItem("排列圖標", this.mnuIcons_click));
            msys.MenuItems.Add(new MenuItem("層層疊疊", this.mnuCascade_click));
            msys.MenuItems.Add(new MenuItem("水平鋪平", this.mnuTileHorizontal_click));
            msys.MenuItems.Add(new MenuItem("垂直鋪平", this.mnuTileVertical_click));
            msys.MenuItems.Add(new MenuItem("關閉所有子窗口", this.CloseAllSubForm_click));
            this.Menu = fmainMenu;
            this.FormClosed += MainForm_FormClosed;

            ES_FORMS.STFORMS.Form_Search fs = new ES_FORMS.STFORMS.Form_Search_Studinfo(conn, this);
            fs.Show();
        }
        private OdbcConnection conn = null;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
            conn.Dispose();
        }
        private void InitializeComp()
        {
            this.sidebar = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.sidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.Controls.Add(this.listView1);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(94, 500);
            this.sidebar.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(94, 500);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(94, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 500);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // MainForm
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.sidebar);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "資料系統";
            this.sidebar.ResumeLayout(false);
            this.ResumeLayout(false);
    }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public void SIBEBARTESTFUN(Object sender, EventArgs e)
        {
            //
        }
        private void msyslogin_click(Object sender, EventArgs e)
        {
            List<SidebarItem> sidebarItems = new List<SidebarItem>();
            SidebarItem sidebaritem = new SidebarItem();
            sidebaritem.title = "item1";
            sidebaritem.subitems = new List<SidebarItem_Struct>();
            sidebaritem.subitems.Add(new SidebarItem_Struct("月份", "SIBEBARTESTFUN", 0));
            sidebaritem.subitems.Add(new SidebarItem_Struct("檢視", "SIBEBARTESTFUN", 0));
            sidebaritem.t = this;
            sidebaritem.atype = Type.GetType("Form1");
            sidebaritem.next = new List<SidebarItem>();
            SidebarItem subsi = new SidebarItem();
            subsi.title = "XLS匯入";
            subsi.subitems = new List<SidebarItem_Struct>();
            subsi.subitems.Add(new SidebarItem_Struct("固定", "SIBEBARTESTFUN", 0));
            subsi.subitems.Add(new SidebarItem_Struct("非固定", "SIBEBARTESTFUN", 0));
            subsi.t = this;
            subsi.atype = Type.GetType("Form1");
            sidebaritem.next.Add(subsi);
            sidebarItems.Add(sidebaritem);
            Sidebar<Form1> sidebar_binding = new Sidebar<Form1>(this, sidebar, listView1, imageList1, sidebarItems);
        }
        private void msyslogout_click(Object sender, EventArgs e)
        {
            Sidebar<Form1>.CleanSibebar(sidebar, listView1);
        }
        private void msysexit_click(Object sender, EventArgs e)
        {
            this.Close();
        }
        private void mnuIcons_click(Object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void mnuCascade_click(Object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }
        private void mnuTileHorizontal_click(Object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void mnuTileVertical_click(Object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }
        private void CloseAllSubForm_click(Object sender, EventArgs e)
        {
            for (int i = MdiChildren.Length - 1; i > -1; i--)
            {
                this.MdiChildren[i].Close();
            }
        }

    }
    class TLOG : ES_FORMS.Publib.ESLog
    {

        public TLOG()
            : base()
        {
        }
        public override void log(string message)
        { }
    }
}
