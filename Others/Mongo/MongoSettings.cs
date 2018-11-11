namespace CM.Shared.Kernel.Others.Mongo
{
    public class MongoSettings
    {
        public string Host { get; set; } = "";

        public string User { get; set; } = "";

        public string Password { get; set; } = "";

        public string ConnectionString => $"mongodb://{User}:{Password}@{Host}";
    }
}
