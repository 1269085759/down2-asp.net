using System;

namespace down2.db
{
    public partial class d_del : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fid = Request.QueryString["fid"];
            string uid = Request.QueryString["uid"];
            string cbk = Request.QueryString["callback"];

            if (string.IsNullOrEmpty(uid)
                || string.IsNullOrEmpty(fid)
                )
            {
                Response.Write(cbk + "({\"value\":0})");
                return;
            }

            DnFile db = new DnFile();
            db.Delete(int.Parse(fid), int.Parse(uid));

            Response.Write(cbk + "({\"value\":1})");
        }
    }
}