using ScreenSound.Web.Pages;
using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
    public class ArtistaAPI
    {
        private readonly HttpClient _httpClient;
        public ArtistaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
        }
        public async Task AddArtistaAsync(ArtistasRequest artistas)
        {
            await _httpClient.PostAsJsonAsync("artistas", artistas);
        }
        public async Task UpdateArtistaAsync(ArtistaRequestEdit artistas)
        {
            await _httpClient.PutAsJsonAsync($"artistas/{artistas.Id}", artistas);
        }
        public async Task DeleteArtistaAsync(int id)
        {
            await _httpClient.DeleteAsync($"artistas/{id}");
        }
        public async Task<ArtistaResponse?> GetArtistaPorNomeAsync(string nome)
        {
            return await _httpClient.GetFromJsonAsync<ArtistaResponse>($"artistas/{nome}");
        }
    }
}
