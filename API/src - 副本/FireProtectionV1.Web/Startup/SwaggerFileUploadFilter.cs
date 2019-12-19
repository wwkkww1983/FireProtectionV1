using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.Startup
{
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        private static readonly string[] FormFilePropertyNames =
        typeof(IFormFile).GetTypeInfo().DeclaredProperties.Select(x => x.Name).ToArray();
        public void Apply(Operation operation, OperationFilterContext context)
        {

            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
               !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var fileParameters = context.ApiDescription.ActionDescriptor.Parameters.Where(n => n.ParameterType == typeof(IFormFile)).ToList();

            if (fileParameters.Count < 0)
            {
                return;
            }

            foreach (var fileParameter in fileParameters)
            {
                var formFileParameters = operation
                .Parameters
                .OfType<NonBodyParameter>()
                .Where(x => FormFilePropertyNames.Contains(x.Name))
                .ToArray();
                foreach (var formFileParameter in formFileParameters)
                {
                    operation.Parameters.Remove(formFileParameter);
                }

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = fileParameter.Name,
                    In = "formData",
                    Description = "图片上传",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");


            }
        }
    }
}
