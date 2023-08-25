using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace test_jeu_tire
{
    internal class Player
    {
        public int pv;
        public int x;
        public int y;
        public int laterX;
        public int laterY;

        public bool moove = true;

        public int charg = 10;

        public void looseHP() 
        {
            pv = pv - 5;
        }

        public void scanmoove()
        {
            if(x == laterX ||  y == laterY)
            {
                moove = false;
            }
            else
            {
                moove = true;
            }
        }

    }
}
