using System;
using System.Windows;
using System.Windows.Controls;

namespace Jackal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Random rnd = new Random();
        }

        private void tg_selected_click(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(tbx.Text); int y = int.Parse(tby.Text);
            var n = Board.GetIndex(x,y);
            BA.Brd.TilesColl[n].Opened = !BA.Brd.TilesColl[n].Opened;
        }

        private void toggle_click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < BA.Brd.TilesColl.Count; x++)
                BA.Brd.TilesColl[x].Opened = !BA.Brd.TilesColl[x].Opened;
        }

        private void clockwise_click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < BA.Brd.TilesColl.Count; x++)
            {
                BA.Brd.TilesColl[x].Direction = (TileDirection)(((int)BA.Brd.TilesColl[x].Direction + 90) % 360);
                BA.Brd.TilesColl[x].RotateVec(TileDirection.up);
            }

        }
        private void counterclockwise_click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < BA.Brd.TilesColl.Count; x++)
            {
                BA.Brd.TilesColl[x].Direction = (TileDirection)(((int)BA.Brd.TilesColl[x].Direction + 270) % 360);
                BA.Brd.TilesColl[x].RotateVec(TileDirection.down);
            }
        }
    }
}
