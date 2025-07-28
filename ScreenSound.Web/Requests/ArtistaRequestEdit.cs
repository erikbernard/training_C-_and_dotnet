namespace ScreenSound.Web.Requests
{
    public record ArtistaRequestEdit(int Id, string nome, string bio, string? fotoPerfil)
    : ArtistasRequest(nome, bio, fotoPerfil);
}
