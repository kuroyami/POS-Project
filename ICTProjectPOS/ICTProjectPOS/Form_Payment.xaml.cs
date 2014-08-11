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
    /// Interaction logic for FormPayment.xaml
    /// </summary>
    public partial class Form_Payment : UserControl
    {

        public delegate void CloseFormButtonClicked(UserControl uc);
        public CloseFormButtonClicked closeFormButtonClicked;

        public delegate void ConfrimButtonClicked();
        public ConfrimButtonClicked confrimButtonClicked;

        public Form_Payment()
        {
            InitializeComponent();
            ContentControl_NumPad.updatePaymentFormAmountDue += UpdateAmountDue;

        }


        //
        //
        //      WILSON - START
        //
        //

        private void UpdateAmountDue(string amount)
        {
            TextBlock_PayNum.Text = amount;

            double dDue = Double.Parse(TextBlock_DueNum.Text);

            double dAmount = Double.Parse(amount);

            double dChange = dAmount - dDue;

            String sChange = String.Format("{0:0.00}",dChange);

            TextBlock_ChangeNum.Text = sChange;

            if (dChange >= 0.00)
            {
                TextBlock_Error.Visibility = Visibility.Hidden;
            }

        }

        private void Button_PaymentMethod_Click(object sender, RoutedEventArgs e)
        {
            //Show PaymentMethodList
            StackPanel_PaymentMethodList.Visibility = Visibility.Visible;
        }

        private void Button_PaymentMethodList_Click(object sender, RoutedEventArgs e)
        {
            //Close/Hide PaymentMethodList
            //Change 'PaymentMethod' to selected method
            //If CASH is selected, show NumPad_Cash; else, hide NumPad_Cash

            StackPanel_PaymentMethodList.Visibility = Visibility.Hidden;

            TextBlock_PaymentMethod.Text = (sender as Button).Content.ToString();
            TextBlock_PayWith.Text = (sender as Button).Content.ToString() + " : ";

            if ((sender as Button).Content.ToString().Equals("CASH")) {
                TextBlock_PayNum.Text = "0.00";
                ShowNumPad();

                Rectangle_Change.Visibility = Visibility.Visible;
                TextBlock_Change.Visibility = Visibility.Visible;
                TextBlock_ChangeNum.Visibility = Visibility.Visible;

            } 
            else {
                HideNumPad();


                Rectangle_Change.Visibility = Visibility.Hidden;
                TextBlock_Change.Visibility = Visibility.Hidden;
                TextBlock_ChangeNum.Visibility = Visibility.Hidden;

                //Automatically set the amount-paid to the amount-due
                TextBlock_PayNum.Text = TextBlock_DueNum.Text;
                TextBlock_ChangeNum.Text = "0.00";
            }

            TextBlock_Error.Visibility = Visibility.Hidden;

        }

        //
        //
        //      WILSON - END
        //
        //

        //
        //
        //      CARLO - START
        //
        //

        private void ShowNumPad()
        {
            if (ContentControl_NumPad.Visibility == Visibility.Hidden)
            {
                this.Height = 835;
                ContentControl_NumPad.Visibility = Visibility.Visible;
                ContentControl_NumPad.ResetAmount();
            }
        }

        private void HideNumPad()
        {
            if (ContentControl_NumPad.Visibility == Visibility.Visible)
            {
                this.Height = 480;
                ContentControl_NumPad.Visibility = Visibility.Hidden;
            }
        }


        internal void SetAmountDue(string sumTaxTotalString)
        {
            String[] values = sumTaxTotalString.Split(',');

            TextBlock_DueNum.Text = values[2];
        }

        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            double dChange = Double.Parse(TextBlock_ChangeNum.Text);

            if (TextBlock_PaymentMethod.Text == "SELECT PAYMENT METHOD")
            {
                //raise error message
                TextBlock_Error.Text = "ERROR: PLEASE SELECT A PAYMENT METHOD";
                TextBlock_Error.Visibility = Visibility.Visible;
            }
            else if (dChange < 0.00)
            {
                //raise error message
                TextBlock_Error.Text = "ERROR: AMOUNT-PAID IS LOWER THAN AMOUNT-DUE";
                TextBlock_Error.Visibility = Visibility.Visible;
            }
            else
            {
                DisableForm();
                confrimButtonClicked();
            }
        }

        internal void EnableForm()
        {
            BrushConverter brush = new BrushConverter();
            this.Background = (Brush)brush.ConvertFrom("#518f64");

            HideNumPad();

            Button_PaymentMethod.IsHitTestVisible = true;

            Polygon_DropDown_Indicator.Fill = (Brush)brush.ConvertFrom("#363636");
            TextBlock_PaymentMethod.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_PayNum.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_Change.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_ChangeNum.Foreground = (Brush)brush.ConvertFrom("#363636");

            Rectangle_Change.Fill = (Brush)brush.ConvertFrom("#cdcdcd");

            TextBlock_PaymentMethod.Text = "SELECT PAYMENT METHOD";
            TextBlock_PayWith.Text = "(PAY WITH) :";
            TextBlock_PayNum.Text = "0.00";
            TextBlock_ChangeNum.Text = "0.00";

            Button_Confirm.IsEnabled = true;


            Rectangle_Change.Visibility = Visibility.Hidden;
            TextBlock_Change.Visibility = Visibility.Hidden;
            TextBlock_ChangeNum.Visibility = Visibility.Hidden;

            TextBlock_Error.Visibility = Visibility.Hidden;
        }

        private void DisableForm()
        {
            BrushConverter brush = new BrushConverter();
            this.Background = (Brush)brush.ConvertFrom("#cdcdcd");

            HideNumPad();

            Button_PaymentMethod.IsHitTestVisible = false;

            Polygon_DropDown_Indicator.Fill = (Brush)brush.ConvertFrom("#cdcdcd");
            TextBlock_PaymentMethod.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");
            TextBlock_PayNum.Foreground = Brushes.White;
            TextBlock_Change.Foreground = Brushes.White;
            TextBlock_ChangeNum.Foreground = Brushes.White;

            Rectangle_Change.Fill = (Brush)brush.ConvertFrom("#363636");

            Button_Confirm.IsEnabled = false;

            TextBlock_Error.Visibility = Visibility.Hidden;
        }

        //
        //
        //      CARLO - END
        //
        //

        //
        //      WILSON-START
        //

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            closeFormButtonClicked(this);
        }

        //
        //      WILSON-END
        //


    }
}
