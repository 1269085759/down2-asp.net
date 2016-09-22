using Newtonsoft.Json;
using System;
using System.Web;

namespace down2.db
{
    public partial class d_create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"];
            string cbk = Request.QueryString["callback"];//应用于jsonp数据
            string fileLoc = Request.QueryString["file"];
            fileLoc = fileLoc.Replace("+", "%20");
            fileLoc = HttpUtility.UrlDecode(fileLoc);

            System.Diagnostics.Debug.WriteLine("uid:" + uid);
            System.Diagnostics.Debug.WriteLine("fileLoc:" + fileLoc);

            if (string.IsNullOrEmpty(uid))
            {
                Response.Write(cbk+"({\"value\":null})");
                Response.End();
                return;
            }

            DnFileInf inf = JsonConvert.DeserializeObject<DnFileInf>(fileLoc);
            DnFile db = new DnFile();
            db.Add(ref inf);

            string json = JsonConvert.SerializeObject(inf);
            json = HttpUtility.UrlEncode(json);
            json = json.Replace("+", "%20");
            json = cbk + "({\"value\":\""+json+"\"})";//返回jsonp格式数据。
            Response.Write(json);
        }
    }
}