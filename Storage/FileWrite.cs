using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storage
{
    public class FileWrite
    {

        public FileWrite()
        {

        }


        public void WriteSrt(string file, string[] lines)
        {
            if (!IsExcpectedFormat(file, "srt"))
            {
                file = GetFilePathWithFormat(file, "srt");
            }

            File.WriteAllLines(file, lines);
        }


        private bool IsExcpectedFormat(string file, string format)
        {
            return file.ToLower().EndsWith(format);
        }

        private string GetFilePathWithFormat(string file, string format)
        {
            StringBuilder str = new StringBuilder();
            str.Append(file.Substring(0, file.LastIndexOf(".")+1));
            str.Append(format);
            return str.ToString();
        }

    }
}
