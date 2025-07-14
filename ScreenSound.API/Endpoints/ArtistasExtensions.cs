using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndpointsArtistas(this WebApplication app) {

            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
            {
                var artistas = dal.Listar();
                if (artistas is null)
                {
                    return Results.NotFound("Nenhum artista encontrado.");
                }
                var listArtistasResponse = EntityListToResponseList(artistas);
                return Results.Ok(listArtistasResponse);
            });
            app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (artista is null)
                {
                    return Results.NotFound($"Artista com nome {nome} não encontrado.");
                }
                return Results.Ok(artista);
            });
            app.MapPost("/Artistas/", ([FromServices] DAL<Artista> dal, [FromBody] ArtistasRequest artistaResquest) =>
            {
                var artista = new Artista();
                artista.Nome = artistaResquest.nome;
                artista.Bio = artistaResquest.bio;
                artista.FotoPerfil = artistaResquest.fotoPerfil;
                if (artista is null)
                {
                    return Results.NotFound();
                }
                dal.Inserir(artista);
                return Results.Ok();
            });
            app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
            {
                var artista = dal.RecuperarPor(a => a.Id == id);
                if (artista is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(artista);
                return Results.NoContent();
            });
            app.MapPut("/Artistas/{id}", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista, int id) =>
            {
                var updateArtista = dal.RecuperarPor(a => a.Id == id);
                if (updateArtista is null)
                {
                    return Results.NotFound();
                }
                if (artista is null)
                {
                    return Results.BadRequest("Artista não pode ser nulo.");
                }
                updateArtista.Nome = artista.Nome;
                updateArtista.Bio = artista.Bio;
                updateArtista.FotoPerfil = artista.FotoPerfil;
                dal.Atualizar(updateArtista);
                return Results.Ok();
            });

        }
        private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
        {
            return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
        }

        private static ArtistaResponse EntityToResponse(Artista artista)
        {
            return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
        }
    }
}
