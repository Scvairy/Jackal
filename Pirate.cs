using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        
        private int _Drunkc;
        public int Drunkc
        {
            get { return this._Drunkc; }
            set { this._Drunkc = value; RaisePropertyChanged(() => this.Drunkc); }
        }
        private bool _Selected;
        public bool Selected
        {
            get { return this._Selected; }
            set { this._Selected = value; RaisePropertyChanged(() => this.Selected); }
        }
        private int _Lab;
        public int Lab
        {
            get { return this._Lab; }
            set { this._Lab = value; RaisePropertyChanged(() => this.Lab); }
        }
        private bool _Trapped;
        public bool Trapped
        {
            get { return this._Trapped; }
            set { this._Trapped = value; RaisePropertyChanged(() => this.Trapped); }
        }

        public Pirate(PirateId id, Player team, int x, int y, bool selected = false, int drunkc = 0)
        {
            Id = id;
            Team = team;
            Pos = new Point(x, y);
            Gold = false;
            Alive = true;
            Lab = 0;
            Drunkc = drunkc;
            Selected = selected;
            Trapped = false;
        }
    }
}
