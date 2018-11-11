namespace CM.Shared.Kernel.Others.Postgres
{
    public class PostgresSettings
    {
        public string Host { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                if (PostgresContextSettings == null)
                {
                    return "";
                }

                return $"Server={Host};Database={PostgresContextSettings.Database};User Id={User};Password={Password}";
            }
        }

    private PostgresContextSettings PostgresContextSettings { get; set; }

        public void AddContextSettings(PostgresContextSettings postgresContextSettings)
        {
            PostgresContextSettings = postgresContextSettings;
        }
    }
}
