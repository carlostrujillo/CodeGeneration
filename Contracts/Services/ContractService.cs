namespace Project.CodeGeneration.Contracts.Services
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ContractService : Base<ContractService>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName)) return;

            FileName = $"I{modelTypeName}Service.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName);

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.BLL + @"Contracts\"));

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
