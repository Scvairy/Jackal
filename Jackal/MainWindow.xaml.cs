using System.Windows;
using System.Windows.Controls;

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
            SizeToContent = SizeToContent.WidthAndHeight; // fits window size to content size
            InitializeComponent();
            App.Board Board = new App.Board();
            
            var o = App.Board.output;

            DrawTiles(Board); // Draw all tiles on grid. To draw current state of board, simply call this method

           
        }

        void DrawTiles(App.Board board) // draws all tiles of given board on grid. To draw current state of board, simply call this method
        {
            var tileImages = board.GetBitmapImages();

            for (var y = 0; y < tileImages.GetLength(1); y++)
            {
                for (var x = 0; x < tileImages.GetLength(0); x++)
                {
                    var currentImageToShow = new Image();
                    var src = tileImages[x, y];
                    currentImageToShow.Source = src;
                    currentImageToShow.Stretch = System.Windows.Media.Stretch.Uniform;
                    grid.Children.Add(currentImageToShow); // add image to grid
                    Grid.SetColumn(currentImageToShow, x); // move it to appropriate column
                    Grid.SetRow(currentImageToShow, y);  // and row
                }
            }
        }
    }
}
