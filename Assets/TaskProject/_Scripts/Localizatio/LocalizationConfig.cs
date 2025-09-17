using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LocalizationConfig", menuName = "Configs/LocalizationConfig")]
    public class LocalizationConfig : ScriptableObject
    {
        private Dictionary<string, Dictionary<string, string>> _localization;

        [SerializeField] private List<LocalizedText> _entries;

        public void Init()
        {
            _localization = new Dictionary<string, Dictionary<string, string>>();

            foreach (var entry in _entries)
            {
                var langs = new Dictionary<string, string>
                {
                    { "ru", entry.Russian },
                    { "en", entry.English },
                };

                _localization[entry.Key] = langs;
            }
        }

        public string Get(string key, string lang)
        {
            if (_localization == null) Init();

            if (_localization.TryGetValue(key, out var langs))
            {
                if (langs.TryGetValue(lang, out var value) && !string.IsNullOrEmpty(value))
                    return value;
            }

            return key;
        }
    }
}
