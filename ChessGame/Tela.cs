using System;
using Tabuleiro;

namespace ChessGame
{
    class Tela
    {
        public static void imprimirTabuleiro(TabuleiroF tab)
        {
            for (int i = 0; i < tab.Linha; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.Peca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }            
        }
    }
}
