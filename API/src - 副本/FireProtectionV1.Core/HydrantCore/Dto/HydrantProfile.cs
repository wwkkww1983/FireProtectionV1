using AutoMapper;
using FireProtectionV1.HydrantCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class HydrantProfile : Profile
    {
        public HydrantProfile()
        {
            CreateMap<AddHydrantInput, Hydrant>();
            CreateMap<UpdateHydrantInput, Hydrant>();
        }
    }
}
