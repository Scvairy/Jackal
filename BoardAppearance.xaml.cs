using System;
using System.Collections.Generic;
using System.Globalization;
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

            PP.PiratesColl = Brd.PiratesColl;
            PP.Brd = Brd;
            PP.P = PP.PiratesColl[0];
            PP.SetAbility();

            GameBoard.ItemsSource = Brd.TilesColl;
            PiratesBoard.ItemsSource = Brd.PiratesColl;
        }
    }
}
namespace Converters
{
    public class CutoffConverter : IValueConverter
    {

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(value.ToString()) > (int)Cutoff;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public int Cutoff { get; set; }
    }
}
