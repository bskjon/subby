using Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder.Interfaces
{
    interface IBaseEncoder
    {
        void Load(List<Dialog> dialogs);
        string[] GetSubtitle();
    }
}
