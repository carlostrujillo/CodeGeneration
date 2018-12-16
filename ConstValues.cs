namespace Project.CodeGeneration
{
    public static class ConstValues
    {
        public static class ProjBaseFolder
        {
            public const string Contracts = @"..\..\..\..\Project.Contracts\";
            public const string Repository = @"..\..\..\..\Project.Repository\";
            public const string BLL = @"..\..\..\..\Project.BLL\";
            public const string Validation = @"..\..\..\..\Project.Validation\";
            public const string Web = @"..\..\..\..\Web\";
        }

        public static class Proj
        {
            //public const string Contracts = ProjBaseFolder.Contracts + "Contracts.csproj";
            public const string Repository = ProjBaseFolder.Repository + "Project.Repository.csproj";
            public const string BLL = ProjBaseFolder.BLL + "Project.BLL.csproj";
            public const string Validation = ProjBaseFolder.Validation + "Project.Validation.csproj";
            public const string Web = ProjBaseFolder.Web + "Web.csproj";
        }
    }
}
