using Gpm.Common.Multilanguage;
using UnityEditor;

namespace Gpm.Manager.Constant
{
    internal static class ManagerInfos
    {
        public const string SERVICE_NAME = "GpmManager";
        public const string BRAND_NAME = "GPM";

        public const string SERVICE_INFO_MULTILANGUAGE_SEPARATOR = "$";

        public const string DEPENDENCY_UNITY_INFO_KEY = "unity";

        private const string LANGUAGE_CODE_KEY = "gpm.manager.language";

        public static string LastLanguage
        {
            get
            {
                return EditorPrefs.GetString(LANGUAGE_CODE_KEY);
            }
            set
            {
                EditorPrefs.SetString(LANGUAGE_CODE_KEY, value);
            }
        }
        
        public static string GetServiceLanguageName(string serviceName)
        {
            return string.Format("{0}{2}{1}", SERVICE_NAME, serviceName, SERVICE_INFO_MULTILANGUAGE_SEPARATOR);
        }

        public static string GetString(string key)
        {
            return GpmMultilanguage.GetString(SERVICE_NAME, key);
        }

        public static string GetString(string key, params object[] args)
        {
            return string.Format(GpmMultilanguage.GetString(SERVICE_NAME, key), args);
        }
    }
}