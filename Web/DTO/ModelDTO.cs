namespace Project.CodeGeneration.Web.DTO
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ModelDTO : Base<ModelDTO>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = $"{modelTypeName}DTO.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName)
                                .Replace("{1}", CreateProperties(modelTypeName));

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + @"DTO\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Web));
                IncludeInProject(proj, $"DTO\\{FileName}");

                return true;
            });
        }

        private string CreateProperties(string modelTypeName)
        {
            string props = string.Empty;

            foreach (var property in Helper.GetModelProperties(modelTypeName))
            {
                props += @"
        public {0} {1} {get;set;}"
            .Replace("{0}", property.PropertyType.Name)
            .Replace("{1}", property.Name);
            }

            return props;
        }
    }
}
