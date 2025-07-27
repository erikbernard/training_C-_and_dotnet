namespace ScreenSound.Web.Requests
{
    public record ArtistaRequestEdit(int Id, string nome, string bio)
    : ArtistasRequest(nome, bio, null);
}
