using System;
using System.Collections.Generic;
using System.Text;
using static Core.Types;

namespace Modals
{
    public class RunOption
    {
        public OptionTypes Switch { set; get; } = OptionTypes.NOT_SET;
        public object Option { get; set; } = null;
    }
}
