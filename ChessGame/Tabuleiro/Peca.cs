using ChessGame.Tabuleiro.Enums;
using Tabuleiro;

namespace ChessGame.Tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qtdMove { get; protected set; }
        public TabuleiroF Tab { get; set; }

        public Peca(Posicao posicao, Cor cor, TabuleiroF tab)
        {
            Posicao = posicao;
            Cor = cor;
            Tab = tab;
            this.qtdMove = 0;
        }
    }
}
