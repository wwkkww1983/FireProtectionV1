using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.SwaggerExt
{
    public class EnumSchema : Schema
    {
        public Type EnumType { set; get; }
    }
}
