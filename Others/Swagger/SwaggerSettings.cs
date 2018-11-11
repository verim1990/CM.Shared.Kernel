using System.Collections.Generic;

namespace CM.Shared.Kernel.Others.Swagger
{
    public class SwaggerSettings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string TermsOfService { get; set; }

        public string FullName { get; set; } 

        public SwaggerClient Client { get; set; }

        public SwaggerEndpoints Endpoints { get; set; }

        public Dictionary<string, string> Scopes { get; set; }
    }

    public class SwaggerClient
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Secret { get; set; }
    }

    public class SwaggerEndpoints
    {
        public string Manifest { get; set; }

        public string Authorize { get; set; }

        public string Token { get; set; }
    }
}
