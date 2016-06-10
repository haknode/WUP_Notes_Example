using Windows.Storage;
using Windows.UI.Xaml;
using Newtonsoft.Json;
using NotesPCL.Services;
using static Newtonsoft.Json.JsonConvert;

namespace Notes.Services
{
    public class WupStorageService : IStorageService
    {
        private readonly ApplicationDataContainer localSettingsContainer;

        public WupStorageService()
        {
            localSettingsContainer = ApplicationData.Current.LocalSettings;
        }

        public void Write<T>(string key, T value)
        {
            var jsonString = SerializeObject(value);
            localSettingsContainer.Values[key] = jsonString;
        }

        public T Read<T>(string key, T defaultValue)
        {
            if (localSettingsContainer.Values.ContainsKey(key))
            {
                var jsonString = localSettingsContainer.Values[key] as string;
                return DeserializeObject<T>(jsonString);
            }

            return defaultValue;
        }
        public T Read<T>(string key)
        {
            return Read<T>(key, default(T));
        }
    }
}
