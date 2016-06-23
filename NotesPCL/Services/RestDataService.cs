using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotesPCL.Models;

namespace NotesPCL.Services
{
    public class RestDataService : IDataService
    {
        private readonly IStorageService storageService;
        private Settings settings;

        private readonly HttpClient httpClient;

        public RestDataService(IStorageService storageService)
        {
            this.storageService = storageService;

            settings = Settings.DefaultSettings;

            httpClient = new HttpClient();
        }

        private String Uri
        {
            get
            {
                if(String.IsNullOrWhiteSpace(settings.DbTenantId))
                    throw new Exception("Tenant ID is not set!");

                return $"http://notesservice.azurewebsites.net/api/{settings.DbTenantId}/Notes";
            }
        }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            var json = await httpClient.GetStringAsync(Uri);
            var notesDto = JsonConvert.DeserializeObject<IEnumerable<NoteDto>>(json);
            var notes = notesDto.Select(NoteDtoConverter.DtoToNote);
            return notes;
        }

        public async Task<Note> GetNote(int id)
        {
            var json = await httpClient.GetStringAsync($"{Uri}/{id}");
            var noteDto = JsonConvert.DeserializeObject<NoteDto>(json);
            var note = NoteDtoConverter.DtoToNote(noteDto);
            return note;
        }

        public async Task AddNote(Note note)
        {
            var noteDto = NoteDtoConverter.NoteToDto(note, settings.DbTenantId);
            var json = JsonConvert.SerializeObject(noteDto);
            var response = await httpClient.PostAsync(Uri, new JsonContent(json));
        }

        public async Task UpdateNote(Note note)
        {
            var noteDto = NoteDtoConverter.NoteToDto(note, settings.DbTenantId);
            var json = JsonConvert.SerializeObject(noteDto);
            await httpClient.PutAsync($"{Uri}/{note.Id}", new JsonContent(json));
        }

        public async Task RemoveNote(Note note)
        {
            await httpClient.DeleteAsync($"{Uri}/{note.Id}");
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Settings> GetSettings()
        {
            return settings;
        }

        public async Task SetSettings(Settings newSettings)
        {
            settings = newSettings;
        }

        public async Task LoadFromStorage()
        {
            var loadedSettings = storageService.Read<Settings>("Settings", Settings.DefaultSettings);
            await SetSettings(loadedSettings);
        }

        public async Task SaveToStorage()
        {
            var s = await GetSettings();
            storageService.Write("Settings", s);
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        private class JsonContent : StringContent
        {
            public JsonContent(string content) : this(content, Encoding.UTF8) { }

            private JsonContent(string content, Encoding encoding, string mediaType = "application/json") : base(content, encoding, mediaType) { }
        }
    }
}
