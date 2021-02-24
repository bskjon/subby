using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Storage
{
    public class StorageAccess
    {
        protected string getCurrentDirectory()
        {
            return @"C:\Users\Brage\Sub test";
            //return Directory.GetCurrentDirectory();
        }

        public string[] getAllFiles()
        {
            return Directory.GetFiles(getCurrentDirectory());
        }

        public string[] getAllAssFiles()
        {
            return Directory.EnumerateFiles(getCurrentDirectory()).Where(
                file => file.ToLower().EndsWith("ass") || file.ToLower().EndsWith("ssa")
                ).ToArray();
        }

        public string[] getAllSrtFiles()
        {
            return Directory.EnumerateFiles(getCurrentDirectory()).Where(
                file => file.ToLower().EndsWith("srt")
                ).ToArray();
        }

        public string[] getAllVttFiles()
        {
            return Directory.EnumerateFiles(getCurrentDirectory()).Where(
                file => file.ToLower().EndsWith("vtt")
                ).ToArray();
        }

        public string[] getAllSmiFiles()
        {
            return Directory.EnumerateFiles(getCurrentDirectory()).Where(
                file => file.ToLower().EndsWith("smi")
                ).ToArray();
        }



    }
}
