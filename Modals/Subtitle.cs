using System;
using System.Collections.Generic;
using System.Text;

namespace Modals
{
    public class Subtitle
    {
        public string file { get; private set; }
        public IList<Dialog> dialogs { get; private set; }

        public Subtitle(string file, IList<Dialog> dialogs)
        {
            this.file = file;
            this.dialogs = dialogs;
        }

    }
}
