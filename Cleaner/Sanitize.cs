using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Cleaner
{
    public class Sanitize
    {
        public static Regex onlyNumber = new Regex("^[0-9]+$");
        public static Regex timeArrow = new Regex("-->");

        readonly Regex curlyBraces = new Regex(@"[{](?<={).*?(?=})[}]");
        readonly Regex tags = new Regex(@"[<](?<=<).*?(?=>)[>]");
        readonly Regex regexIllegalChars = new Regex(@"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000\x10FFFF]");
        readonly Regex extraSpaces = new Regex(@"\s\s+");
        readonly Regex stripLinebreak = new Regex(@"(?i)\\N");

        readonly Regex validText = new Regex(@"(?i)[^\p{L}.:`'´#0-9]+");
        readonly Regex onlyText = new Regex(@"(?i)[\p{L}]+");
        readonly Regex required = new Regex(@"[\p{L}.:]"); // Required minimum. If there is only numbers, dissalow
        readonly Regex anyNumer = new Regex(@"[0-9]");
        readonly Regex spaced = new Regex(@"[\s]");


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
                    string rline = GetValidText(line);
                    builder.AppendLine(rline);
                }
                text = builder.ToString();
            }
            else
            {
                text = GetValidText(text);
            }
            text = text.Trim();


            return extraSpaces.Replace(text, " ");
        }

        private string GetValidText(string inputText)
        {
            string text = validText.Replace(inputText, " ");
            if (onlyText.Matches(text).Count > 1)
            {
                return text;
            }
            else if (required.Matches(text).Count >= 1 && spaced.Matches(text).Count <= 4 )
            {
                return text;
            }

            return "";
        }

        public string[] GetLines(string text)
        {
            return stripLinebreak.Split(text);
        }
    }
}
