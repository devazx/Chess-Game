using ChessGame.Tabuleiro;

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
    }
}
