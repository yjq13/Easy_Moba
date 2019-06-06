using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class,
AllowMultiple = true)]
    public class UIResourceAttribute : Attribute
    {
        public string ResID { get; private set; }

        public UIResourceAttribute(string resID)
        {
            this.ResID = @"UI/" + resID;
        }
    }
}
