namespace Project.CodeGeneration.Service
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ModelService : Base<ModelService>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = modelTypeName + "Service.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName);

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.BLL + @"Services\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.BLL));
                IncludeInProject(proj, $"Services\\{FileName}");

                return true;
            });
        }
    }
}
