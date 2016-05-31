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

        private void newgame_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
