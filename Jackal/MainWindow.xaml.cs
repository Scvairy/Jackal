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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int w = 100, h = 100;
        public MainWindow()
        {
            InitializeComponent();
            App.Board Board = new App.Board();
            
            var o = App.Board.output;
            /*
            var window = new Window();
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            stackPanel.Children.Add(new Label { Content = "Label" });
            stackPanel.Children.Add(new Button { Content = "Button" });
            window.Content = stackPanel;
            window.Show(); //баловашки с WPF и динамическим изменением интерфейса/

        }
    }
}
