using AutoMapper;
using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class MiniFireStationProfile : Profile
    {
        public MiniFireStationProfile()
        {
            CreateMap<AddMiniFireStationInput, MiniFireStation>();
        }
    }
}
