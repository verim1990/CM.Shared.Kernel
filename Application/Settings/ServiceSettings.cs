namespace CM.Shared.Kernel.Application.Settings
{
    public class ServiceSettings
    {
        public string Name { get; set; } = "";

        public string HttpUrl { get; set; } = "";

        public string HttpsUrl { get; set; } = "";

        public string LocalUrl { get; set; } = "";

        public string Path => Name.ToLower();
    }
}
