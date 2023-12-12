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
            PosicaoChess pos = new PosicaoChess('a', 1);
            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosicao());
            Console.ReadLine();

        }
    }
}
