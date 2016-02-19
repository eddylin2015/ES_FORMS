"# ES_FORMS"

public Form1()
{
  string ConnStr = "Driver={{MySQL ODBC 5.1 Driver}};Server={0};Database={1};UID={2};PWD={3};OPTION=67108867";//optoin=3
  String _conn_txt = string.Format(ConnStr, "127.0.0.1", "db", "u", "p");
  conn = new OdbcConnection(_conn_txt);
  conn.Open();
  ES_FORMS.STFORMS.Form_Search fs = new ES_FORMS.STFORMS.Form_Search_Studinfo(conn, this);
  fs.Show();
}
private OdbcConnection conn = null;
private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    conn.Close();
    conn.Dispose();
}
