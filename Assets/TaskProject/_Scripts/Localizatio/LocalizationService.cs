using UnityEngine;

namespace Game
{
    public class LocalizationService
    {
        private readonly LocalizationConfig _localization;
        private string _currentLanguage;

        public string GetCurrentLanguage => _currentLanguage;

        public LocalizationService(LocalizationConfig localization)
        {
            _localization = localization;
            _currentLanguage = DetectLanguage();
        }
        
        public void SetLanguage(string lang)
        {
            _currentLanguage = lang;
        }

        public string Get(string key)
        {
            return _localization.Get(key, _currentLanguage);
        }

        private string DetectLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian: return "ru";
                case SystemLanguage.English: return "en";
                default: return "ru";
            }
        }
    }
}
