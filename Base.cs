namespace Project.CodeGeneration
{
    //using Project.Utils.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public abstract class Base<T>
        where T : Base<T>
    {
        public string FileName { get; set; }

        public Assembly GetAssembly => Assembly.GetExecutingAssembly();

        public string CurrentDirectory => Directory.GetCurrentDirectory();

        public string CurrentFilePath => Path.Combine(CurrentDirectory, FileName);

        public string ResourceName => $"{((T)this).GetType().Namespace}.{((T)this).GetType().Name}.txt";

        public void ReadFile(Func<string, bool> code, string file = null)
        {
            if (file == null)
            {
                file = ResourceName;
            }

            using (Stream stream = GetAssembly.GetManifestResourceStream(file))
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                code.Invoke(result);
            }
        }

        public void CreateFile(string path, string content, bool checkExists = true)
        {
            if (checkExists)
            {
                if (File.Exists(path))
                {
                    return;
                }
            }

            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
        }

        public void CopyToDest(string dest, bool checkIfExists = true)
        {
            if (File.Exists(dest) && checkIfExists)
            {
                Console.WriteLine($"** File {dest} exists in the project already");
                return;
            }

            (new FileInfo(dest)).Directory.Create();

            File.Copy(CurrentFilePath, dest, true);
            File.SetAttributes(dest, FileAttributes.Normal);
            Console.WriteLine($"- File created: {dest}");
        }

        public void DeleteCurrentFile()
        {
            File.SetAttributes(CurrentFilePath, FileAttributes.Normal);
            File.Delete(CurrentFilePath);
        }

        public void IncludeInProject(string projPath, string file)
        {
            Console.WriteLine("- Including files in the project...");

            var filePath = Path.GetDirectoryName(file);

            if (!FileBuilder.IncludeProj.ContainsKey(projPath))
            {
                FileBuilder.IncludeProj.Add(projPath, new List<IncludeProj>());
            }

            var key = FileBuilder.IncludeProj[projPath];

            key.Add(new IncludeProj("Folder", filePath));
            key.Add(new IncludeProj("Compile", file));
        }

        public List<string> ReadCode(string name)
        {
            List<string> list = new List<string>();
            using (var reader = new StreamReader(name))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    list.Add(line);
                }
            }

            return list;
        }
    }
}
