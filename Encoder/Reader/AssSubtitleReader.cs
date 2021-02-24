using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encoder.Reader
{
    public class AssSubtitleReader : SubtitleReader
    {
        public AssSubtitleReader(string[] lines)
        {
            IList<string> dialogeLines = GetDialogeLines(lines);
            base.sanitize(dialogeLines);


            foreach (string dline in dialogeLines)
            {
                if (!base.isDialog(dline))
                    continue;

                base.dialogs.Add(GetDialog(dline));
            }
        }
        private Dialog GetDialog(string line)
        {
            string[] dlines = line.Split(new[] { "," }, StringSplitOptions.None);
            string startTime = dlines[1];
            string endTime = dlines[2];

            string text = string.Join(",", dlines.Skip(8)).Trim(" , ".ToCharArray());


            Dialog dialog = new Dialog
            {
                startTime = new Timing(startTime).getMilliseconds(),
                endTime = new Timing(endTime).getMilliseconds(),
                text = text
            };


            return dialog;
        }


        private IList<string> GetDialogeLines(string[] lines)
        {
            IList<string> dialogeLines = new List<string>();
            foreach (string line in lines)
            {
                if (IsDialogLine(line))
                {
                    dialogeLines.Add(line.Substring(line.IndexOf(":")));
                }
            }
            return dialogeLines;
        }

        private bool IsDialogLine(string text)
        {
            if (text.Contains(":"))
                return text.Substring(0, text.IndexOf(":")).ToLower().Equals("dialogue");
            return false;
        }


    }
}
