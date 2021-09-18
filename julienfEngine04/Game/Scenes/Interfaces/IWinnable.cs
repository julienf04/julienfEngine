using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    interface IWinnable
    {
        public void GameOver(Spaceship.E_PlayerID playerID);
    }
}
