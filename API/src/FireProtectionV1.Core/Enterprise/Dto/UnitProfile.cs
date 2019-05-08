using AutoMapper;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            CreateMap<AddSafeUnitInput, SafeUnit>();
            CreateMap<UpdateSafeUnitInput, SafeUnit>();
        }
    }
}
