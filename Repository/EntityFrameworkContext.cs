namespace Project.CodeGeneration.Validation
{
    using Project.Utils;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using static Project.CodeGeneration.ConstValues;

    public class EntityFrameworkContext : Base<EntityFrameworkContext>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName)) return;

            FileName = "EFContext.cs";

            string validatorPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Repository + @"EFContext.cs"));

            List<string> listA = ReadCode(validatorPath);
            var index = listA.FindIndex(x => x.Contains("public DbSet<Issue> IssueList { get; set; }"));

            var validation = @"
        public DbSet<{0}> {0}List { get; set; }"
            .Replace("{0}", modelTypeName);
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
