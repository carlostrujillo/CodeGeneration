namespace Project.CodeGeneration.Web.Converter.DtoToModel
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class DtoToModel : Base<DtoToModel>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = $"{modelTypeName}DtoToModel.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName)
                                .Replace("{1}", CreateProperties(modelTypeName));

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + @"Converter\DtoToModel\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Web));
                IncludeInProject(proj, $"Converter\\DtoToModel\\{FileName}");

                return true;
            });
        }

        private string CreateProperties(string modelTypeName)
        {
            string props = string.Empty;

            foreach (var property in Helper.GetModelProperties(modelTypeName))
            {
                props += @"
            model.{0} = dto.{0};"
            .Replace("{0}", property.Name);
            }

            return props;
        }
    }
}
