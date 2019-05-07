using AutoMapper;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<FireUnitUserInput, FireUnitUser>();
        }

    }
}
