using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storage
{
    public class FileRead : StorageAccess
    {
        private string[] files;

        public string[] GetContent(string file)
        {
            return File.ReadAllLines(file);
        }


    }
}
