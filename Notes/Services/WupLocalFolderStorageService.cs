using System;
using System.IO;
using Windows.Storage;
using Newtonsoft.Json;
using NotesPCL.Services;

namespace Notes.Services
{
    public class WupLocalFolderStorageService : IStorageService
    {
        private readonly StorageFolder storageFolder;
        private readonly JsonSerializer jsonSerializer;

        public WupLocalFolderStorageService()
        {
            storageFolder = ApplicationData.Current.LocalFolder;

            jsonSerializer = new JsonSerializer();
        }

        public void Write<T>(string key, T value)
        {
            var stream = storageFolder.OpenStreamForWriteAsync(key, CreationCollisionOption.ReplaceExisting).Result;
            using (var streamWriter = new StreamWriter(stream))
            {
                jsonSerializer.Serialize(streamWriter, value);
            }
        }

        public T Read<T>(string key)
        {
            return Read<T>(key, default(T));
        }

        public T Read<T>(string key, T defaultValue)
        {
            try
            {
                var stream = storageFolder.OpenStreamForReadAsync(key).Result;
                using (var streamReader = new JsonTextReader(new StreamReader(stream)))
                {
                    return jsonSerializer.Deserialize<T>(streamReader);
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
