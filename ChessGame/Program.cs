using Chess;
using Tabuleiro;
using Tabuleiro.Enums;
using Tabuleiro.Exceptions;


namespace ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TabuleiroF tab = new TabuleiroF(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

                Tela.imprimirTabuleiro(tab);
                                
            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

        }
    }
}
