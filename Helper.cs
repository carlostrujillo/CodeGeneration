namespace Project.CodeGeneration
{
    using Project.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class Helper
    {
        public static IEnumerable<PropertyInfo> GetModelProperties(string modelTypeName)
        {
            return GetModels()
                    .Where(x => x.Name == modelTypeName)
                    .FirstOrDefault()
                    .GetTypeInfo()
                    .DeclaredProperties;
        }

        public static bool CheckModelName(string name)
        {
            return GetModels()
                    .Select(x => x.Name)
                    .Any(x => x == name);
        }

        public static IEnumerable<Type> GetModels()
        {
            return Assembly
                  .Load("Project.Models")
                  .GetTypes()

                  .Where(x => x.IsDefined(typeof(CodeGenerationAttribute)));
        }
    }
}
