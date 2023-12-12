using Tabuleiro;
using Tabuleiro.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace Chess
{
    class Torre : Peca
    {
        public Torre(TabuleiroF tab, Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "T";
        }
    }
}
