﻿using System.Windows;
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

        private void tg_selected_click(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(tbx.Text); int y = int.Parse(tby.Text);
            var n = y * 13 + x;
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
                BA.Brd.TilesColl[x].Direction = (TileDirection)(((int)BA.Brd.TilesColl[x].Direction + 90) % 360);
        }
        private void counterclockwise_click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < BA.Brd.TilesColl.Count; x++)
                BA.Brd.TilesColl[x].Direction = (TileDirection)(((int)BA.Brd.TilesColl[x].Direction + 270) % 360);
        }
    }
}
