using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private bool _Dead;
        public bool Dead
        {
            get { return this._Dead; }
            set { this._Dead = value; RaisePropertyChanged(() => this.Dead); }
        }

        public Pirate(PirateId id, Player team, int x, int y)
        {
            Id = id;
            Team = team;
            Pos = new Point(x,y);
            Gold = false;
            Dead = false;
        }
        
    }
}
