﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jackal
{
    /// <summary>
    /// Логика взаимодействия для PlayerPanel.xaml
    /// </summary>
    public partial class PlayerPanel : UserControl
    {
        public ObservableCollection<Pirate> PiratesColl;
        public Board Brd;
        public Pirate P;
        public int pn = 0;
        public int Turn = 0;
        public int Selected = 0;

        public PlayerPanel()
        {
            InitializeComponent();
            try { P = PiratesColl[0]; } catch { }
        }

        public void NextTurn()
        {
            switch (Turn)
            {
                case (0):
                    t0.IsChecked = false;
                    t1.IsChecked = true;
                    break;
                case (1):
                    t1.IsChecked = false;
                    t2.IsChecked = true;
                    break;
                case (2):
                    t2.IsChecked = false;
                    t3.IsChecked = true;
                    break;
                case (3):
                    t3.IsChecked = false;
                    t0.IsChecked = true;
                    break;
            }

            Turn = (Turn + 1) % 4;
            Selected = (Turn * 3) + pn;
            {
                P.Selected = false;
                P = PiratesColl[Selected];
                P.Selected = true;
            }
        }

        public void SelectPirate(int pn)
        {
            Selected = (Turn * 3) + pn;
            try
            {
                P.Selected = false;
                P = PiratesColl[Selected];
                P.Selected = true;
            }
            catch { }
        }
        public void SetAbility()
        {
            ul.IsEnabled = true;//P.Able[0, 0];
            u.IsEnabled = true;
            ur.IsEnabled = true;
            l.IsEnabled = true;
            r.IsEnabled = true;
            dl.IsEnabled = true;
            d.IsEnabled = true;
            dr.IsEnabled = true;
        }

        //public void SetAbility()
        //{
        //    ul.IsEnabled = P.Able[0, 0];
        //    u.IsEnabled = P.Able[1, 0];
        //    ur.IsEnabled = P.Able[2, 0];
        //    l.IsEnabled = P.Able[0, 1];
        //    r.IsEnabled = P.Able[2, 1];
        //    dl.IsEnabled = P.Able[0, 2];
        //    d.IsEnabled = P.Able[1, 2];
        //    dr.IsEnabled = P.Able[2, 2];
        //}

        private void ul_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y - 1;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void u_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X;
            int y = (int)P.Pos.Y - 1;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void ur_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y - 1;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void l_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void coin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void r_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void dl_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y + 1;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void d_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X;
            int y = (int)P.Pos.Y + 1;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void dr_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y + 1;
            if (Brd.Move(new Point(x, y), P) == 0)
                NextTurn();
        }

        private void firstp_Checked(object sender, RoutedEventArgs e)
        {
            SelectPirate(0);
        }

        private void secondp_Checked(object sender, RoutedEventArgs e)
        {
            SelectPirate(1);
        }

        private void thirdp_Checked(object sender, RoutedEventArgs e)
        {
            SelectPirate(2);
        }
    }
}