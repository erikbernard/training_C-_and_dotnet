using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarArtista : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        base.Executar(artistaDAL);
        ExibirTituloDaOpcao("Registro dos Artistas");
        Console.Write("Digite o nome do artista que deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        Console.Write("Digite a bio do artista que deseja registrar: ");
        string bioDoArtista = Console.ReadLine()!;
        var artista = new Artista(nomeDoArtista, bioDoArtista);
        artistaDAL.Inserir(artista);
        Console.WriteLine($"O artista {artista.Nome} foi registrado com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
