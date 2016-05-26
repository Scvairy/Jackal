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
                BA.Brd.TilesColl[x].Direction = (TileDirection)(((int)BA.Brd.TilesColl[x].Direction + 90) % 360);
        }
        private void counterclockwise_click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < BA.Brd.TilesColl.Count; x++)
                BA.Brd.TilesColl[x].Direction = (TileDirection)(((int)BA.Brd.TilesColl[x].Direction + 270) % 360);
        }

        private void sldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            for(int x = 0; x < BA.Brd.TilesColl.Count; x++)
            BA.Brd.TilesColl[x].Direction = (TileDirection)e.NewValue;
        }
    }
}
