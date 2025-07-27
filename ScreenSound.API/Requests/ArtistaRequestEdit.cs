namespace ScreenSound.API.Requests
{
    public record ArtistaRequestEdit(int Id, string nome, string bio)
    : ArtistasRequest(nome, bio, null);
}
