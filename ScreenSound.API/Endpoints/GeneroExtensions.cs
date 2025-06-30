using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class GeneroExtensions
    {
        public static void AddEndpointsGeneros(this WebApplication app) 
        {
            app.MapGet("/Genero", ([FromServices] DAL<Genero> dal) => 
            {
                var genero = EntityListToResponseList(dal.Listar());
                if (genero is null)
                {
                    return Results.NotFound("Nenhuma gênero encontrada.");
                }
                return Results.Ok(genero);
            });
            app.MapGet("/Genero/{nome}", ([FromServices] DAL<Genero> dal, string nome) => 
            {
                var genero = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (genero is null) 
                {
                    return Results.NotFound($"Gênero com nome {nome} não encontrado.");
                }
                return Results.Ok(EntityToResponse(genero));
            });
            app.MapPost("/Genero", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoResquest) =>
            {
                if (generoResquest is null) 
                {
                    Results.NotFound("O valor de gênero não pode ser vazio");
                }
                var isExist =dal.RecuperarPor(a => a.Nome.ToUpper().Equals(generoResquest.Nome.ToUpper()));
                if (isExist is not null ) 
                {
                    return Results.Conflict("Valor duplicado!");
                }
                var genero  = RequestToEntity(generoResquest);
                return Results.Ok(genero);
            });
            app.MapDelete("/Genero/{id}", ([FromServices] DAL<Genero> dal, int id) => 
            {
                var genero = dal.RecuperarPor(a=> a.Id == id);
                if (genero is null)
                {
                    return Results.NotFound("Gênero para exclusão não encontrado.");
                }
                dal.Deletar(genero);
                return Results.NoContent();

            });
            
        }

        private static Genero RequestToEntity(GeneroRequest generoRequest)
        {
            return new Genero() { Nome = generoRequest.Nome, Descricao = generoRequest.Descricao };
        }

        private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> generos)
        {
            return generos.Select(a => EntityToResponse(a)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!);
        }
    }
}
