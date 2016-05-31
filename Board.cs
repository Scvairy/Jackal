using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace Jackal
{
    public class Board
    {
        public Tile[,] boardArray { get; set; }
        public ObservableCollection<Tile> TilesColl = new ObservableCollection<Tile>();
        public ObservableCollection<Pirate> PiratesColl = new ObservableCollection<Pirate> {

            new Pirate(PirateId.first, Player.black, 6, 0),
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
                MessageBox.Show("Количество тайлов не совпадает");
                return;
            }

            boardArray = new Tile[13, 13];

            for (int y = 0; y < 13; y += 12)
                for (int x = 0; x < 13; x++)
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

        public void SetShip(int x, int y, Tile[,] tiles, Player team = Player.black)
        {
            tiles[x, y] = new Tile(TileType.ship, 0, 0, true, TileDirection.up, 0, team);
        }

        public bool IsEnemyShip(Tile tile, Pirate pir)
        {
            return !(tile.Team == pir.Team);
        }

        public int ForceMove(Point pTo, Pirate pir)
        {
            if (pTo.X < 0 || pTo.Y < 0 || pTo.X > 12 || pTo.Y > 12)
                return -1; //выход за пределы диапазона поля

            var pFrom = pir.Pos;
            var from = GetIndex(pFrom);
            var to = GetIndex(pTo);
            var fromTile = TilesColl[from];
            var toTile = TilesColl[to];
            Point newpos = new Point();
            Point dir = new Point(pTo.X - pFrom.X, pTo.Y - pFrom.Y);
            if (Tile.IsRightDir(fromTile, dir))
            {
                Open(pTo);
                switch (toTile.Type)
                {
                    case (TileType.water):
                        pir.InSea = true;
                        newpos = pTo;
                        return 0;

                    case (TileType.ship):
                        if (IsEnemyShip(TilesColl[to], pir))
                        {
                            Kill(pir);
                            break;
                        }
                        else
                        {
                            newpos = pTo;
                            break;
                        }

                    default:
                        newpos = pTo;
                        break;
                }
            }
            else
            {
                return -3; //поворот не туда
            }
            Open(newpos);
            pir.Pos = newpos;
            return 0;
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

                default:
                    newpos = pFrom;
                    break;
            }
            pir.Pos = newpos;
            UpdateAble(pir);
            return 0;
        }

        public int Move(Point pTo, Pirate pir)
        {
            if (pTo.X < 0 || pTo.Y < 0 || pTo.X > 12 || pTo.Y > 12)
                return -1; //выход за пределы диапазона поля
            var pFrom = pir.Pos;
            var from = GetIndex(pFrom);
            var to = GetIndex(pTo);
            var fromTile = TilesColl[from];
            var toTile = TilesColl[to];
            Point newpos = new Point();

            if (pir.Drunk)
                return -2; //пират пьян
            else {
                switch (fromTile.Type)
                {
                    case (TileType.astr1):
                    case (TileType.astr2):
                    case (TileType.astr4):
                    case (TileType.adiag1):
                    case (TileType.adiag2):
                    case (TileType.adiag4):
                    case (TileType.a3):
                        return ForceMove(pTo, pir);
                    case (TileType.water):
                        return Swim(pTo, pir);
                }
                Open(pTo);
                switch (toTile.Type)
                {
                    case (TileType.water):
                        newpos = pFrom;
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
                        pir.Pos = newpos;
                        return 0;


                    case (TileType.croc):
                        newpos = pir.Pos;
                        pir.Pos = pTo;
                        break;

                    case (TileType.rum):
                        pir.Drunk = true;
                        newpos = pTo;
                        break;

                    default:
                        newpos = pTo;
                        break;
                }
                Open(newpos);
                pir.Pos = newpos;
                UpdateAble(pir);
                return 0;
            }
        }

        public void Kill(Pirate pir)
        {
            pir.Alive = false;
            pir.Pos = new Point(-1, -1);
        }

        public void UpdateAble(Pirate pir)
        {
            for (int y = -1; y < 2; y++)
                for (int x = -1; x < 2; x++)
                {
                    var nx = pir.Pos.X + x;
                    var ny = pir.Pos.Y + y;
                    if (nx < 0 || ny < 0 || nx > 12 || ny > 12)
                    {
                        pir.Able[x + 1, y + 1] = false;
                        return;
                    }
                    if (TilesColl[GetIndex(nx, ny)].Opened)
                        pir.Able[x + 1, y + 1] = !(TilesColl[GetIndex(nx, ny)].Type == TileType.croc ||
                                        TilesColl[GetIndex(nx, ny)].Type == TileType.water);
                    else pir.Able[x + 1, y + 1] = !pir.Gold;
                }
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