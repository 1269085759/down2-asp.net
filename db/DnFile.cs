using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace down2.db
{
    public class DnFile
    {
        public int Add(ref DnFileInf inf)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into down_files(");
            sql.Append(" f_uid");
            sql.Append(",f_nameLoc");
            sql.Append(",f_pathLoc");
            sql.Append(",f_fileUrl");
            sql.Append(",f_lenSvr");
            sql.Append(",f_sizeSvr");
            sql.Append(") values(");
            sql.Append(" @f_uid");
            sql.Append(",@f_nameLoc");
            sql.Append(",@f_pathLoc");
            sql.Append(",@f_fileUrl");
            sql.Append(",@f_lenSvr");
            sql.Append(",@f_sizeSvr");
            sql.Append(");");
            sql.Append("select @@IDENTITY");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql.ToString());
            db.AddInt(ref cmd,"@f_uid", inf.uid);
            db.AddString(ref cmd, "@f_nameLoc", inf.nameLoc,255);
            db.AddString(ref cmd, "@f_pathLoc", inf.pathLoc,255);
            db.AddString(ref cmd, "@f_fileUrl", inf.fileUrl, 255);
            db.AddInt64(ref cmd, "@f_lenSvr", inf.lenSvr);
            db.AddString(ref cmd, "@f_sizeSvr", inf.sizeSvr, 19);
            var obj = db.ExecuteScalar(cmd);
            inf.idSvr = int.Parse(obj.ToString());
            return inf.idSvr;
        }

        /// <summary>
        /// 将文件设为已完成
        /// </summary>
        /// <param name="fid"></param>
        public void Complete(int fid)
        {
            string sql = "update down_files set f_complete=1 where f_id=@f_id;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@f_id", fid);
            db.ExecuteNonQuery(ref cmd);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fid"></param>
        public void Delete(int fid,int uid)
        {
            string sql = "delete from down_files where f_id=@f_id and f_uid=@f_uid";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@f_id", fid);
            db.AddInt(ref cmd, "@f_uid", uid);
            db.ExecuteNonQuery(ref cmd);
        }

        public void updateProcess(int fid, int uid, string lenLoc, string perLoc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update down_files set");
            sb.Append(" f_lenLoc=@f_lenLoc");
            sb.Append(",f_perLoc=@f_perLoc");
            sb.Append(" where");
            sb.Append(" f_id =@f_id and f_uid=@f_uid;");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddInt64(ref cmd, "@f_lenLoc",long.Parse(lenLoc));
            db.AddString(ref cmd, "@f_perLoc", perLoc, 6);
            db.AddInt(ref cmd, "@f_id", fid);
            db.AddInt(ref cmd, "@f_uid", uid);
            db.ExecuteNonQuery(ref cmd);
        }

        /// <summary>
        /// 获取所有未完成的文件列表
        /// </summary>
        /// <returns></returns>
        public string GetAll(int uid)
        {
            string sql = "select * from down_files where f_uid=@f_uid and f_complete=0;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@f_uid", uid);

            DbDataReader r = db.ExecuteReader(ref cmd);
            List<DnFileInf> files = new List<DnFileInf>();
            while (r.Read())
            {
                DnFileInf f = new DnFileInf();
                f.idSvr = r.GetInt32(0);
                f.nameLoc = r.GetString(3);
                f.pathLoc = r.GetString(4);
                f.fileUrl = r.GetString(5);
                f.perLoc = r.GetString(6);
                f.lenLoc = r.GetInt64(7);
                f.lenSvr = r.GetInt64(8);
                f.sizeSvr = r.GetString(9);
                files.Add(f);
            }
            r.Close();

            string json = JsonConvert.SerializeObject(files);
            return json;
        }

        static public void Clear()
        {
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand("delete from down_files;");
            db.ExecuteNonQuery(ref cmd);
        }
    }
}