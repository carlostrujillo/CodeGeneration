namespace Project.CodeGeneration.Web.Loader
{
    using System.IO;
    using System.Linq;
    using static Project.CodeGeneration.ConstValues;

    public class ModelWebLoader : Base<ModelWebLoader>
    {
        public void Execute()
        {
            var types = Helper.GetModels()
                        .Select(x => x.Name);

            FileName = "ServiceLoader.cs";

            ReadFile(
                (value) =>
            {
                var kernel = string.Empty;
                var methods = string.Empty;

                foreach (var item in types)
                {
                    kernel += string.Format(
            @"Kernel.Bind<I{0}Service>().To<{0}Service>();
            ", item);
                    methods +=
         @"public static I{0}Service Get{0}Service(IMongoDatabase db)
        {
            return KernelLoader.Kernel.Get<I{0}Service>(new ConstructorArgument(""db"", db));
        }

        ".Replace("{0}", item);
                }

                var code = value
                            .Replace("{0}", kernel)
                            .Replace("{1}", methods);

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + @"Loader\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code, false);

                CopyToDest(destFilePath, false);

                DeleteCurrentFile();

                return true;
            });
        }
    }
}
