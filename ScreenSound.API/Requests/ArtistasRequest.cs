using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record ArtistasRequest([Required] string nome, [Required] string bio, string? fotoPerfil);
}
