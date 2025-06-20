using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

// Classe que implementa o modo de jogo Travessia
static class TravesiaTxt
{
    // Método principal do modo Travessia
    public static void Jogar(string caminhoRecordes)
    {
        Console.Title = "JogosTxt - Modo Travessia";
        int nivel = 1;
        int pontos = 0;
        List<DateTime> temposNiveis = new List<DateTime>();

        while (true)
        {
            DateTime inicioNivel = DateTime.Now;
            bool nivelCompleto = ExecutarNivel(nivel, ref pontos);
            DateTime fimNivel = DateTime.Now;

            if (!nivelCompleto)
            {
                // Jogador perdeu - mostrar tela de game over
                MostrarGameOver(pontos, temposNiveis, caminhoRecordes);
                return;
            }

            temposNiveis.Add(fimNivel - inicioNivel);
            nivel++;
            pontos += 10; // Bônus por completar nível

            // Mostrar tela entre níveis
            MostrarEntreNiveis(nivel, pontos);
        }
    }

    // Executa um nível completo do jogo
    private static bool ExecutarNivel(int nivel, ref int pontos)
    {
        Console.Clear();
        Console.WriteLine("TravessiaTxt");

        // Configurações do nível
        int intervaloObstaculos = Cauculos.CalcularIntervaloObstaculos(nivel);
        int colunasObstaculos = Cauculos.CalcularColunasObstaculos(nivel);
        double velocidadeObstaculos = Cauculos.CalcularVelocidade(pontos);

        // Posição inicial do jogador
        int jogadorLinha = 7;
        int jogadorColuna = 2;

        // Lista de obstáculos e itens H
        var obstaculos = new List<Obstaculo>();
        var itensH = new List<ItemH>();

        // Gerar itens H iniciais
        for (int i = 0; i < 10; i++)
        {
            itensH.Add(GerarItemH(nivel, obstaculos, itensH));
        }

        // Temporizador para novos obstáculos
        DateTime ultimoObstaculo = DateTime.Now;

        // Loop principal do jogo
        while (true)
        {
            // Verifica se o jogador chegou ao final
            if (jogadorColuna >= 79)
                return true;

            // Desenha o cenário
            DesenharCenario(jogadorLinha, jogadorColuna, obstaculos, itensH);

            // Processa entrada do jogador
            if (Console.KeyAvailable)
            {
                var tecla = Console.ReadKey(true).Key;
                switch (tecla)
                {
                    case ConsoleKey.UpArrow when jogadorLinha > 3:
                        jogadorLinha--;
                        break;
                    case ConsoleKey.DownArrow when jogadorLinha < 14:
                        jogadorLinha++;
                        break;
                    case ConsoleKey.LeftArrow when jogadorColuna > 2:
                        jogadorColuna--;
                        break;
                    case ConsoleKey.RightArrow when jogadorColuna < 79:
                        jogadorColuna++;
                        break;
                    case ConsoleKey.Escape:
                        if (Cardapio.ConfirmarSaida())
                            return false;
                        break;
                }

                // Limpa o buffer de teclas para movimento contínuo
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }

            // Adiciona novos obstáculos periodicamente
            if ((DateTime.Now - ultimoObstaculo).TotalMilliseconds > intervaloObstaculos)
            {
                AdicionarObstaculos(nivel, colunasObstaculos, obstaculos);
                ultimoObstaculo = DateTime.Now;
            }

            // Move obstáculos
            MoverObstaculos(velocidadeObstaculos, obstaculos);

            // Verifica colisões
            if (VerificarColisoes(jogadorLinha, jogadorColuna, obstaculos, itensH, ref pontos))
                return false;

            // Pausa pequena para evitar uso excessivo da CPU
            Thread.Sleep(50);
        }
    }

    // Restante da implementação (métodos auxiliares) seria continuado aqui...
    // [Métodos adicionais seriam implementados seguindo o mesmo padrão]
}
