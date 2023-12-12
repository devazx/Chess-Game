using Chess;
using Tabuleiro;
using Tabuleiro.Enums;


namespace ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TabuleiroF tab = new TabuleiroF(8,8);

            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0,0));
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(1, 4));

            Tela.imprimirTabuleiro(tab);

            Console.ReadLine();

        }
    }
}
