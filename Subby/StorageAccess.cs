using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Subby
{
    public class StorageAccess
    {
        public class ReadFile
        {
            public string[] GetLines(string file)
            {
                return File.ReadAllLines(file);
            }
        }

        public class WriteFile
        {
            public void WriteSrt(string file, string[] lines)
            {
                if (!IsExcpectedFormat(file, "srt"))
                {
                    file = GetFilePathWithFormat(file, "srt");
                }

                File.WriteAllLines(file, lines);
            }

            public void WriteVtt(string file, string[] lines)
            {
                if (!IsExcpectedFormat(file, "vtt"))
                {
                    file = GetFilePathWithFormat(file, "vtt");
                }

                File.WriteAllLines(file, lines);
            }

            public void WriteSmi(string file, XmlDocument document)
            {
                if (!IsExcpectedFormat(file, "smi"))
                {
                    file = GetFilePathWithFormat(file, "smi");
                }
                document.Save(file);
            }


            private bool IsExcpectedFormat(string file, string format)
            {
                return file.ToLower().EndsWith(format);
            }

            private string GetFilePathWithFormat(string file, string format)
            {
                StringBuilder str = new StringBuilder();
                str.Append(file.Substring(0, file.LastIndexOf(".") + 1));
                str.Append(format);
                return str.ToString();
            }

        }

        public string[] getAllFiles()
        {
            return Directory.GetFiles(Program.SubtitleFolder);
        }

        public string[] GetAllAssFiles()
        {
            return Directory.EnumerateFiles(Program.SubtitleFolder).Where(
                file => file.ToLower().EndsWith("ass") || file.ToLower().EndsWith("ssa")
                ).ToArray();
        }

        public string[] GetAllSrtFiles()
        {
            return Directory.EnumerateFiles(Program.SubtitleFolder).Where(
                file => file.ToLower().EndsWith("srt")
                ).ToArray();
        }

        public string[] GetAllVttFiles()
        {
            return Directory.EnumerateFiles(Program.SubtitleFolder).Where(
                file => file.ToLower().EndsWith("vtt")
                ).ToArray();
        }

        public string[] GetAllSmiFiles()
        {
            return Directory.EnumerateFiles(Program.SubtitleFolder).Where(
                file => file.ToLower().EndsWith("smi")
                ).ToArray();
        }

    }
}
