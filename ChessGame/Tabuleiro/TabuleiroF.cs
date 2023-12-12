﻿using Tabuleiro;

namespace Tabuleiro
{
    class TabuleiroF
    {
        public int Linha { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public TabuleiroF(int linha, int colunas)
        {
            Linha = linha;
            Colunas = colunas;
            pecas = new Peca[linha, colunas];
        }

        public Peca Peca (int linha, int colunas)
        {
            return pecas[linha, colunas];
        }

        public void colocarPeca(Peca p, Posicao pos)
        {
            pecas[pos.Linha,pos.Coluna] = p;
            p.Posicao = pos;
        }
    }
}
