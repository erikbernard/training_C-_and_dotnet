using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Requests
{
    public record ArtistasRequest([Required] string nome, [Required] string bio, string? fotoPerfil);
}
