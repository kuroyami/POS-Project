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
    /// Interaction logic for Reservation_Form.xaml
    /// </summary>
    public partial class Reservation_Form : UserControl
    {
        public delegate void CancelButtonClick();
        public CancelButtonClick cancelButtonClick;

        public Reservation_Form()
        {
            InitializeComponent();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            cancelButtonClick();
        }

        internal void InitializeForm(int resNum)
        {
            TextBlock_ResNum.Text = resNum.ToString();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
