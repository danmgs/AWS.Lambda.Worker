using Microsoft.Extensions.Configuration;
using System.IO;

namespace AWS.Lambda.Worker.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration = null;
        public static IConfiguration Instance
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
                }

                return _configuration;
            }
        }
    }
}