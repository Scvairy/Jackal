using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
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
        { }

        public void NextTurn(int a)
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

        private void ul_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y - 1;
            MoveIfPossible(x, y);
        }

        private void u_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X;
            int y = (int)P.Pos.Y - 1;
            MoveIfPossible(x, y);
        }

        private void ur_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y - 1;
            MoveIfPossible(x, y);
        }

        private void l_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y;
            MoveIfPossible(x, y);
        }

        private void coin_Click(object sender, RoutedEventArgs e)
        {
            var tile = Brd.TilesColl[Board.GetIndex(P.Pos)];
            if (P.Gold)
            {
                tile.Gold++;
                P.Gold = false;
            }
            else if (tile.Gold > 0)
            {
                tile.Gold--;
                P.Gold = true;
            }
            else
            {
                MessageBox.Show("Нет монетки");
            }
        }

        private void r_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y;
            MoveIfPossible(x, y);
        }

        private void dl_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y + 1;
            MoveIfPossible(x, y);
        }

        private void d_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X;
            int y = (int)P.Pos.Y + 1;
            MoveIfPossible(x, y);
        }

        private void dr_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y + 1;
            MoveIfPossible(x, y);
        }

        private void MoveIfPossible(int newx, int newy)
        {
            var moveResult = Brd.Move(new System.Drawing.Point(newx, newy), P);
            if (moveResult == 0)
                NextTurn();
            if (moveResult == -1)
                MessageBox.Show("выход за пределы диапазона поля");
            if (moveResult == -2)
                MessageBox.Show("пират пьян");
            if (moveResult == -3)
                MessageBox.Show("поворот не туда");

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
