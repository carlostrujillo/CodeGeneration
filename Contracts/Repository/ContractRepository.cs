namespace Project.CodeGeneration.Contracts.Repository
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ContractRepository : Base<ContractRepository>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = $"I{modelTypeName}Repository.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName);

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Repository + @"Contracts\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Repository));
                IncludeInProject(proj, $"Repository\\{FileName}");

                return true;
            });
        }
    }
}
