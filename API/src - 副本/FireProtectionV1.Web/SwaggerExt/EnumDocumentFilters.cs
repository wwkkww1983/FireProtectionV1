using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.SwaggerExt
{
    public class EnumDocumentInfo
    {
        public string RefName { set; get; }
        public string Description { set; get; }
    }

    public class EnumDocumentFilters : IDocumentFilter
    {
        private List<Type> types = new List<Type>();
        private List<EnumDocumentInfo> enumDocumentInfos = new List<EnumDocumentInfo>();

        public EnumDocumentFilters(Assembly[] assemblies)
        {
            this.types = EnumTool.GetTypes(assemblies);
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            types.ForEach(type =>
            {
                var schemaObj = swaggerDoc.Definitions.Where(c => c.Key == type.Name).FirstOrDefault();
                if (schemaObj.Value != null)
                {
                    var schema = schemaObj.Value;
                    schema.Description += EnumTool.getDescription(type);

                    enumDocumentInfos.Add(new EnumDocumentInfo()
                    {
                        RefName = $"#/definitions/{type.Name}",
                        Description = schema.Description
                    });
                }
            });

            foreach (KeyValuePair<string, Schema> schemaDictionaryItem in swaggerDoc.Definitions)
            {
                Schema schema = schemaDictionaryItem.Value;
                if (schema.Extensions.Count > 0)
                {

                }
                if (schema != null)
                    DescribeEnumProperties(schema.Properties);
            }

            if (swaggerDoc.Paths.Count > 0)
            {
                foreach (PathItem pathItem in swaggerDoc.Paths.Values)
                {
                    DescribeEnumParameters(pathItem.Parameters);

                    List<Operation> possibleParameterisedOperations = new List<Operation> { pathItem.Get, pathItem.Post, pathItem.Put };
                    possibleParameterisedOperations.FindAll(x => x != null).ForEach(x => DescribeEnumParameters(x.Parameters));
                }
            }
        }

        private void DescribeEnumProperties(IDictionary<string, Schema> keyValuePair)
        {
            if (keyValuePair == null)
                return;

            foreach (KeyValuePair<string, Schema> propertyDictionaryItem in keyValuePair)
            {
                Schema property = propertyDictionaryItem.Value;
                if (property.Ref != null)
                {
                    var info = enumDocumentInfos.Where(c => c.RefName == property.Ref).FirstOrDefault();
                    if (info != null)
                        property.Description += info.Description;
                }

                if (property.Properties != null)
                    DescribeEnumProperties(property.Properties);
            }
        }

        private void DescribeEnumParameters(IList<IParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (IParameter param in parameters)
                {
                    if (param is NonBodyParameter)
                    {
                        var p = (param as NonBodyParameter);
                        if (p.Extensions.Count > 0)
                        {
                            if (p.Extensions.Keys.Contains("MyDescription"))
                            {
                                p.Description += p.Extensions["MyDescription"].ToString();
                            }
                        }
                    }
                }
            }
        }

    }
}
