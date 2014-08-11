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
    /// Interaction logic for FormTableOrder.xaml
    /// </summary>
    public partial class Form_TableOrder : UserControl
    {

        public delegate void ItemFormButtonClicked(object sender);
        public ItemFormButtonClicked itemFormButtonClicked;

        public delegate void PaymentFormButtonClicked(object sender);
        public PaymentFormButtonClicked paymentFormButtonClicked;

        public delegate void CloseFormButtonClicked(UserControl uc);
        public CloseFormButtonClicked closeFormButtonClicked;

        internal delegate void DeleteItemFromTableOrder(TableRow tableRow);
        internal DeleteItemFromTableOrder deleteItem;

        internal delegate void DisableAddItemForm();
        internal DisableAddItemForm disableAddItemForm;

        internal delegate void EnableAddItemForm();
        internal EnableAddItemForm enableAddItemForm;

        TableRow selectedTableRow;

        TableOrder thisTableOrder;

        //
        //
        //      CARLO - START
        //
        //

        public Form_TableOrder()
        {
            InitializeComponent();

            TableTableOrder.itemSelected += ItemSelected;

        }


        internal void EnableForm(TableOrder tableOrder)
        {
            thisTableOrder = tableOrder;

            TextBlock_TableNum.Text = "TABLE " + tableOrder.GetTableNumber();

            TableTableOrder.ConvertTableOrderToTableRow(tableOrder);
            UpdateSumTaxTotal(thisTableOrder.GetSumTaxTotal());

            if (thisTableOrder.GetTableStatus() == TableOrder.PRESETTLEMENT_BILL)
            {
                DisableTableTableOrder();

            }
            else
            {
                EnableTableTableOrder();

            }

            BrushConverter brush = new BrushConverter();

            Rectangle1.Fill = (Brush)brush.ConvertFrom("#363636");
            Rectangle2.Fill = (Brush)brush.ConvertFrom("#363636");
            Rectangle3.Fill = (Brush)brush.ConvertFrom("#363636");

            TextBlock_TableNum.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_ItemOrderedHeader.Foreground = (Brush)brush.ConvertFrom("#363636");

            TextBlock_SumHeader.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_SumNum.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_TaxHeader.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_TaxNum.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_TotalHeader.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_TotalNum.Foreground = (Brush)brush.ConvertFrom("#363636");
            TextBlock_PrintHeader.Foreground = (Brush)brush.ConvertFrom("#363636");

        }

        internal void DisableForm()
        {
            BrushConverter brush = new BrushConverter();

            Rectangle1.Fill = (Brush)brush.ConvertFrom("#cdcdcd");
            Rectangle2.Fill = (Brush)brush.ConvertFrom("#cdcdcd");
            Rectangle3.Fill = (Brush)brush.ConvertFrom("#cdcdcd");

            TextBlock_TableNum.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");
            TextBlock_ItemOrderedHeader.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");

            DisableTableTableOrder();

            TextBlock_SumHeader.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");
            TextBlock_SumNum.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");
            TextBlock_TaxHeader.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");
            TextBlock_TaxNum.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");
            TextBlock_TotalHeader.Foreground = Brushes.White;
            TextBlock_TotalNum.Foreground = Brushes.White;
            TextBlock_PrintHeader.Foreground = (Brush)brush.ConvertFrom("#cdcdcd");

            Button_PrintDraft.IsEnabled = false;
            Button_PaymentForm.IsEnabled = false;
            Button_EditReceipt.IsEnabled = false;

        }


        internal void TogglePrintDraftButtonState()
        {
            if (thisTableOrder.GetItemsOrdered().Count() > 0)
            {
                Button_PrintDraft.IsEnabled = true;
            }
            else
            {
                Button_PrintDraft.IsEnabled = false;
            }
        }

        private void ItemSelected(TableRow tableRow)
        {
            Button_DeleteItem.IsEnabled = true;
            selectedTableRow = tableRow;
        }

        private void Button_ItemForm_Click(object sender, RoutedEventArgs e)
        {
            itemFormButtonClicked(sender);
            Button_ItemForm.IsEnabled = false;
        }

        private void Button_PaymentForm_Click(object sender, RoutedEventArgs e)
        {
            paymentFormButtonClicked(this);
        }

        private void Button_PrintDraft_Click(object sender, RoutedEventArgs e)
        {
            //Reset selectedTableRow
            TableTableOrder.ConvertTableOrderToTableRow(thisTableOrder);

            thisTableOrder.SetTableStatus(TableOrder.PRESETTLEMENT_BILL);
            
            DisableTableTableOrder();

            disableAddItemForm();

        }

        //
        // CARLO - END
        //

        //
        // WILSON - START
        //

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            closeFormButtonClicked(this);
            
        }

        internal void Button_DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //delete selectedTableRow
            deleteItem(selectedTableRow);

            //selected item will be deleted, hence, no item will be 'selected'/'highlighted'
            //so Button_DeleteItem needs to be disabled
            Button_DeleteItem.IsEnabled = false;
        }

        internal void UpdateSumTaxTotal(string sumTaxTotalString)
        {
            String[] values = sumTaxTotalString.Split(',');

            TextBlock_SumNum.Text = values[0];
            TextBlock_TaxNum.Text = values[1];
            TextBlock_TotalNum.Text = values[2];

        }

        private void Button_EditReceipt_Click(object sender, RoutedEventArgs e)
        {
            thisTableOrder.SetTableStatus(TableOrder.OPEN_BILL);
            EnableTableTableOrder();

            TogglePrintDraftButtonState();

            Button_EditReceipt.IsEnabled = false;
            Button_PaymentForm.IsEnabled = false;

            enableAddItemForm();

        }


        //
        //
        // WILSON - END
        //
        //

        //
        //
        // VINCENT - START
        //
        //

        internal TableOrder GetTableOrder()
        {
            return thisTableOrder;
        }

        //
        //
        // VINCENT - END
        //
        //

        //
        //
        // CARLO - START
        //
        //

        internal void DisableTableTableOrder()
        {
            TableTableOrder.IsHitTestVisible = false;
            TableTableOrder.Opacity = 0.30;
            Button_ItemForm.IsEnabled = false;
            Button_DeleteItem.IsEnabled = false;

            Button_PrintDraft.IsEnabled = false;

            Button_EditReceipt.IsEnabled = true;
            Button_PaymentForm.IsEnabled = true;
        }

        internal void EnableTableTableOrder()
        {
            TableTableOrder.IsHitTestVisible = true;
            TableTableOrder.Opacity = 1;
            Button_ItemForm.IsEnabled = true;

            TogglePrintDraftButtonState();

            Button_EditReceipt.IsEnabled = false;
            Button_PaymentForm.IsEnabled = false;

        }

        //
        //
        // CARLO - END
        //
        //

    }
}
