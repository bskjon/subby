using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Encoder;
using Modals;

namespace Core.Reader
{
    public class AssSubtitleReader : SubtitleReader
    {
        public override void Load(string[] data)
        {
            List<Dialog> dialogs = GetDialogFromLines(GetDialogLines(data));

            if (Config.AddSigns)
            {
                List<Dialog> signs = GetSignFromLines(GetSignLines(data));
                dialogs.AddRange(signs);
            }

            base.SetDialogs(dialogs);
        }


        protected List<Dialog> GetDialogFromLines(IList<string> lines)
        {
            List<Dialog> dialogs = new List<Dialog>();
            foreach (string line in lines)
            {
                if (!IsDialogEntry(line))
                    continue;

                string[] data = GetDialogDataFromLine(line);

                dialogs.Add
                    (
                        new Dialog
                        {
                            startTime = new Timing(data[0]).getMilliseconds(),
                            endTime = new Timing(data[1]).getMilliseconds(),
                            text = data[2]
                        }
                    );
            }
            return dialogs;
        }

        protected List<Dialog> GetSignFromLines(IList<string> lines)
        {
            List<Dialog> signs = GetDialogFromLines(lines);
            foreach (Dialog sign in signs)
            {
                sign.isDialog = false;
            }
            return signs;

        }

        public string[] GetDialogDataFromLine(string line)
        {
            string[] dlines = line.Split(new[] { "," }, StringSplitOptions.None);

            string startTime = dlines[1];
            string endTime = dlines[2];

            // Skip was set at 8 before
            // Changed to 9 since it also god the effect...
            string text = string.Join(",", dlines.Skip(9)).Trim(" , ".ToCharArray());

            string[] data = new string[3];
            data[0] = startTime;
            data[1] = endTime;
            data[2] = text;
            return data;
        }

        private string GetDialogKey(string line)
        {
            string[] splints = line.Split(',');
            string style = (splints.Length >= 4) ? splints[3] : "";
            return style;
        }

        protected IList<string> GetDialogLines(string[] lines)
        {
            IList<string> dialogLines = new List<string>();
            foreach (string line in lines)
            {
                
                if (IsDialog(GetDialogKey(line), line))
                {
                    if (Config.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[Idify] Dialog: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(string.Format("{0}", line));
                    }
                    dialogLines.Add(line);
                }
                else
                {
                    if (Config.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[Idify] NonDialog: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(string.Format("{0}", line));
                    }
                }
            }
            return dialogLines;
        }

        protected IList<string> GetSignLines(string[] lines)
        {
            IList<string> signLines = new List<string>();
            foreach (string line in lines)
            {
                if (IsSongOrSign(GetDialogKey(line), line))
                {
                    signLines.Add(line);
                }
            }
            return signLines;
        }

        private bool IsDialogEntry(string text)
        {
            if (text.Contains(":"))
                return text.Substring(0, text.IndexOf(":")).ToLower().Equals("dialogue");
            return false;
        }

    }
}
