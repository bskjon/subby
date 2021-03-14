using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Encoder.Interfaces
{
    interface ISMIEncoder
    {
        XmlDocument GetSubtitle(string title);
    }
}
