namespace Project.CodeGeneration
{
    public class IncludeProj
    {
        public IncludeProj(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
