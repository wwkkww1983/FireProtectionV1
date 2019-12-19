using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.DBContext
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class ExportAttribute : Attribute
    {
        public string Name { get; set; }

        public ExportAttribute(string name)
        {
            this.Name = name;
        }
    }
}
