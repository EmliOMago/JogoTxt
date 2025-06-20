using System;

// Classe com todos os cálculos e fórmulas matemáticas utilizadas no jogo
static class Cauculos
{
    private static readonly Random random = new Random();

    // Gera um número inteiro aleatório dentro de um intervalo
    public static int NumeroAleatorio(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    // Calcula a velocidade dos obstáculos baseado na pontuação
    public static double CalcularVelocidade(int pontos)
    {
        // Velocidade diminui (obstáculos ficam mais rápidos) conforme os pontos aumentam
        // De 1s no início até 0.5s quando atingir 2000 pontos
        double velocidadeBase = 1000; // em milissegundos
        double velocidadeMinima = 500;
        double fator = Math.Min(pontos / 2000.0, 1.0);
        return velocidadeBase - (fator * (velocidadeBase - velocidadeMinima));
    }

    // Calcula o intervalo entre novos obstáculos baseado no nível
    public static int CalcularIntervaloObstaculos(int nivel)
    {
        // Intervalo diminui de 20s no nível 1 até 1s no nível 30+
        return Math.Max(20000 - (nivel * 700), 1000);
    }

    // Calcula o número de colunas de obstáculos baseado no nível
    public static int CalcularColunasObstaculos(int nivel)
    {
        // Aumenta 2 colunas por nível até máximo de 35
        return Math.Min(5 + (nivel * 2), 35);
    }

    // Gera uma cor aleatória para os obstáculos
    public static ConsoleColor GerarCorAleatoria()
    {
        Array cores = Enum.GetValues(typeof(ConsoleColor));
        ConsoleColor[] coresPermitidas = { 
            ConsoleColor.DarkYellow, // laranja
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.DarkMagenta // roxo
        };
        return coresPermitidas[random.Next(coresPermitidas.Length)];
    }

    // Verifica se duas posições colidem (mesma linha e coluna)
    public static bool VerificarColisao(int linha1, int coluna1, int linha2, int coluna2)
    {
        return linha1 == linha2 && coluna1 == coluna2;
    }

    // Calcula a distância entre duas posições
    public static double CalcularDistancia(int x1, int y1, int x2, int y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }
}
