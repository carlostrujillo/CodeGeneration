﻿namespace Project.CodeGeneration.Web.ClientApp.Components.ListItem
{
    using System.IO;
    using static Project.CodeGeneration.ConstValues;

    public class ComponentListItemLESS : Base<ComponentListItemLESS>
    {
        public void Execute(string modelTypeName)
        {
            if (string.IsNullOrEmpty(modelTypeName))
            {
                return;
            }

            FileName = $"listitem.component.less".ToLower();

            ReadFile((value) =>
            {
                var code = value.Replace("{0}", modelTypeName)
                   .Replace("{1}", modelTypeName.ToLower())
                   .Replace("{2}", modelTypeName.ToUpper());

                string destPath = Path.GetFullPath(Path.Combine(CurrentDirectory, ProjBaseFolder.Web + @"ClientApp\src\app\components\"+ modelTypeName.ToLower()+ "\\listitem\\"));

                var destFilePath = Path.Combine(destPath, FileName);

                CreateFile(CurrentFilePath, code);

                CopyToDest(destFilePath);

                DeleteCurrentFile();

                string proj = Path.GetFullPath(Path.Combine(CurrentDirectory, Proj.Web));
                IncludeInProject(proj, $"ClientApp\\src\\app\\components\\{modelTypeName.ToLower()}\\listitem\\{FileName}");

                return true;
            });
        }
    }
}
