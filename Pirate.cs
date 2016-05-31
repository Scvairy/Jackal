using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Jackal
{
    public class Pirate : ViewModelBase
    {
        private PirateId _Id;
        public PirateId Id
        {
            get { return this._Id; }
            set { this._Id = value; RaisePropertyChanged(() => this.Id); }
        }

        private bool _Gold;
        public bool Gold
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

        private Point _Pos;
        public Point Pos
        {
            get { return this._Pos; }
            set { this._Pos = value; RaisePropertyChanged(() => this.Pos); }
        }

        private bool _Alive;
        public bool Alive
        {
            get { return this._Alive; }
            set { this._Alive = value; RaisePropertyChanged(() => this.Alive); }
        }

        private bool[,] _Able = new bool[3, 3];
        public bool[,] Able
        {
            get { return this._Able; }
            set { this._Able = value; RaisePropertyChanged(() => this.Able); }
        }

        private bool _InSea;
        public bool InSea
        {
            get { return this._InSea; }
            set { this._InSea = value; RaisePropertyChanged(() => this.InSea); }
        }

        private bool _Drunk;
        public bool Drunk
        {
            get { return this._Drunk; }
            set { this._Drunk = value; RaisePropertyChanged(() => this.Drunk); }
        }
        private bool _Selected;
        public bool Selected
        {
            get { return this._Selected; }
            set { this._Selected = value; RaisePropertyChanged(() => this.Selected); }
        }

        public Pirate(PirateId id, Player team, int x, int y, bool inSea = false, bool drunk = false)
        {
            Id = id;
            Team = team;
            Pos = new Point(x, y);
            Gold = false;
            Alive = true;
            InSea = inSea;
            Drunk = drunk;
            Selected = false;
            for (int j = 0; y < 3; y++)
                for (int i = 0; x < 3; x++)
                    Able[i, j] = true;
        }
    }
}
