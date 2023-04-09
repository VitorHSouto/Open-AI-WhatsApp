namespace VHS_Tarefas.Data
{
    public class LowercaseNamingConvention : IPluralizer
    {
        public string Pluralize(string name)
        {
            return name;
        }

        public string Singularize(string name)
        {
            return name;
        }

        public string ConvertToDatabaseName(string name)
        {
            return name.ToLower();
        }

        public string ConvertToPropertyName(string name)
        {
            return name;
        }
    }
}
