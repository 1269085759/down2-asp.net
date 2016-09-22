using System;

namespace down2.db
{
    public partial class d_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fid = Request.QueryString["idSvr"];
            string uid = Request.QueryString["uid"];
            string per = Request.QueryString["perLoc"];
            string lenLoc = Request.QueryString["lenLoc"];
            string cbk = Request.QueryString["callback"];

            if (string.IsNullOrEmpty(uid)
                || string.IsNullOrEmpty(fid)
                || string.IsNullOrEmpty(cbk)
                || string.IsNullOrEmpty(lenLoc))
            {
                Response.Write(cbk+"({\"value\":null})");
                Response.End();
                return;
            }

            DnFile db = new DnFile();
            db.updateProcess(int.Parse(fid), int.Parse(uid), lenLoc, per);
            Response.Write(cbk + "({\"value\":1})");
        }
    }
}