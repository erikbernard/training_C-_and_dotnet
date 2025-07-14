using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointsMusicas(this WebApplication app)
        {

            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                var musicas = dal.Listar();
                if (musicas is null)
                {
                    return Results.NotFound("Nenhuma música encontrada.");
                }
                var listaDeMusicasResponse = EntityListToResponseList(musicas);
                return Results.Ok(listaDeMusicasResponse);
            });
            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound($"Música com nome {nome} não encontrada.");
                }
                return Results.Ok(musica);
            });
            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromServices] DAL<Genero> dalGenero, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica()
                {
                    ArtistaId = musicaRequest.ArtistaId,
                    Nome = musicaRequest.nome,
                    AnoLancamento = musicaRequest.anoLancamento,
                    Generos = musicaRequest.Generos is not null ? GeneroRequestConverter(musicaRequest.Generos, dalGenero) : new List<Genero>(),
                };
    
                if (musica is null)
                {
                    return Results.BadRequest("Música não pode ser nula.");
                }
                dal.Inserir(musica);
                return Results.Ok();
            });
            app.MapPut("/Musicas/{id}", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica, int id) =>
            {
                var updateMusica = dal.RecuperarPor(a => a.Id == id);
                if (updateMusica is null)
                {
                    return Results.NotFound($"Música com id {id} não encontrada.");
                }
                if (musica is null)
                {
                    return Results.BadRequest("Música não pode ser nula.");
                }
                updateMusica.Nome = musica.Nome;
                updateMusica.AnoLancamento = musica.AnoLancamento;
                return Results.Ok();

            });
            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
            {
                var musica = dal.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound($"Música com id {id} não encontrada.");
                }
                dal.Deletar(musica);
                return Results.NoContent();
            });
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
        }
        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
        {
            var listarGenero = new List<Genero>();
            foreach (var item in generos) 
            {
                var entity = RequestToEntity(item);
                var genero = dalGenero.RecuperarPor(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
                if (genero is not null)
                {
                    listarGenero.Add(genero);
                }
                else 
                {
                    listarGenero.Add(entity);
                }
            }
            
            return listarGenero;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
        }
    }
}
