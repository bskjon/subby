using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Encoder
{


    class Sanitize
    {
        readonly Regex cbraces = new Regex(@"[{](?<={).*?(?=})[}]");
        readonly Regex regexTags = new Regex(@"[<](?<=<).*?(?=>)[>]");
        readonly Regex regexIllegalChars = new Regex(@"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000\x10FFFF]");
        readonly Regex extraSpaces = new Regex(@"\s\s+");


        public string sanitize(string text)
        {
            text = cbraces.Replace(text, " ");//   Regex.Replace(text, cbraces, " ");
            text = regexTags.Replace(text, " ");// Regex.Replace(text, regexTags, " ");
            text = regexIllegalChars.Replace(text, " "); // Regex.Replace(text, regexIllegalChars, " ");
            text = extraSpaces.Replace(text, " ");
            return text;
        }
    }
}
