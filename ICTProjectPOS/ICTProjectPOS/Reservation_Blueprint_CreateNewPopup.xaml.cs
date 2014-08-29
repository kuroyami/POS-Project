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

namespace ICTProjectPOS
{
    /// <summary>
    /// Interaction logic for Reservation_Blueprint_CreateNewPopup.xaml
    /// </summary>
    public partial class Reservation_Blueprint_CreateNewPopup : UserControl
    {
        public delegate void CancelButtonClick();
        public CancelButtonClick cancelButtonClick;

        public Reservation_Blueprint_CreateNewPopup()
        {
            InitializeComponent();
        }

        private void Button_CreateReservation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            cancelButtonClick();
        }
    }
}
