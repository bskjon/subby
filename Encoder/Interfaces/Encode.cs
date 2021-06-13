using Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encoder.Interfaces
{
    public abstract class Encode: IEncode
    {
        private readonly string plug = "Made as a part of the StreamIT project";
        protected IList<Dialog> dialogs = new List<Dialog>();

        public abstract void Load(List<Dialog> dialogs);

        /// <summary>
        /// Method will append encoder line to the start of the subtitle if there is any available timeslot
        /// </summary>
        protected void AppendEncoderLine()
        {
            if (dialogs.Count > 0 && CanShamelesslyInjectEncoder(dialogs.ElementAt(0)))
            {
                Dialog shamelessPlug = new Dialog
                {
                    startTime = 0,
                    endTime = GetShamelessPlugEnd(dialogs.ElementAt(0)),
                    text = plug
                };
                dialogs.Insert(0, shamelessPlug);
            }
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
