using Encoder.Reader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Encoder
{
    public class SubtitleReader : ISubtitleReader
    {
        private Regex regex = new Regex("(?<=,)(?i)sign");
        protected IList<Dialog> dialogs = new List<Dialog>();

        protected void sanitize(IList<string> vs)
        {
            Sanitize sanitizeer = new Sanitize();
            for (int i = 0; i < vs.Count; i++) // (string s in vs)
            {
                vs[i] = sanitizeer.sanitize(vs[i]);
            }
        }


        protected bool isDialog(string line)
        {
            return !regex.IsMatch(line);
        }


        public IList<Dialog> GetDialogs()
        {
            return dialogs;
        }
    }
}
