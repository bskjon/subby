using System;
using System.Collections.Generic;
using System.Text;

namespace Modals
{
    public class Dialog
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public string text { get; set; }

        public bool isDialog { get; set; } = true;

    }
}
