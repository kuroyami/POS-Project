using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for TitleBar.xaml
    /// </summary>

    public partial class TitleBar : UserControl
    {

        Brush sbsCerulean;
        Brush sbsWhite;

        public delegate void TabSelected(object sender);
        public TabSelected tabSelected;

        //
        //
        // CARLO - START
        //
        //

        public TitleBar()
        {
            InitializeComponent();

            sbsCerulean = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#395966"));
            sbsWhite = new SolidColorBrush(Colors.White);

        }

        private void Tab_Button_Click(object sender, RoutedEventArgs e)
        {

            ResetTabButtonColor();
            ChangeTabButtonColor(sender);
            tabSelected(sender);
            
        }


        private void ChangeTabButtonColor(object sender)
        {
            Button btn = (Button)sender;

            btn.Background = sbsWhite;
            btn.Foreground = sbsCerulean;
        }

        private void ResetTabButtonColor()
        {
            Button[] tabButtons = { Tab_Summary, Tab_Tables, Tab_Reservations, Tab_Reports };

            for (int i = 0; i < tabButtons.Length; i++)
            {
                tabButtons[i].Background = sbsCerulean;
                tabButtons[i].Foreground = sbsWhite;

            }

        }

        //
        //
        // CARLO - END
        //
        //

    }
}
