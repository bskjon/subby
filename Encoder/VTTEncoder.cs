using Encoder.Interfaces;
using Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder
{
    public class VTTEncoder : Encode, IBaseEncoder
    {
        public string[] GetSubtitle()
        {
            List<string> lines = new List<string>();
            lines.Add("WEBVTT\n\n");

            Timing timing = new Timing(0);

            foreach (Dialog dialog in dialogs)
            {
                string startTime = new Timing(dialog.startTime).GetVttTime();
                string stopTime = new Timing(dialog.endTime).GetVttTime();

                StringBuilder str = new StringBuilder();

                //str.AppendLine((dialogs.IndexOf(dialog) + 1).ToString());
                str.Append(startTime).Append(" --> ").Append(stopTime).AppendLine();
                str.AppendLine(dialog.text);
                str.AppendLine();
                lines.Add(str.ToString());
            }

            return lines.ToArray();
        }

        public override void Load(List<Dialog> dialogs)
        {
            base.dialogs = dialogs;
            AppendEncoderLine();
        }
    }
}
