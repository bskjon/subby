using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Cleaner
{
    public class Sanitize
    {
        readonly Regex curlyBraces = new Regex(@"[{](?<={).*?(?=})[}]");
        readonly Regex tags = new Regex(@"[<](?<=<).*?(?=>)[>]");
        readonly Regex regexIllegalChars = new Regex(@"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000\x10FFFF]");
        readonly Regex extraSpaces = new Regex(@"\s\s+");
        readonly Regex stripLinebreak = new Regex(@"(?i)\\N");

        readonly Regex validText = new Regex(@"(?i)[^.,'`´0-9:A-Z !?+-;*&%$@#§|]+");


        public string GetSanitized(string text)
        {
            text = curlyBraces.Replace(text, " ");//   Regex.Replace(text, cbraces, " ");
            text = tags.Replace(text, " ");// Regex.Replace(text, regexTags, " ");
            text = regexIllegalChars.Replace(text, " "); // Regex.Replace(text, regexIllegalChars, " ");

            if (stripLinebreak.IsMatch(text))
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = stripLinebreak.Split(text);
                foreach (string line in lines)
                {
                    string rline = validText.Replace(line, " ");
                    builder.AppendLine(rline);
                }
                text = builder.ToString();
            }
            else
            {
                text = validText.Replace(text, " ");
            }
            text = text.Trim();


            return extraSpaces.Replace(text, " ");
        }

        public string[] GetLines(string text)
        {
            return stripLinebreak.Split(text);
        }
    }
}
