namespace Project.CodeGeneration.Validation
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ModelValidation : Base<ModelValidation>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = modelTypeName + "Validator.cs";

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName);

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Validation + @"Validators\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Validation));
                IncludeInProject(proj, $"Validators\\{FileName}");

                return true;
            });
        }
    }
}
