using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.VersionCore.Model
{
    public class Suggest : EntityBase
    {
        public string suggest { get; set; }
    }
}
