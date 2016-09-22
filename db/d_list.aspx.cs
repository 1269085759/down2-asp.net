using System;
using System.Web;

namespace down2.db
{
    public partial class d_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"];
            string cbk = Request.QueryString["callback"];

            if (string.IsNullOrEmpty(uid))
            {
                Response.Write(cbk + "({\"value\":null})");
                Response.End();
                return;
            }

            DnFile db = new DnFile();
            string json = db.GetAll(int.Parse(uid));
            json = HttpUtility.UrlEncode(json);//urlencode会将空格转换为+号
            json = json.Replace("+", "%20");

            Response.Write(cbk + "({\"value\":\""+json+"\"})");
        }
    }
}