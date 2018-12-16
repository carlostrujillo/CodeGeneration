namespace Project.CodeGeneration.Web.Controller
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class Controller : Base<Controller>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = $"{modelTypeName}Controller.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName);

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + @"Controllers\API\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Web));
                IncludeInProject(proj, $"Controllers\\Api\\{FileName}");

                return true;
            });
        }
    }
}
