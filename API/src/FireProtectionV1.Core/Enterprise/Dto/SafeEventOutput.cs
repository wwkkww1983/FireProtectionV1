using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class SafeEventOutput
    {
        public List<FireUnitSafe> FireUnits { get; set; }
    }
    public class FireUnitSafe
    {
        public string FireUnitId { get; set; }
        public string FireUnitName { get; set; }
        public bool HaveSafeEvent { get; set; }
    }
}
