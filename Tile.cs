using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jackal
{
    public class Tile : ViewModelBase
    {

        public Uri ImageUri { get; set; } // relative path to image

        public TileType Type { get; set; }
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
        private Team _Team;
        public Team Team
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


        public Tile(TileType type, int x = 0, int y = 0, bool opened = false, TileDirection direction = TileDirection.up, short gold = 0, Team team = Team.none, bool pirate1 = false, bool pirate2 = false, bool pirate3 = false)
        {
            Random r = new Random();

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

                case (TileType.balloon):
                    ImageUri = new Uri("./tiles/balloon.png", UriKind.Relative);
                    break;

                case (TileType.cannon):
                    ImageUri = new Uri("./tiles/gun.png", UriKind.Relative);
                    break;

                case (TileType.water):
                    ImageUri = new Uri("./tiles/water.png", UriKind.Relative);
                    break;

                case (TileType.astr1):
                    ImageUri = new Uri("./tiles/arrow-1.png", UriKind.Relative);
                    Direction = (TileDirection)((r.Next() % 4) * 90);
                    break;

                case (TileType.adiag1):
                    ImageUri = new Uri("./tiles/arrow-2.png", UriKind.Relative);
                    Direction = (TileDirection)((r.Next() % 4) * 90);
                    break;

                case (TileType.adiag2):
                    ImageUri = new Uri("./tiles/arrow-4.png", UriKind.Relative);
                    Direction = (TileDirection)((r.Next() % 4) * 90);
                    break;

                case (TileType.astr2):
                    ImageUri = new Uri("./tiles/arrow-3.png", UriKind.Relative);
                    Direction = (TileDirection)((r.Next() % 4) * 90);
                    break;

                case (TileType.a3):
                    ImageUri = new Uri("./tiles/arrow-5.png", UriKind.Relative);
                    Direction = (TileDirection)((r.Next() % 4) * 90);
                    break;

                case (TileType.astr4):
                    ImageUri = new Uri("./tiles/arrow-6.png", UriKind.Relative);
                    Direction = (TileDirection)((r.Next() % 4) * 90);
                    break;

                case (TileType.adiag4):
                    ImageUri = new Uri("./tiles/arrow-7.png", UriKind.Relative);
                    Direction = (TileDirection)((r.Next() % 4) * 90);
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
        }

        public Tile(short g, TileType type) : this(type, 0, 0, false, TileDirection.up, g) { }
    }
}
