using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using System.Linq;

namespace Jackal
{
    public class Board
    {
        public Tile[,] boardArray { get; set; }
        public ObservableCollection<Tile> TilesColl = new ObservableCollection<Tile>();
        public ObservableCollection<Pirate> PiratesColl = new ObservableCollection<Pirate> {

            new Pirate(PirateId.first, Player.black, 6, 0, true),
            new Pirate(PirateId.second, Player.black, 6, 0),
            new Pirate(PirateId.third, Player.black, 6, 0),

            new Pirate(PirateId.first, Player.red, 0, 6),
            new Pirate(PirateId.second, Player.red, 0, 6),
            new Pirate(PirateId.third, Player.red, 0, 6),

            new Pirate(PirateId.first, Player.white, 6, 12),
            new Pirate(PirateId.second, Player.white, 6, 12),
            new Pirate(PirateId.third, Player.white, 6, 12),

            new Pirate(PirateId.first, Player.yellow, 12, 6),
            new Pirate(PirateId.second, Player.yellow, 12, 6),
            new Pirate(PirateId.third, Player.yellow, 12, 6)
        };
        public static Random rand = new Random();

        public Board()
        {
            var tiles = GenerateAllTiles();
            if (tiles.Count != 117)
            {
                System.Windows.MessageBox.Show("Количество тайлов не совпадает");
                return;
            }

            boardArray = new Tile[13, 13];

            for (int y = 0; y < 13; y += 12)
                for (int x = 1; x < 12; x++)
                    SetWater(x, y, boardArray); //0th and last with water
            for (int y = 1; y < 12; y++)
                for (int x = 0; x < 13; x += 12)
                    SetWater(x, y, boardArray); //left and right with water
            for (int y = 1; y < 12; y += 10)
                for (int x = 1; x < 12; x += 10)
                    SetWater(x, y, boardArray); //corners with water

            for (int y = 1; y < 12; y += 10)
                for (int x = 2; x < 11; x++)
                    SetRandomTile(x, y, tiles, boardArray); //1st and last without corners

            for (int y = 2; y < 11; y++)
                for (int x = 2; x < 11; x++)
                    SetRandomTile(x, y, tiles, boardArray); //2,2 -> 9,

            for (int x = 1; x < 12; x += 10)
                for (int y = 2; y < 11; y++)
                    SetRandomTile(x, y, tiles, boardArray);

            SetGraveyard(0, 0, boardArray);
            SetGraveyard(0, 12, boardArray);
            SetGraveyard(12, 12, boardArray);
            SetGraveyard(12, 0, boardArray);

            SetShip(6, 0, boardArray, Player.black);
            SetShip(0, 6, boardArray, Player.red);
            SetShip(6, 12, boardArray, Player.white);
            SetShip(12, 6, boardArray, Player.yellow);

            for (int y = 0; y < 13; y++)
                for (int x = 0; x < 13; x++)
                {
                    var tmp = boardArray[x, y];
                    tmp.Pos = new Point(x, y);
                    TilesColl.Add(tmp);
                }


        }

        public void SetGraveyard(int x, int y, Tile[,] tiles)
        {
            tiles[x, y] = new Tile(TileType.graveyard);
        }
        public void SetShip(int x, int y, Tile[,] tiles, Player team = Player.black)
        {
            tiles[x, y] = new Tile(TileType.ship, 0, 0, true, TileDirection.up, 0, team);
        }

        public bool IsEnemyShip(Tile tile, Pirate pir)
        {
            return !(tile.Team == pir.Team);
        }

        public Point GetShotPos(Point cannon, Pirate pir)
        {
            Point newpos = new Point();
            int to = GetIndex(cannon);
            switch (TilesColl[to].Direction)
            {
                case (TileDirection.up):
                    newpos = new Point(cannon.X, 12);
                    break;
                case (TileDirection.left):
                    newpos = new Point(0, cannon.Y);
                    break;
                case (TileDirection.down):
                    newpos = new Point(cannon.X, 0);
                    break;
                case (TileDirection.right):
                    newpos = new Point(12, cannon.Y);
                    break;
            }
            return newpos;
        }

        public int MoveToShip(Point pTo, Pirate pir)
        {
            int to = GetIndex(pTo);
            if (IsEnemyShip(TilesColl[to], pir))
            {
                Kill(pir);
                return 1; //пират убит
            }
            else
            {
                pir.Pos = pTo;
                return 0;
            }
        }

        public int Move(Point pTo, Pirate pir, bool arrow = false, bool force = false)
        {
            if (pir.Alive == false) return -6; //пират мёртв (graveyard tile)
            if (pTo.X < 0 || pTo.Y < 0 || pTo.X > 12 || pTo.Y > 12)
                return -1; //выход за пределы диапазона поля
            if (pir.Drunkc > 0)
                return -2; //пират пьян
            if (pir.Trapped == true)
            {
                var pirates = PiratesColl.Where(X => X.Pos == pir.Pos);
                if (pirates.Count() > 1)
                    foreach (Pirate p in pirates)
                    {
                        p.Trapped = false;
                        Move(pTo, p);
                    }
                return -4; //пират в ловушке
            }
            if (pir.Lab > 0)
            {
                pir.Lab--;
                return FinishStep(1, pir);
            }

            var pFrom = pir.Pos;
            var from = GetIndex(pFrom);
            var to = GetIndex(pTo);
            var fromTile = TilesColl[from];
            var toTile = TilesColl[to];
            Point newpos = new Point();
            Point dir = new Point(pTo.X - pFrom.X, pTo.Y - pFrom.Y);

            if (arrow && !Tile.IsRightDir(fromTile, dir))
                return -3; //поворот не туда

            bool drink = false;

            if (!arrow)
                switch (fromTile.Type)
                {
                    case (TileType.astr1):
                    case (TileType.astr2):
                    case (TileType.astr4):
                    case (TileType.adiag1):
                    case (TileType.adiag2):
                    case (TileType.adiag4):
                    case (TileType.a3):
                        return Move(pTo, pir, true, true);
                }

            if (fromTile.Type == TileType.water)
                return FinishStep(Swim(pTo, pir), pir);

            Open(pTo);
            switch (toTile.Type)
            {
                case (TileType.water):
                    if (force) newpos = pTo;
                    else newpos = pFrom;
                    break;

                case (TileType.graveyard):
                    newpos = pFrom;
                    break;

                case (TileType.gfort):
                    var pirsThere = PiratesColl.Where(X => X.Pos == pTo);
                    var enemyPirsThere = pirsThere.Where(X => X.Team != pir.Team);
                    if (enemyPirsThere.Count() == 0)
                    {
                        newpos = pTo;
                        var deadpirs = PiratesColl.Where(X => X.Alive == false);
                        foreach (Pirate p in deadpirs)
                        {
                            p.Alive = true;
                            p.Pos = pTo;
                        }
                    }
                    else return -5; //пират идёт в занятый форт
                    break;

                case (TileType.fort):
                    var pirates = PiratesColl.Where(X => X.Pos == pTo);
                    var enemypirates = pirates.Where(X => X.Team != pir.Team);
                    if (enemypirates.Count() == 0) newpos = pTo;
                    else return -5; //пират идёт в занятый форт
                    break;

                case (TileType.hole):
                    pir.Trapped = true;
                    newpos = pTo;
                    break;

                case (TileType.lab2):
                    pir.Lab = 1;
                    newpos = pTo;
                    break;
                case (TileType.lab3):
                    pir.Lab = 2;
                    newpos = pTo;
                    break;
                case (TileType.lab4):
                    pir.Lab = 3;
                    newpos = pTo;
                    break;
                case (TileType.lab5):
                    pir.Lab = 4;
                    newpos = pTo;
                    break;

                case (TileType.cannibal):
                    Kill(pir);
                    break;

                case (TileType.grass1):
                case (TileType.grass2):
                case (TileType.grass3):
                case (TileType.grass4):
                case (TileType.coins1):
                case (TileType.coins2):
                case (TileType.coins3):
                case (TileType.coins4):
                case (TileType.coins5):
                    newpos = pTo;
                    break;

                case (TileType.cannon):
                    Open(pTo);
                    switch (TilesColl[to].Direction)
                    {
                        case (TileDirection.up):
                            newpos = new Point(pTo.X, 0);
                            break;
                        case (TileDirection.left):
                            newpos = new Point(0, pTo.Y);
                            break;
                        case (TileDirection.down):
                            newpos = new Point(pTo.X, 12);
                            break;
                        case (TileDirection.right):
                            newpos = new Point(12, pTo.Y);
                            break;
                    }
                    Open(newpos);
                    pir.Pos = pTo;
                    return FinishStep(Move(newpos, pir, false, true), pir);

                case (TileType.ice):
                    pir.Pos = pTo;
                    return Move(pTo + new Size(dir), pir);

                case (TileType.croc):
                    newpos = pir.Pos;
                    pir.Pos = pTo;
                    break;

                case (TileType.rum):
                    drink = true;
                    newpos = pTo;
                    break;

                case (TileType.balloon):
                    newpos = GetShipPos(pir.Team);
                    break;

                case (TileType.ship):
                    return FinishStep(MoveToShip(pTo, pir), pir);

                default:
                    newpos = pTo;
                    break;
            }
            Open(newpos);
            var pirs = PiratesColl.Where(X => X.Pos == newpos && X.Team != pir.Team);
            if (pirs.Count() == 1)
            {
                var victim = pirs.ElementAt(0);
                if (victim.Gold == true)
                {
                    victim.Gold = false;
                    TilesColl[GetIndex(victim.Pos)].Gold++;
                }
                victim.Pos = GetShipPos(victim.Team);
            }
            else if (pirs.Count() > 1)
            {
                if (pir.Gold == true)
                {
                    pir.Gold = false;
                    TilesColl[GetIndex(newpos)].Gold++;
                }
                newpos = GetShipPos(pir.Team);
            }
            pir.Pos = newpos;
            return FinishStep(0, pir, drink);
        }

        public int Swim(Point pTo, Pirate pir)
        {
            if (pTo.X < 0 || pTo.Y < 0 || pTo.X > 12 || pTo.Y > 12)
                return -1; //выход за пределы диапазона поля
            var pFrom = pir.Pos;
            var from = GetIndex(pFrom);
            var to = GetIndex(pTo);
            var fromTile = TilesColl[from];
            var toTile = TilesColl[to];
            Point newpos = new Point();
            Open(pTo);
            switch (toTile.Type)
            {
                case (TileType.water):
                    newpos = pTo;
                    break;
                case (TileType.ship):
                    if (toTile.Team == pir.Team)
                        newpos = pTo;
                    else
                    {
                        Kill(pir);
                    }
                    break;

                default:
                    newpos = pFrom;
                    break;
            }
            pir.Pos = newpos;
            return 0;
        }

        public Point GetShipPos(Player team)
        {
            var tile = TilesColl.Where(X => X.Team == team).FirstOrDefault();
            if (tile == null) return new Point();
            else return tile.Pos;
        }

        public int FinishStep(int result, Pirate pir, bool drink = false)
        {
            for (int i = (int)pir.Team - 1; i < (int)pir.Team - 1 + 3; i++)
            {
                Pirate p = PiratesColl[i];
                if (p.Drunkc > 0)
                    p.Drunkc -= 1;
            }
            if (drink) pir.Drunkc++;
            return result;
        }

        public void Kill(Pirate pir)
        {
            pir.Alive = false;
            var newpos = new Point();
            switch (pir.Team)
            {
                case (Player.red):
                    newpos = new Point(0, 12);
                    break;
                case (Player.white):
                    newpos = new Point(12, 12);
                    break;
                case (Player.yellow):
                    newpos = new Point(12, 0);
                    break;
            }
            pir.Pos = newpos;
        }


        public void Open(int x, int y)
        {
            TilesColl[GetIndex(x, y)].Opened = true;
        }
        public void Open(Point p)
        {
            if (p.X >= 0 && p.Y >= 0)
                TilesColl[GetIndex((int)p.X, (int)p.Y)].Opened = true;
        }

        static public Point GetPoint(int index)
        {
            return new Point(index % 13, index / 13);
        }

        static public int GetIndex(double x, double y)
        {
            if (x >= 0 && y >= 0)
                return (int)(y * 13 + x);
            else return 0;
        }

        static public int GetIndex(Point p)
        {
            if (p.X >= 0 && p.Y >= 0)
                return (int)(p.Y * 13 + p.X);
            else return 0;
        }

        static void SetRandomTile(int x, int y, List<Tile> tiles, Tile[,] board)
        {
            int r = rand.Next() % (tiles.Count);
            board[x, y] = tiles[r];
            tiles.RemoveAt(r);
        }
        static void SetWater(int x, int y, Tile[,] board)
        {
            board[x, y] = new Tile(TileType.water, x, y, true);
        }

        List<Tile> GenerateAllTiles()
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < 3; i++)
                tiles.Add(new Tile(TileType.grass2)); //grass

            for (int g = 1; g < 5; g++)
                for (int i = 0; i < 10; i++)
                    tiles.Add(new Tile((TileType)g)); //grass

            for (int a = 5; a < 12; a++)
                for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile((TileType)a)); //arrows

            for (int i = 0; i < 4; i++)
                tiles.Add(new Tile(TileType.rum));
            for (int i = 0; i < 5; i++)
                tiles.Add(new Tile(TileType.lab2));
            for (int i = 0; i < 4; i++)
                tiles.Add(new Tile(TileType.lab3));
            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.lab4));

            tiles.Add(new Tile(TileType.lab5));

            for (int i = 0; i < 6; i++)
                tiles.Add(new Tile(TileType.ice));
            for (int i = 0; i < 3; i++)
                tiles.Add(new Tile(TileType.hole));
            for (int i = 0; i < 4; i++)
                tiles.Add(new Tile(TileType.croc));
            tiles.Add(new Tile(TileType.cannibal));

            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.fort));
            tiles.Add(new Tile(TileType.gfort));

            for (int i = 0; i < 5; i++)
                tiles.Add(new Tile(1, TileType.coins1));
            for (int i = 0; i < 5; i++)
                tiles.Add(new Tile(2, TileType.coins1));
            for (int i = 0; i < 3; i++)
                tiles.Add(new Tile(3, TileType.coins1));
            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(4, TileType.coins1));
            tiles.Add(new Tile(5, TileType.coins1));

            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.balloon));
            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.cannon));

            /*
            tiles.Add(new Tile(TileType.plane));
            tiles.Add(new Tile(TileType.horse));
            tiles.Add(new Tile(TileType.horse));
            */
            return tiles;
        }
    }
}