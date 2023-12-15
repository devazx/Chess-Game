using Tabuleiro.Enums;
using Tabuleiro;
using Tabuleiro.Exceptions;
using Chess;

namespace Tabuleiro
{
    abstract class Peca
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

        public void IncremetarQteMovimentos()
        {
            qtdMove ++;
        }
        public void decremetarQteMovimentos()
        {
            qtdMove--;
        }


        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for(int i =0; i<Tab.Linha; i++)
            {
                for(int j=0; j<Tab.Linha; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] movimentosPossiveis();
    }
}

