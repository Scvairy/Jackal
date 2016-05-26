using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackal
{
    public enum Team { none, red, black, yellow, white }

    public enum TileType
    {
        water, grass1, grass2, grass3, grass4, astr1, adiag1, adiag2, astr2, a3,
        astr4, adiag4, rum, lab2, lab3, lab4, lab5, ice, hole, croc,
        cannibal, fort, gfort, coins1, coins2, coins3, coins4, coins5, balloon, cannon, horse, plane
    }

    public enum TileDirection { up = 0, left = 90, right = 270, down = 180 }
}
