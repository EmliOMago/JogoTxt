using System;

// Classe para o modo "Em Construção"
static class Vazio
{
    public static void MostrarTelaConstrucao()
    {
        Console.Clear();
        Console.Title = "JogosTxt - Em Construção";
        Console.WriteLine("Em construção");
        
        // Desenha contorno
        for (int linha = 2; linha <= 15; linha++)
        {
            Console.SetCursorPosition(1, linha);
            Console.Write("O");
            Console.SetCursorPosition(80, linha);
            Console.Write("O");
        }
        
        Console.SetCursorPosition(2, 8);
        Console.WriteLine("Este modo de jogo está em construção.");
        Console.SetCursorPosition(2, 10);
        Console.WriteLine("Pressione ESC para voltar ao menu principal.");

        while (true)
        {
            var tecla = Console.ReadKey(true).Key;
            if (tecla == ConsoleKey.Escape)
                break;
        }
    }
}
