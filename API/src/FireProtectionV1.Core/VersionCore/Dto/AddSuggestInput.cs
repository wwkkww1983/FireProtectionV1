using FireProtectionV1.VersionCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.VersionCore.Dto
{
    public class AddSuggestInput
    {
        [Required]
        public string Suggest { get; set; }
    }
}
