using AutoMapper;
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Account.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Account.Dto
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<FireUnitAccountInput, FireUnitUser>();
        }

    }
}
