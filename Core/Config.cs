using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public static class Config
    {
        public static readonly bool Debug = false;
        public static bool AddSigns { get; set; } = false;
        public static string SubtitleFolder { get; set; } = Directory.GetCurrentDirectory();
    }
}
