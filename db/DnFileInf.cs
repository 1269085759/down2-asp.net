using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace down2.db
{
    public class DnFileInf
    {
        public int idSvr = 0;
        public int uid = 0;
        public string mac = string.Empty;
        public string nameLoc = string.Empty;
        public string pathLoc = string.Empty;
        public string fileUrl = string.Empty;
        public long lenLoc = 0;
        public long lenSvr = 0;
        public string sizeSvr = string.Empty;
        public string perLoc = string.Empty;
    }
}