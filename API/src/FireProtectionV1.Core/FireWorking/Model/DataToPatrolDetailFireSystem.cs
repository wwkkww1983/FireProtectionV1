using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class DataToPatrolDetailFireSystem : EntityBase
    {
        public int PatrolDetailId { get; set; }
        public int FireSystemID { get; set; }
    }
}
