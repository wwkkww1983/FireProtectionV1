using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.SwaggerExt
{
    public static class SwaggerEnumExt
    {
        public static void AddEnumParameterFilter(this IList<IParameterFilter> parameterFilters, params Assembly[] assemblies)
        {
            parameterFilters.Add(new EnumParameterFilter());
        }


        /// <summary>
        /// 添加枚举类型的文档处理
        /// </summary>
        /// <param name="documentFilters"></param>
        /// <param name="assemblies"></param>
        public static void AddEnumDocumentFilters(this IList<IDocumentFilter> documentFilters, params Assembly[] assemblies)
        {
            documentFilters.Add(new EnumDocumentFilters(assemblies));
        }

        /// <summary>
        /// 添加枚举类型映射
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="assemblies"></param>
        public static void AddEnumTypeMapping(this IDictionary<Type, Func<Schema>> keyValuePairs, params Assembly[] assemblies)
        {
            var types = EnumTool.GetTypes(assemblies);
            types.ForEach(type =>
            {
                var nullableType = typeof(Nullable<>).MakeGenericType(type);
                keyValuePairs.Add(nullableType, () =>
                {
                    EnumSchema schema = new EnumSchema()
                    {
                        Ref = $"#/definitions/{type.Name}",
                        EnumType = type
                    };
                    return schema;
                });

                keyValuePairs.Add(type, () =>
                {
                    EnumSchema schema = new EnumSchema()
                    {
                        Ref = $"#/definitions/{type.Name}",
                        EnumType = type
                    };
                    return schema;
                });
            });
        }
    }
}
