using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Table_ReportContent.xaml
    /// </summary>
    public partial class Table_ReportContent : UserControl
    {

        //
        //
        // CARLO - START
        //
        //

        public Table_ReportContent()
        {
            InitializeComponent();
        }

        internal TextBlock[,] GetTextBlock2D()
        {
            TextBlock[,] textblock2D = {
                                           {TextBlock_0_1, TextBlock_1_1, TextBlock_2_1, TextBlock_3_1, TextBlock_4_1, TextBlock_5_1},
                                           {TextBlock_0_2, TextBlock_1_2, TextBlock_2_2, TextBlock_3_2, TextBlock_4_2, TextBlock_5_2},
                                           {TextBlock_0_3, TextBlock_1_3, TextBlock_2_3, TextBlock_3_3, TextBlock_4_3, TextBlock_5_3},
                                           {TextBlock_0_4, TextBlock_1_4, TextBlock_2_4, TextBlock_3_4, TextBlock_4_4, TextBlock_5_4},
                                           {TextBlock_0_5, TextBlock_1_5, TextBlock_2_5, TextBlock_3_5, TextBlock_4_5, TextBlock_5_5},
                                           {TextBlock_0_6, TextBlock_1_6, TextBlock_2_6, TextBlock_3_6, TextBlock_4_6, TextBlock_5_6},
                                           {TextBlock_0_7, TextBlock_1_7, TextBlock_2_7, TextBlock_3_7, TextBlock_4_7, TextBlock_5_7},
                                           {TextBlock_0_8, TextBlock_1_8, TextBlock_2_8, TextBlock_3_8, TextBlock_4_8, TextBlock_5_8},
                                           {TextBlock_0_9, TextBlock_1_9, TextBlock_2_9, TextBlock_3_9, TextBlock_4_9, TextBlock_5_9},
                                           {TextBlock_0_10, TextBlock_1_10, TextBlock_2_10, TextBlock_3_10, TextBlock_4_10, TextBlock_5_10},
                                           {TextBlock_0_11, TextBlock_1_11, TextBlock_2_11, TextBlock_3_11, TextBlock_4_11, TextBlock_5_11},
                                           {TextBlock_0_12, TextBlock_1_12, TextBlock_2_12, TextBlock_3_12, TextBlock_4_12, TextBlock_5_12}
                                       };


            return textblock2D;
        }


        internal void UpdateDataTable(Collection<ReportData_Invoice> reportDataCollection, int startIndex) 
        {
            EmptyTable();

            TextBlock[,] textBlock2D = GetTextBlock2D();

            if (reportDataCollection.Count() > 0)
            {
                try
                {
                    for (int row = 0; row < 12; row++)
                    {

                        //Set INVOICE NUMBER
                        textBlock2D[row, 1].Text = String.Format("{0:00000000}", Int32.Parse(reportDataCollection[startIndex].GetInvoiceNumber()));
                        //Set TIME
                         textBlock2D[row, 2].Text = reportDataCollection[startIndex].GetTimeStamp();
                        //Set ITEMS ORDERED
                        textBlock2D[row, 3].Text = reportDataCollection[startIndex].GetItemsOrdered();
                        //Set AMOUNT DUE
                        textBlock2D[row, 4].Text = String.Format("{0:0.00}", Double.Parse(reportDataCollection[startIndex].GetAmountDue()));
                        //Set PAYMENT METHOD
                        textBlock2D[row, 5].Text = reportDataCollection[startIndex].GetPaymentMethod();

                        //Set NO (row number)
                        textBlock2D[row, 0].Text = (startIndex + 1).ToString();

                        startIndex++;
                    }
                }
                catch
                {
                    //empty
                }
            }

        }

        private void EmptyTable()
        {
            TextBlock[,] textBlock2D = GetTextBlock2D();

            foreach (TextBlock tb in textBlock2D) { tb.Text = ""; }
        }

        //
        //
        // CARLO - END
        //
        //

    }

}
