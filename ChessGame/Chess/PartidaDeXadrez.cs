using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;
using Tabuleiro.Enums;

namespace Chess
{
    class PartidaDeXadrez
    {
        public TabuleiroF tab { get; private set; }
        public int Turno;
        private Cor jogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new TabuleiroF(8, 8);
            Turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao Origem, Posicao Destino)
        {
            Peca p = tab.retirarPeca(Origem);
            p.IncremetarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(Destino);
            tab.colocarPeca(p, Destino);
        }

        private void colocarPecas()
        {
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoChess('c', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoChess('c', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoChess('d', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoChess('e', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoChess('e', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoChess('d', 1).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoChess('c', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoChess('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoChess('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoChess('e', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoChess('e', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoChess('d', 8).toPosicao());
        }
    }
}
