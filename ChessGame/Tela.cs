using System;
using Tabuleiro;
using Tabuleiro.Enums;

namespace ChessGame
{
    class Tela
    {
        public static void imprimirTabuleiro(TabuleiroF tab)
        {
            for (int i = 0; i < tab.Linha; i++)
            {
                Console.Write(8 -i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.Peca(i,j));
                        Console.Write(" ");

                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.WriteLine(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
