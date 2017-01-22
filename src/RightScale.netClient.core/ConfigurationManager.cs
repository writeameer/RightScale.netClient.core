using Microsoft.Extensions.Configuration;
using System.IO;

namespace RightScale.netClient.core
{
    public class ConfigurationManager
    {
        static public IConfigurationRoot AppSettings { get; set; }

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

            AppSettings = builder.Build(); 
        }
    }
}
