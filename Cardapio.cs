using System;
using System.Collections.Generic;
using System.IO;

// Classe principal que controla o menu e a navegação entre os modos de jogo
class Cardapio
{
    // Caminho para o arquivo de recordes na pasta Documentos/JogosTxt
    private static readonly string caminhoRecordes = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "JogosTxt", "recordes.txt");

    // Método principal que inicia o jogo
    static void Main(string[] args)
    {
        Console.Title = "JogosTxt - Menu Principal";
        string nomeJogador = ObterNomeJogador();
        MostrarMenuPrincipal(nomeJogador);
    }

    // Solicita o nome do jogador na tela de boas-vindas
    private static string ObterNomeJogador()
    {
        Console.Clear();
        Console.WriteLine("Bem-vindo ao JogosTxt!");
        Console.WriteLine("Digite seu nome e pressione Enter:");
        return Console.ReadLine();
    }

    // Exibe o menu principal com recordes e opções
    private static void MostrarMenuPrincipal(string nomeJogador)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Olá, {nomeJogador}!");
            Console.WriteLine("==== MENU PRINCIPAL ====");
            
            // Exibe os recordes
            ExibirRecordes();

            // Exibe instruções de controle
            Console.WriteLine("\nCONTROLES:");
            Console.WriteLine("1 - Modo Travessia");
            Console.WriteLine("2 - Modo Labirinto");
            Console.WriteLine("3 - Modo Em Construção");
            Console.WriteLine("ESC - Sair do jogo");

            // Processa a entrada do usuário
            var tecla = Console.ReadKey(true).Key;
            switch (tecla)
            {
                case ConsoleKey.D1:
                    IniciarModoTravessia();
                    break;
                case ConsoleKey.D2:
                    IniciarModoLabirinto();
                    break;
                case ConsoleKey.D3:
                    IniciarModoConstrucao();
                    break;
                case ConsoleKey.Escape:
                    if (ConfirmarSaida())
                        return;
                    break;
            }
        }
    }

    // Exibe os 5 maiores recordes de cada modo
    private static void ExibirRecordes()
    {
        // Cria o arquivo se não existir
        if (!File.Exists(caminhoRecordes))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoRecordes));
            File.WriteAllText(caminhoRecordes, "");
        }

        // Lê e formata os recordes
        string[] linhas = File.ReadAllLines(caminhoRecordes);
        var recordesTravessia = new List<string>();
        var recordesLabirinto = new List<string>();

        foreach (string linha in linhas)
        {
            if (linha.StartsWith("Travessia:"))
                recordesTravessia.Add(linha.Substring(10));
            else if (linha.StartsWith("Labirinto:"))
                recordesLabirinto.Add(linha.Substring(10));
        }

        // Preenche com espaços vazios se não houver 5 recordes
        while (recordesTravessia.Count < 5) recordesTravessia.Add("Vazio - 0 - 00/00/0000 00:00");
        while (recordesLabirinto.Count < 5) recordesLabirinto.Add("Vazio - 0 - 00/00/0000 00:00");

        // Exibe em colunas
        Console.WriteLine("\nRECORDES:");
        Console.WriteLine("{0,-40} {1,-40}", "TRAVESSIA", "LABIRINTO");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("{0,-40} {1,-40}", 
                recordesTravessia[i], 
                recordesLabirinto[i]);
        }
    }

    // Inicia o modo Travessia
    private static void IniciarModoTravessia()
    {
        // Implementação no arquivo TravesiaTxt.cs
        TravesiaTxt.Jogar(caminhoRecordes);
    }

    // Inicia o modo Labirinto
    private static void IniciarModoLabirinto()
    {
        // Implementação no arquivo LabirinTxt.cs
        LabirinTxt.Jogar(caminhoRecordes);
    }

    // Inicia o modo em construção
    private static void IniciarModoConstrucao()
    {
        // Implementação no arquivo Vazio.cs
        Vazio.MostrarTelaConstrucao();
    }

    // Confirma se o jogador realmente quer sair
    private static bool ConfirmarSaida()
    {
        Console.Clear();
        Console.WriteLine("Deseja realmente sair do jogo?");
        Console.WriteLine("Enter - Confirmar");
        Console.WriteLine("ESC - Voltar ao jogo");
        
        while (true)
        {
            var tecla = Console.ReadKey(true).Key;
            if (tecla == ConsoleKey.Enter)
                return true;
            if (tecla == ConsoleKey.Escape)
                return false;
        }
    }
}
