using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class UpdateHydrantInput : AddHydrantInput
    {
        public int Id { get; set; }
    }
}
