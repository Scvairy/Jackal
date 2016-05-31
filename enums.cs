using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackal
{
    public enum Player { none, white, red, black, yellow }

    public enum PirateId { first, second, third }

    public enum TileType
    {
        water, grass1, grass2, grass3, grass4, astr1, adiag1, adiag2, astr2, a3,
        astr4, adiag4, rum, lab2, lab3, lab4, lab5, ice, hole, croc,
        cannibal, fort, gfort, coins1, coins2, coins3, coins4, coins5, balloon, cannon, horse, plane, ship
    }

    public enum TileDirection { right = 0, up = 90, left = 180, down = 270 }

}
