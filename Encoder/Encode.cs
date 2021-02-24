using Encoder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encoder
{
    public class Encode : IEncode
    {
        private readonly string plug = "Made as a part of the StreamIT project";

        public string[] GetSMIEncoded(List<Dialog> dialogs)
        {
            List<string> lines = new List<string>();

            foreach (Dialog dialog in dialogs)
            {

            }

            return lines.ToArray();
        }

        public string[] GetSrtEncoded(List<Dialog> dialogs)
        {
            List<string> lines = new List<string>();

            if (CanShamelesslyInjectEncoder(dialogs.ElementAt(0)))
            {
                Dialog shamelessPlug = new Dialog
                {
                    startTime = 0,
                    endTime = GetShamelessPlugEnd(dialogs.ElementAt(0)),
                    text = plug

                };
                dialogs.Insert(0, shamelessPlug);
            }

            Timing timing = new Timing(0);

            foreach (Dialog dialog in dialogs)
            {
                string startTime = new Timing(dialog.startTime).GetSrtTime();
                string stopTime = new Timing(dialog.endTime).GetSrtTime();
            
                StringBuilder str = new StringBuilder();

                str.AppendLine((dialogs.IndexOf(dialog) + 1).ToString());
                str.Append(startTime).Append(" --> ").Append(stopTime).AppendLine();
                str.AppendLine(dialog.text);
                str.AppendLine();
                lines.Add(str.ToString());
            }

            return lines.ToArray();
        }

        public string[] GetVttEncoded(List<Dialog> dialogs)
        {
            List<string> lines = new List<string>();

            foreach (Dialog dialog in dialogs)
            {

            }

            return lines.ToArray();
        }


        private bool CanShamelesslyInjectEncoder(Dialog firstDialog)
        {
            return (firstDialog.startTime - 1000 > 500);
        }

        private long GetShamelessPlugEnd(Dialog firstDialog)
        {
            return (firstDialog.startTime > 2000) ? 2000 : firstDialog.startTime;
        }


    }
}
