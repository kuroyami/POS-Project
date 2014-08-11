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
    /// Interaction logic for NumPad_Cash.xaml
    /// </summary>
    public partial class NumPad_Cash : UserControl
    {
        string sAmount = "000";

        internal delegate void UpdatePaymentFormAmountDue(String amount);
        internal UpdatePaymentFormAmountDue updatePaymentFormAmountDue;

        public NumPad_Cash()
        {
            InitializeComponent();
        }

        //
        //
        // CARLO - START
        //
        //

        private void Button_NumPad_Click(object sender, RoutedEventArgs e)
        {
            String content = (sender as Button).Content.ToString();

            if (content == "CE")
            {
                sAmount = "000";
            }
            else if (content == "C")
            {
                sAmount = sAmount.Remove(sAmount.Length-1);
                if (sAmount.Length < 3)
                {
                    sAmount = "0" + sAmount;
                }
            }
            else
            {
                if (sAmount.ElementAt(0) == '0')
                {
                    sAmount = sAmount.Substring(1);
                }

                sAmount += content;
                
            }

            UpdateTotal();

        }

        internal void UpdateTotal()
        {
            TextBlock_Total.Text = sAmount.Insert(sAmount.Length - 2, ".");

            updatePaymentFormAmountDue(TextBlock_Total.Text);

        }

        internal void ResetAmount()
        {
            this.sAmount = "000";
            UpdateTotal();
        }

        //
        //
        // CARLO - END
        //
        //


    }
}
