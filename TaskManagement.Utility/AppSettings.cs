using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Utility.Email;

namespace TaskManagement.Utility
{

    public interface IAppSettings
    {
        IConfigurationSection GetConfigurationSection(string Key);
        MailSettings MailSetting { get; }
    }
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IConfigurationSection GetConfigurationSection(string key) => _configuration.GetSection(key);

        public MailSettings MailSetting => new()
        {
            DisplayName = _configuration["MailSettings:DisplayName"],
            FromEmail = _configuration["MailSettings:Mail"],
            Password = _configuration["MailSettings:Password"],
            Host = _configuration["MailSettings:Host"],
            Port = Convert.ToInt32(_configuration["MailSettings:Port"])
        };
    }
}
