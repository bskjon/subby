using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder.Reader
{
    interface ISubtitleReader
    {

        IList<Dialog> GetDialogs();

    }
}
