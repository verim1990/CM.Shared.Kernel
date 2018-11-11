namespace CM.Shared.Kernel.Others.Sql
{
    public class SqlSettings
    {
        public string Host { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                if (SqlContextSettings == null)
                {
                    return "";
                }

                return $"Server={Host};Database={SqlContextSettings.Database};User Id={User};Password={Password}";
            }
        }

        private SqlContextSettings SqlContextSettings { get; set; }

        public void AddContextSettings(SqlContextSettings sqlContextSettings)
        {
            SqlContextSettings = sqlContextSettings;
        }
    }
}
