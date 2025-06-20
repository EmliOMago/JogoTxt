using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

// Classe que implementa o modo de jogo Labirinto
static class LabirinTxt
{
    // Método principal do modo Labirinto
    public static void Jogar(string caminhoRecordes)
    {
        Console.Title = "JogosTxt - Modo Labirinto";
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
        Console.WriteLine("LabirinTxt");

        // Gera o labirinto
        char[,] labirinto = GerarLabirinto(nivel);

        // Posições importantes
        int jogadorLinha = 7, jogadorColuna = 2;
        int destinoLinha = 0, destinoColuna = 0;
        EncontrarPosicaoDestino(labirinto, ref destinoLinha, ref destinoColuna);

        // Lista de itens H
        var itensH = new List<ItemH>();
        for (int i = 0; i < 10; i++)
        {
            itensH.Add(GerarItemH(labirinto, itensH));
        }

        // Controle de cores dos obstáculos
        DateTime ultimaMudancaCor = DateTime.Now;
        int faseCor = 0;

        // Loop principal do jogo
        while (true)
        {
            // Verifica se o jogador chegou ao destino
            if (Cauculos.VerificarColisao(jogadorLinha, jogadorColuna, destinoLinha, destinoColuna))
                return true;

            // Atualiza cores dos obstáculos
            if ((DateTime.Now - ultimaMudancaCor).TotalSeconds >= 10)
            {
                faseCor = (faseCor + 1) % 5;
                ultimaMudancaCor = DateTime.Now;
            }

            // Desenha o cenário
            DesenharLabirinto(labirinto, jogadorLinha, jogadorColuna, itensH, faseCor);

            // Processa entrada do jogador
            if (Console.KeyAvailable)
            {
                var tecla = Console.ReadKey(true).Key;
                int novaLinha = jogadorLinha, novaColuna = jogadorColuna;

                switch (tecla)
                {
                    case ConsoleKey.UpArrow:
                        novaLinha--;
                        break;
                    case ConsoleKey.DownArrow:
                        novaLinha++;
                        break;
                    case ConsoleKey.LeftArrow:
                        novaColuna--;
                        break;
                    case ConsoleKey.RightArrow:
                        novaColuna++;
                        break;
                    case ConsoleKey.Escape:
                        if (Cardapio.ConfirmarSaida())
                            return false;
                        break;
                }

                // Verifica se a nova posição é válida
                if (novaLinha >= 2 && novaLinha <= 14 && 
                    novaColuna >= 2 && novaColuna <= 79 && 
                    labirinto[novaLinha, novaColuna] != 'O')
                {
                    jogadorLinha = novaLinha;
                    jogadorColuna = novaColuna;
                }

                // Limpa o buffer de teclas
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }

            // Verifica colisões com itens H
            VerificarColisoesItensH(jogadorLinha, jogadorColuna, itensH, ref pontos);

            // Verifica timeout (quando obstáculos ficam vermelhos por 10s)
            if (faseCor == 4 && (DateTime.Now - ultimaMudancaCor).TotalSeconds >= 10)
                return false;

            // Pausa pequena para evitar uso excessivo da CPU
            Thread.Sleep(50);
        }
    }

    // Restante da implementação (métodos auxiliares) seria continuado aqui...
    // [Métodos adicionais seriam implementados seguindo o mesmo padrão]
}
