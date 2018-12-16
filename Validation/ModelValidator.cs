namespace Project.CodeGeneration.Validation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Project.Utils;
    using static Project.CodeGeneration.ConstValues;

    public class IncludeModelValidator : Base<IncludeModelValidator>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = "ModelValidator.cs";

            string validatorPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Validation + @"ModelValidator.cs"));

            List<string> listA = ReadCode(validatorPath);
            var index = listA.FindIndex(x => x.Contains("private ValidationResult _results;"));

            var validation = @"
        public ModelValidator Validate({0} model)
        {
            _results = new {0}Validator()
                            .Validate(model);

            return this;
        }
                ".Replace("{0}", modelTypeName);
            listA.Insert(index + 1, validation);
            var newCode = listA.ToStringBuilder().ToString();
            File.WriteAllText(validatorPath, newCode);
        }

        public void InsertLineAfter(string file, string lineToFind, string lineToInsert)
        {
            List<string> lines = File.ReadLines(file).ToList();
            int index = lines.IndexOf(lineToFind);
            lines.Insert(index + 1, lineToInsert);
            File.WriteAllLines(file, lines);
        }
    }
}
