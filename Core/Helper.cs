using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public class Helper
    {
        public static Types.SubtitleFormat GetSubtitleFormat(string input)
        {
            if (input.Contains(".") || input.Contains("/") || input.Contains("\\"))
            {
                input = Path.GetExtension(input).Trim('.');
            }
            switch(input)
            {
                case "srt":
                    return Types.SubtitleFormat.SRT;
                case "ass":
                case "ssa":
                    return Types.SubtitleFormat.ASS;
                case "vtt":
                    return Types.SubtitleFormat.VTT;
                case "smi":
                case "sami":
                    return Types.SubtitleFormat.SMI;
                default:
                    return Types.SubtitleFormat.NOT_SET;
            }
        }

        public static string GetOutputFile(string OutPath, string file)
        {
            string filename = null;
            if (file.Contains("/") || file.Contains("\\"))
            {
                filename = Path.GetFileName(file);
            }
            else
            {
                filename = file;
            }

            return Path.Combine(OutPath, filename);

        }

        public static List<Types.SubtitleFormat> GetExportFormats(Types.SubtitleFormat configured)
        {
            List<Types.SubtitleFormat> formats = new List<Types.SubtitleFormat>();
            if (configured != Types.SubtitleFormat.NOT_SET)
            {
                formats.Add(configured);
            }
            else
            {
                formats.Add(Types.SubtitleFormat.SRT);
                formats.Add(Types.SubtitleFormat.VTT);
                formats.Add(Types.SubtitleFormat.SMI);
            }

            return formats;
        }

    }
}
