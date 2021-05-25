using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public static class Config
    {
#if DEBUG
        public static bool Debug = true;
#else
        public static bool Debug = false;
#endif
        public static bool AddSigns { get; set; } = false;
        public static string SubtitleFolder { get; set; } = Directory.GetCurrentDirectory();
    }
}
