using Windows.Storage;
using Newtonsoft.Json;
using NotesPCL.Services;

namespace Notes.Services
{
    class WupLocalSettingsStorageService : IStorageService
    {
        private readonly ApplicationDataContainer localSettingsContainer;

        public WupLocalSettingsStorageService()
        {
            localSettingsContainer = ApplicationData.Current.LocalSettings;
        }

        public void Write<T>(string key, T value)
        {
            var jsonString = JsonConvert.SerializeObject(value);
            localSettingsContainer.Values[key] = jsonString;
        }

        public T Read<T>(string key)
        {
            return Read<T>(key, default(T));
        }

        public T Read<T>(string key, T defaultValue)
        {
            if (localSettingsContainer.Values.ContainsKey(key))
            {
                var jsonString = localSettingsContainer.Values[key] as string;
                return JsonConvert.DeserializeObject<T>(jsonString);
            }

            return defaultValue;
        }
    }
}
