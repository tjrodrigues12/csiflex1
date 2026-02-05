namespace CSIFLEX.DataMigration
{
    public static class Global
    {
        public static string Database { get; set; }

        public static string ConnectionString
        {
            get
            {
                return $"server={Database};user=client;password=csiflex123;port=3306;Convert Zero Datetime=True;Allow User Variables=True;";
            }
        }

        public static string User { get; set; }
    }
}
