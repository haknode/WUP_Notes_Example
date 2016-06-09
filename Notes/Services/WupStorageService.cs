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

        public void Save<T>(string key, T value)
        {
            var jsonString = SerializeObject(value);
            localSettingsContainer.Values[key] = jsonString;
        }

        public T Load<T>(string key)
        {
            if (localSettingsContainer.Values.ContainsKey(key))
            {
                return DeserializeObject<T>(localSettingsContainer.Values[key].ToString());
            }

            return default(T);
        }
    }
}
