using Tabuleiro.Enums;
using Tabuleiro;

namespace Tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qtdMove { get; protected set; }
        public TabuleiroF Tab { get; set; }

        public Peca(TabuleiroF tab, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            this.qtdMove = 0;
        } 
    }
}
