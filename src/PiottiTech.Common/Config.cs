using System.Configuration;

namespace PiottiTech.Common
{
    public class Config
    {
        public static string AppSetting(string appSettingName)
        {
            return ConfigurationManager.AppSettings[appSettingName] ?? "";
        }

        public static string ConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}