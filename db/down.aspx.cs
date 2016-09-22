using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace down2.db
{
    public partial class down : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = "网易云音乐.exe";//客户端保存的文件名
            string filePath = "d:\\网易云音乐.exe";//路径
            string ext = Path.GetExtension(fileName);
            string encodefileName = ToHexString(fileName);       //使用自定义的
            if (Request.Browser.Browser.Contains("IE"))
            {
                string name = encodefileName.Remove(encodefileName.Length - ext.Length);//得到文件名称
                name = name.Replace(".", "%2e"); //关键代码
                encodefileName = name + ext;
            }

            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(fileName);

            string fnUtf8 = Encoding.UTF8.GetString(Encoding.GetEncoding("gb2312").GetBytes(fileName));
            fnUtf8 = HttpUtility.UrlEncode(fileName);

            Stream iStream = null;
            try
            {
                // Open the file.
                iStream = new System.IO.FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                // Total bytes to read:
                long dataToRead = iStream.Length;

                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fnUtf8 + "\"");
                string range = Request.Headers.Get("Range");
                if (!string.IsNullOrEmpty(range))
                {
                    string[] rs = range.Split("-".ToCharArray());//bytes=10254
                    int posBegin = rs[0].IndexOf("=") + 1;
                    string pos = rs[0].Substring(posBegin);
                    iStream.Seek(long.Parse(pos), SeekOrigin.Begin);
                    dataToRead -= long.Parse(pos);//fix(2015-08-12):修复返回长度不正确的问题。
                }
                Response.AddHeader("Content-Length", dataToRead.ToString());

                byte[] buffer = new Byte[10000];
                int length;
                // Read the bytes.
                while (dataToRead > 0)
                {
                    // Verify that the client is connected.
                    if (Response.IsClientConnected)
                    {
                        // Read the data in buffer.
                        length = iStream.Read(buffer, 0, 10000);

                        // Write the data to the current output stream.
                        Response.OutputStream.Write(buffer, 0, length);

                        // Flush the data to the HTML output.
                        Response.Flush();

                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //prevent infinite loop if user disconnects
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trap the error, if any.
                Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    //Close the file.
                    iStream.Close();
                }
            }
        }
        
        /// <summary>
        /// 为字符串中的非英文字符编码Encodes non-US-ASCII characters in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        ///指定一个字符是否应该被编码 Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";
            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;
            return true;
        }
        /// <summary>
        /// 为非英文字符串编码Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }
    }
}