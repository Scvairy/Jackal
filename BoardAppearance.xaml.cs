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
    /// Логика взаимодействия для BoardAppearance.xaml
    /// </summary>
    public partial class BoardAppearance : UserControl
    {
        public Board Brd;
        public BoardAppearance()
        {
            InitializeComponent();
            Brd = new Board();
            GameBoard.ItemsSource = Brd.TilesColl;
        }


        /*   void DrawTiles(Board board) // draws all tiles of given board on grid. To draw current state of board, simply call this method
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
           }*/
    }
}
