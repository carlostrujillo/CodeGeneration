﻿namespace Project.CodeGeneration.Web.ClientApp.Store.Selector
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ClientAppSelector : Base<ClientAppSelector>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = $"{modelTypeName}.selectors.ts".ToLower();

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName)
                                .Replace("{1}", modelTypeName.ToLower())
                                .Replace("{2}", modelTypeName.ToUpper());

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + @"ClientApp\src\app\store\selectors\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Web));
                IncludeInProject(proj, $"ClientApp\\src\\app\\store\\selectors\\{FileName}");

                return true;
            });
        }
    }
}
