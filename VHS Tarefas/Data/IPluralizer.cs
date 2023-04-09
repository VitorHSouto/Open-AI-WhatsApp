namespace VHS_Tarefas.Data
{
    public interface IPluralizer
    {
        public string Pluralize(string name);

        public string Singularize(string name);

        public string ConvertToDatabaseName(string name);

        public string ConvertToPropertyName(string name);
    }
}
