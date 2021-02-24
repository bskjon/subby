using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder.Interfaces
{
    interface IEncode
    {

        string[] GetSrtEncoded(List<Dialog> dialogs);
        string[] GetVttEncoded(List<Dialog> dialogs);
        string[] GetSMIEncoded(List<Dialog> dialogs);

    }
}
