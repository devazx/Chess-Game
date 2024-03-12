using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;
using Tabuleiro.Enums;
using Tabuleiro.Exceptions;

namespace Chess
{
    class PartidaDeXadrez
    {
        public TabuleiroF tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual {  get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new TabuleiroF(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao Origem, Posicao Destino)
        {
            Peca p = tab.retirarPeca(Origem);
            p.IncremetarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(Destino);
            tab.colocarPeca(p, Destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // jogada especial roque pequeno
            if (p is Rei && Destino.Coluna == Origem.Coluna+2)
            {
                Posicao origemT = new Posicao(Origem.Linha, Origem.Coluna + 3);
                Posicao destinoT = new Posicao(Origem.Linha, Origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.IncremetarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // roque grande
            if (p is Rei && Destino.Coluna == Origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(Origem.Linha, Origem.Coluna - 4);
                Posicao destinoT = new Posicao(Origem.Linha, Origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.IncremetarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            if (p is Peao)
            {
                if(Origem.Coluna != Destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(Destino.Linha +1, Destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(Destino.Linha - 1, Destino.Coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decremetarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                    capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decremetarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decremetarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }
             // jogada especial enPssant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if ( p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao Origem, Posicao Destino)
        {
            Peca pecaCapturada = executaMovimento(Origem, Destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(Origem, Destino, pecaCapturada);
                throw new TabuleiroException("Voce não pode se colocar em xeque!");
            }

            Peca p = tab.peca(Destino);
            // jogada especial promocao

            if ( p is Peao)
            {
                if ((p.Cor == Cor.Branca && Destino.Linha == 0) || (p.Cor == Cor.Preta && Destino.Linha == 7))
                {
                    p = tab.retirarPeca(Destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.Cor);
                    tab.colocarPeca(dama, Destino);
                    pecas.Add(dama);
                }
            } 

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            // # jogada especial EnPassant

            if ( p is Peao && (Destino.Linha == Origem.Linha -2 || Destino.Linha == Origem.Linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).Cor)
            {
                throw new TabuleiroException("Essa peça não é a sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não tem movimentos para a peça escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino invalida");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else{
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach ( Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                {
                    bool[,] mat = x.movimentosPossiveis();
                    for (int i = 0; i < tab.Linha; i++)
                    {
                        for (int j = 0; i < tab.Linha; j++)
                        {
                            if (mat[i, j])
                            {
                                Posicao origem = x.Posicao;
                                Posicao destino = new Posicao(i, j);
                                Peca pecaCapturada = executaMovimento(origem, destino);
                                bool testeXeque = estaEmXeque(cor);
                                desfazMovimento(origem, destino, pecaCapturada);
                                if (!testeXeque)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }                
            }
            return true;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoChess(coluna,linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));


        }
    }
}
