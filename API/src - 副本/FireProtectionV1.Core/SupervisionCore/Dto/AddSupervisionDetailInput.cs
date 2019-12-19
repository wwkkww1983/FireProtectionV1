using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class AddSupervisionDetailInput : SupervisionDetail
    {
        public string Remark { get; set; }
    }
}
