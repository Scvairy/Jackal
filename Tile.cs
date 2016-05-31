using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Jackal
{
    public class Tile : ViewModelBase
    {

        public Uri ImageUri { get; set; } // relative path to image

        private TileType _Type;
        public TileType Type
        {
            get { return this._Type; }
            set { this._Type = value; RaisePropertyChanged(() => this.Type); }
        }
        private Point _Pos;
        public Point Pos
        {
            get { return this._Pos; }
            set { this._Pos = value; RaisePropertyChanged(() => this.Pos); }
        }
        private bool _Opened;
        public bool Opened
        {
            get { return this._Opened; }
            set { this._Opened = value; RaisePropertyChanged(() => this.Opened); }
        }

        private short _Gold;
        public short Gold
        {
            get { return this._Gold; }
            set { this._Gold = value; RaisePropertyChanged(() => this.Gold); }
        }
        private Player _Team;
        public Player Team
        {
            get { return this._Team; }
            set { this._Team = value; RaisePropertyChanged(() => this.Team); }
        }
        private bool _Pirate1;
        public bool Pirate1
        {
            get { return this._Pirate1; }
            set { this._Pirate1 = value; RaisePropertyChanged(() => this.Pirate1); }
        }
        private bool _Pirate2;
        public bool Pirate2
        {
            get { return this._Pirate2; }
            set { this._Pirate2 = value; RaisePropertyChanged(() => this.Pirate2); }
        }
        private bool _Pirate3;
        public bool Pirate3
        {
            get { return this._Pirate3; }
            set { this._Pirate3 = value; RaisePropertyChanged(() => this.Pirate3); }
        }


        private TileDirection _Direction;
        public TileDirection Direction
        {
            get { return this._Direction; }
            set { this._Direction = value; RaisePropertyChanged(() => this.Direction); }
        }

        public List<Point> Vectors = new List<Point>();

        public Tile(TileType type, int x = 0, int y = 0, bool opened = false, TileDirection direction = TileDirection.up, short gold = 0, Player team = Player.none, bool pirate1 = false, bool pirate2 = false, bool pirate3 = false)
        {

            this.Pos = new Point(x, y);
            this.Type = type;
            this.Gold = gold;
            this.Team = team;
            this.Pirate1 = pirate1;
            this.Pirate2 = pirate2;
            this.Pirate3 = pirate3;
            this.Opened = opened;

            switch (Type) // setup image paths according to tile types
            {
                case (TileType.grass1):
                    ImageUri = new Uri("./tiles/empty-1.png", UriKind.Relative);
                    break;

                case (TileType.grass2):
                    ImageUri = new Uri("./tiles/empty-2.png", UriKind.Relative);
                    break;

                case (TileType.grass3):
                    ImageUri = new Uri("./tiles/empty-3.png", UriKind.Relative);
                    break;

                case (TileType.grass4):
                    ImageUri = new Uri("./tiles/empty-4.png", UriKind.Relative);
                    break;

                case (TileType.rum):
                    ImageUri = new Uri("./tiles/keg-of-rum.png", UriKind.Relative);
                    break;

                case (TileType.ice):
                    ImageUri = new Uri("./tiles/ice.png", UriKind.Relative);
                    break;

                case (TileType.hole):
                    ImageUri = new Uri("./tiles/pitfall.png", UriKind.Relative);
                    break;

                case (TileType.croc):
                    ImageUri = new Uri("./tiles/crocodile.png", UriKind.Relative);
                    break;

                case (TileType.cannibal):
                    ImageUri = new Uri("./tiles/cannibal.png", UriKind.Relative);
                    break;

                case (TileType.fort):
                    ImageUri = new Uri("./tiles/fort.png", UriKind.Relative);
                    break;

                case (TileType.gfort):
                    ImageUri = new Uri("./tiles/fort-w-aborigine.png", UriKind.Relative);
                    break;

                case (TileType.coins1):
                    ImageUri = new Uri("./tiles/coins-1.png", UriKind.Relative);
                    break;

                case (TileType.coins2):
                    ImageUri = new Uri("./tiles/coins-2.png", UriKind.Relative);
                    break;

                case (TileType.coins3):
                    ImageUri = new Uri("./tiles/coins-3.png", UriKind.Relative);
                    break;

                case (TileType.coins4):
                    ImageUri = new Uri("./tiles/coins-4.png", UriKind.Relative);
                    break;

                case (TileType.coins5):
                    ImageUri = new Uri("./tiles/coins-5.png", UriKind.Relative);
                    break;

                case (TileType.balloon):
                    ImageUri = new Uri("./tiles/balloon.png", UriKind.Relative);
                    break;

                case (TileType.cannon):
                    ImageUri = new Uri("./tiles/gun.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    break;

                case (TileType.water):
                    ImageUri = new Uri("./tiles/water.png", UriKind.Relative);
                    break;

                case (TileType.astr1):
                    ImageUri = new Uri("./tiles/arrow-1.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    Vectors.Add(new Point(0, -1));
                    break;

                case (TileType.adiag1):
                    ImageUri = new Uri("./tiles/arrow-2.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    Vectors.Add(new Point(1, -1));
                    break;

                case (TileType.adiag2):
                    ImageUri = new Uri("./tiles/arrow-4.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    Vectors.Add(new Point(1, -1));
                    Vectors.Add(new Point(-1, 1));
                    break;

                case (TileType.astr2):
                    ImageUri = new Uri("./tiles/arrow-3.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    Vectors.Add(new Point(0, -1));
                    Vectors.Add(new Point(0, 1));
                    break;

                case (TileType.a3):
                    ImageUri = new Uri("./tiles/arrow-5.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    Vectors.Add(new Point(0, 1));
                    Vectors.Add(new Point(-1, 0));
                    Vectors.Add(new Point(1, -1));
                    break;

                case (TileType.astr4):
                    ImageUri = new Uri("./tiles/arrow-6.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    Vectors.Add(new Point(1, 0));
                    Vectors.Add(new Point(0, 1));
                    Vectors.Add(new Point(0, -1));
                    Vectors.Add(new Point(-1, 0));
                    break;

                case (TileType.adiag4):
                    ImageUri = new Uri("./tiles/arrow-7.png", UriKind.Relative);
                    Direction = (TileDirection)((Board.rand.Next() % 4) * 90);
                    Vectors.Add(new Point(1, -1));
                    Vectors.Add(new Point(1, 1));
                    Vectors.Add(new Point(-1, 1));
                    Vectors.Add(new Point(-1, -1));
                    break;

                case (TileType.lab2):
                    ImageUri = new Uri("./tiles/rotate-2.png", UriKind.Relative);
                    break;

                case (TileType.lab3):
                    ImageUri = new Uri("./tiles/rotate-3.png", UriKind.Relative);
                    break;

                case (TileType.lab4):
                    ImageUri = new Uri("./tiles/rotate-4.png", UriKind.Relative);
                    break;

                case (TileType.lab5):
                    ImageUri = new Uri("./tiles/rotate-5.png", UriKind.Relative);
                    break;

                case (TileType.horse):
                    ImageUri = new Uri("./tiles/horse.png", UriKind.Relative);
                    break;

                case (TileType.plane):
                    ImageUri = new Uri("./tiles/airplane.png", UriKind.Relative);
                    break;

                default: // TODO: remove this when proper image arranging is done
                    ImageUri = new Uri("./tiles/empty!.png", UriKind.Relative);
                    break;
            }
            RotateVec(Direction);
        }

        public Tile(short g, TileType type) : this(type, 0, 0, false, TileDirection.up, g) { }

        static public bool IsRightDir(Tile t, Point dir)
        {
            if (t.Vectors.Count == 0) return true;
            return t.Vectors.Contains(dir);
        }

        int cos(int degree)
        {
            switch (degree)
            {
                case (0):
                    return 1;
                case (90):
                    return 0;
                case (180):
                    return -1;
                case (270):
                    return 0;
                default:
                    return 2;
            }
        }
        int sin(int degree)
        {
            switch (degree)
            {
                case (0):
                    return 0;
                case (90):
                    return 1;
                case (180):
                    return 0;
                case (270):
                    return -1;
                default:
                    return 2;
            }
        }

        public void RotateVec(TileDirection dir)
        {
            for (int i = 0; i < Vectors.Count; i++)
            {
                var v = Vectors[i];
                int x = (v.X * cos((int)dir)) - (v.Y * sin((int)dir)),
                       y = (v.X * sin((int)dir)) + (v.Y * cos((int)dir));
                Vectors[i] = new Point(x, y);
            }
        }
    }
}
