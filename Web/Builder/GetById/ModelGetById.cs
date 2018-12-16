namespace Project.CodeGeneration.Web.Builder.GetById
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ModelGetById : Base<ModelGetById>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = $"{modelTypeName}GetByIdBuilder.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName);

                Directory.CreateDirectory(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + $"Builder\\{modelTypeName}\\"));

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + $"Builder\\{modelTypeName}\\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Web));
                IncludeInProject(proj, $"Builder\\{modelTypeName}\\{FileName}");

                return true;
            });
        }
    }
}
