using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace down2.db
{
    public class DnFileInf
    {
        public DnFileInf()
        { }

        public int idSvr { get { return this.m_fid; } set { this.m_fid = value; } }
        public int uid { get { return this.m_uid; } set { this.m_uid = value; } }
        public string mac { get { return this.m_mac; } set { this.m_mac = value; } }
        public string nameLoc { get { return this.m_nameLoc; } set { this.m_nameLoc = value; } }
        public string pathLoc { get { return this.m_pathLoc; } set { this.m_pathLoc = value; } }
        public string fileUrl { get { return this.m_fileUrl; } set { this.m_fileUrl = value; } }
        public long lenLoc { get { return this.m_lenLoc; } set { this.m_lenLoc = value; } }
        public long lenSvr { get { return this.m_lenSvr; } set { this.m_lenSvr = value; } }
        public string sizeSvr { get { return this.m_sizeSvr; } set { this.m_sizeSvr = value; } }
        public string perLoc { get { return this.m_perLoc; } set { this.m_perLoc = value; } }

        [JsonIgnore]
        private int m_fid;

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonIgnore]
        private int m_uid;

        /// <summary>
        /// MAC地址
        /// </summary>
        [JsonIgnore]
        private string m_mac;

        private string m_nameLoc;
        /// <summary>
        /// 本地文件路径
        /// </summary>
        [JsonIgnore]
        private string m_pathLoc;

        /// <summary>
        /// 服务器文件路径
        /// </summary>
        [JsonIgnore]
        private string m_fileUrl;

        /// <summary>
        /// 本地文件长度
        /// </summary>
        [JsonIgnore]
        private long m_lenLoc;

        /// <summary>
        /// 服务器文件长度
        /// </summary>
        [JsonIgnore]
        private long m_lenSvr;

        /// <summary>
        /// 传输进度
        /// </summary>
        [JsonIgnore]
        private string m_perLoc;
        private string m_sizeSvr;
    }
}