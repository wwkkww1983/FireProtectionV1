using AutoMapper;
using FireProtectionV1.VersionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.VersionCore.Dto
{
    public class VersionProfile: Profile
    {
        public VersionProfile()
        {
            CreateMap<AddSuggestInput, Suggest>();
        }        
    }
}
