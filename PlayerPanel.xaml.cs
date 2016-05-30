using System;
using System.Collections.Generic;
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
        public System.Collections.ObjectModel.ObservableCollection<Pirate> PiratesColl;
        public Board Brd;
        public Pirate P;

        public PlayerPanel()
        {
            InitializeComponent();
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
        /*
        public void SetAbility()
        {
            ul.IsEnabled = true;//P.Able[0, 0];
            u.IsEnabled = P.Able[1, 0];
            ur.IsEnabled = P.Able[2, 0];
            l.IsEnabled = P.Able[0, 1];
            r.IsEnabled = P.Able[2, 1];
            dl.IsEnabled = P.Able[0, 2];
            d.IsEnabled = P.Able[1, 2];
            dr.IsEnabled = P.Able[2, 2];
        }*/

        private void ul_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y - 1;
            Brd.Move(new Point(x, y), P);
        }

        private void u_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X;
            int y = (int)P.Pos.Y - 1;
            Brd.Move(new Point(x, y), P);
        }

        private void ur_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y - 1;
            Brd.Move(new Point(x, y), P);
        }

        private void l_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y;
            Brd.Move(new Point(x, y), P);
        }

        private void coin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void r_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y;
            Brd.Move(new Point(x, y), P);
        }

        private void dl_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X - 1;
            int y = (int)P.Pos.Y + 1;
            Brd.Move(new Point(x, y), P);
        }

        private void d_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X;
            int y = (int)P.Pos.Y + 1;
            Brd.Move(new Point(x, y), P);
        }

        private void dr_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)P.Pos.X + 1;
            int y = (int)P.Pos.Y + 1;
            Brd.Move(new Point(x, y), P);
        }
    }
}
