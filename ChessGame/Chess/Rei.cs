using Tabuleiro;
using Tabuleiro.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Rei : Peca
    {
        public Rei(TabuleiroF tab, Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "R";
        }
    }
}
