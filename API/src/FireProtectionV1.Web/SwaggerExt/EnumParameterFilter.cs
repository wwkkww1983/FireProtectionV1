using FireProtectionV1.Common.DBContext;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.SwaggerExt
{
    public class EnumParameterFilter : IParameterFilter
    {
        public void Apply(IParameter parameter, ParameterFilterContext context)
        {
            var type = context.ApiParameterDescription.Type;
            var IsEnum = type.IsEnum;
            if (!IsEnum && type.IsGenericType && (type.GetGenericArguments().FirstOrDefault()?.IsEnum == true))
            {
                IsEnum = true;
            }

            if (IsEnum && 0 < type.GetCustomAttributes(typeof(ExportAttribute), true).Length)
            {
                var Description = EnumTool.getDescription(type);
                Description = parameter.Description + Description;
                parameter.Extensions.Add("MyDescription", Description);

                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("$ref", $"#/definitions/{type.Name}");
                parameter.Extensions.Add("schema", keyValuePairs);
            }
        }
    }
}
