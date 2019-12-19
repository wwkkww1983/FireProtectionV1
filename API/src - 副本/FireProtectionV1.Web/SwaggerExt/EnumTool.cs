using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.SwaggerExt
{
    public class EnumTool
    {
        public static List<Type> GetTypes(params Assembly[] assemblies)
        {
            List<Type> types = new List<Type>();
            foreach (var item in assemblies)
            {
                var list = item.GetTypes().Where(c => c.IsEnum == true && c.GetCustomAttribute<ExportAttribute>() != null).ToArray();

                types.AddRange(list);
            }
            var typesss = types.Distinct(new TypeComparer());

            return typesss.ToList();
        }

        public class TypeComparer : IEqualityComparer<Type>
        {
            public bool Equals(Type x, Type y)
            {
                if (x == null)
                    return y == null;

                return x.Name == y.Name;
            }

            public int GetHashCode(Type obj)
            {
                if (obj == null)
                    return 0;

                return obj.Name.GetHashCode();
            }
        }

        private static Dictionary<Type, string> enumTypeList = new Dictionary<Type, string>();
        private static Dictionary<string, string> NoEnumTypeList = new Dictionary<string, string>();

        public static string getDescription(Type enumType)
        {
            string res = "";
            if (enumTypeList.TryGetValue(enumType, out res))
            {
                return res;
            }

            List<string> enumDescriptions = new List<string>();
            var enums = Enum.GetValues(enumType);

            foreach (object enumOption in enums)
            {
                var type = enumOption.GetType();

                var desc = type.GetFields(BindingFlags.Public | BindingFlags.Static).Where(x => x.Name == enumOption.ToString()).FirstOrDefault()
                      .GetCustomAttributes().OfType<DescriptionAttribute>().FirstOrDefault();

                string Description = string.Empty;
                if (desc != null)
                {
                    Description = string.Format("{0} = {1} <br/> ", enumOption.ToString(), desc.Description);

                }
                else
                {
                    Description = string.Format("{0} = {1} <br/> ", (int)enumOption, Enum.GetName(enumOption.GetType(), enumOption));

                    ///保存没有备注的枚举值
                    string key = $"{enumType.Name}_{enumOption.ToString()}";
                    throw new Exception($"请把枚举 {key} 的备注加上");
                }

                enumDescriptions.Add(Description);
            }

            res = "<br/> 枚举【" + enumType.Name + "】定义：<br/> " + string.Join("", enumDescriptions.ToArray());

            enumTypeList.Add(enumType, res);

            return res;
        }
    }
}
