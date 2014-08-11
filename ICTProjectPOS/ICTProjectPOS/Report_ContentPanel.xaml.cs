using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Report_ContentPanel.xaml
    /// </summary>
    public partial class Report_ContentPanel : UserControl
    {
        string selectedDate;

        Collection<ReportData_Invoice> invoiceCollection = new Collection<ReportData_Invoice>();

        public Report_ContentPanel()
        {
            InitializeComponent();
        }

        //
        //
        // CARLO - START
        //
        //

        internal void InitializePanel()
        {

            SetDateTextBlock(GetTodaysDate());

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
                {
                    UpdateData();

                });

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    UpdateDataTable(0);
                    UpdateSummaryTable();
                });

            bw.RunWorkerAsync();

        }

        //
        //
        // CARLO - END
        //
        //

        //
        //
        // WILSON - START
        //
        //

        private void SetDateTextBlock(DateTime dateTime)
        {
            TextBlock_Date.Text = dateTime.ToString("D", CultureInfo.CreateSpecificCulture("en-US")).ToUpper();
            selectedDate = dateTime.ToShortDateString();

        }

        private void Button_SetDate_Click(object sender, RoutedEventArgs e)
        {
            int day = Int32.Parse(TextBox_Day.Text);
            int month = Int32.Parse(TextBox_Month.Text);
            int year = Int32.Parse(TextBox_Year.Text);

            DateTime datePicker = new DateTime(year, month, day);

            SetDateTextBlock(datePicker);

            Grid_DatePicker.Visibility = Visibility.Hidden;
            BrushConverter brush = new BrushConverter();
            Button_ChangeDate.Background = (Brush)brush.ConvertFrom("#518f64");
            Button_ChangeDate.Foreground = (Brush)brush.ConvertFrom("#FFFFFF");

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                UpdateData();

            });

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object o, RunWorkerCompletedEventArgs args)
            {
                UpdateDataTable(0);
                UpdateSummaryTable();
            });

            bw.RunWorkerAsync();

            
        }

        //
        //
        // WILSON - START
        //
        //

        //
        //
        // VINCENT - START
        //
        //

        private void UpdateData()
        {
            invoiceCollection = new Collection<ReportData_Invoice>();

            OleDbConnection cn = new OleDbConnection();    //Connection
            OleDbCommand cmd = new OleDbCommand();         //Command
            OleDbDataReader dr;

            String path = Assembly.GetExecutingAssembly().Location;
            path = path.Replace("bin\\Debug\\ICTProjectPOS.exe", "RestaurantDB.accdb");
            String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", path);

            cn.ConnectionString = @connectionString;
            cmd.Connection = cn;

            try
            {
                cmd.CommandText = "SELECT * FROM Invoice WHERE DineDate = ? ORDER BY InvoiceNum";
                cmd.Parameters.AddWithValue("DineDate", selectedDate);

                cn.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        //Create new ReportData
                        ReportData_Invoice invoice = new ReportData_Invoice();

                        //SetInvoiceNumber
                        invoice.SetInvoiceNumber(dr[0].ToString());
                        //SetTimeStamp
                        invoice.SetTimeStamp(dr[4].ToString().Substring(11));    //Output is 'dd/mm/yyyy hh:mm:ss AM', so we remove the first 11 char to get 'hh:mm:ss: AM'
                        //SetAmountDue
                        invoice.SetAmountDue(dr[5].ToString().Replace("$", ""));
                        //SetPaymentMethod
                        invoice.SetPaymentMethod(dr[6].ToString());

                        //SetItemsOrdered
                        OleDbCommand cmd2 = new OleDbCommand();
                        cmd2.Connection = cn;
                        cmd2.CommandText = "SELECT * FROM InvoiceItem WHERE InvoiceNum = ?";
                        cmd2.Parameters.AddWithValue("InvoiceNum", dr[0].ToString());

                        OleDbDataReader dr2 = cmd2.ExecuteReader();

                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                ReportData_InvoiceItem invoiceItem = new ReportData_InvoiceItem(dr2[1].ToString(), Int32.Parse(dr2[2].ToString()));

                                OleDbCommand cmd3 = new OleDbCommand();
                                cmd3.Connection = cn;
                                cmd3.CommandText = "SELECT * FROM Item WHERE ItemID = ?";
                                cmd3.Parameters.AddWithValue("ItemID", dr2[1].ToString());

                                OleDbDataReader dr3 = cmd3.ExecuteReader();

                                if(dr3.HasRows)
                                {
                                    while (dr3.Read())
                                    {
                                        invoiceItem.SetPricePerItem(dr3[2].ToString());

                                        invoice.AddInvoiceItem(invoiceItem);
                                    }
                                }
                            }
                        }

                        //AddToReportDataCollection
                        invoiceCollection.Add(invoice);
                    }
                }

                cn.Close();

            }
            catch
            {
                //empty
            }

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

        private void UpdateDataTable(int startIndex)
        {
            TableReportContent.UpdateDataTable(invoiceCollection, startIndex);

            int currentPage = (startIndex / 12) + 1;
            int totalPage = invoiceCollection.Count() / 12;
            if (invoiceCollection.Count % 12 > 0 || totalPage == 0)
            {
                totalPage++;
            }

            if (currentPage == 1)
            {
                Button_PreviousPage.IsEnabled = false;
            }
            else
            {
                Button_PreviousPage.IsEnabled = true;
            }

            if (currentPage == totalPage)
            {
                Button_NextPage.IsEnabled = false;
            }
            else
            {
                Button_NextPage.IsEnabled = true;
            }

            TextBlock_PageIndicator.Text = String.Format("{0} OF {1}", currentPage, totalPage);

        }

        //
        //
        // CARLO - END
        //
        //

        //
        //
        // WILSON - START
        //
        //


        private void UpdateSummaryTable()
        {

            int lunchCount = 0;
            int dinnerCount = 0;
            double lunchTotalSales = 0.0;
            double dinnerTotalSales = 0.0;
           
            int foodCount = 0;
            int beverageCount = 0;
            double foodTotalSales = 0.0;
            double beverageTotalSales = 0.0;

            int cashCount = 0;
            int creditCount = 0;
            int debitCount = 0;
            int netsCount = 0;
            double cashTotalSales = 0.0;
            double creditTotalSales = 0.0;
            double debitTotalSales = 0.0;
            double netsTotalSales = 0.0;


            foreach (ReportData_Invoice invoice in invoiceCollection)
            {
                //Update Invoice Breakdown part1
                string timeStamp = invoice.GetTimeStamp();                // 'hh:mm:ss AM'
                int hh = Int32.Parse(timeStamp.Substring(0, 2));          // 'hh'
                string am = timeStamp.Substring((timeStamp.Length - 2));  // 'AM'
                if (am == "PM" && hh < 12) { hh += 12; }


                if (hh < 16) // if hh is < 16 hours it is lunch, else it is dinner
                {
                    lunchCount++;
                    lunchTotalSales += Double.Parse(invoice.GetAmountDue());
                } 
                else 
                {
                    dinnerCount++;
                    dinnerTotalSales += Double.Parse(invoice.GetAmountDue());
                }


                //Update Payment Method part1
                string paymentMethod = invoice.GetPaymentMethod();
                switch (paymentMethod)
                {
                    case "CASH":
                        cashCount++;
                        cashTotalSales += Double.Parse(invoice.GetAmountDue());
                        break;
                    case "CREDIT CARD":
                        creditCount++;
                        creditTotalSales += Double.Parse(invoice.GetAmountDue());
                        break;
                    case "DEBIT CARD":
                        debitCount++;
                        debitTotalSales += Double.Parse(invoice.GetAmountDue());
                        break;
                    case "NETS":
                        netsCount++;
                        netsTotalSales += Double.Parse(invoice.GetAmountDue());
                        break;
                }

                //Update Items Ordered part1
                Collection<ReportData_InvoiceItem> invoiceItemCollection = invoice.GetInvoiceItemCollection();
                foreach (ReportData_InvoiceItem item in invoiceItemCollection)
                {
                    string itemId = item.GetItemId();     //CS00    C => F for food, or B for beverage
                    if (itemId.Substring(0, 1) == "F")
                    {
                        foodCount += item.GetQuantity();
                        foodTotalSales += (item.GetPricePerItem() * item.GetQuantity());
                    }
                    else if (itemId.Substring(0, 1) == "B")
                    {
                        beverageCount += item.GetQuantity();
                        beverageTotalSales += (item.GetPricePerItem() * item.GetQuantity());
                    }
                }

            }

            //Update Items Ordered part2
            TextBlock_SummaryContent1_2_1.Text = String.Format("({0})", foodCount);                 //Number of FOOD ordered
            TextBlock_SummaryContent1_2_2.Text = String.Format("({0})", beverageCount);             //Number of BEVERAGE ordered

            TextBlock_SummaryContent1_3_1.Text = String.Format("{0:0.00}", foodTotalSales);         //Total sales for FOOD ordered
            TextBlock_SummaryContent1_3_2.Text = String.Format("{0:0.00}", beverageTotalSales);     //Total sales for BEVERAGE ordered

            TextBlock_SummaryContent1_2_3.Text = String.Format("({0})", foodCount + beverageCount);
            TextBlock_SummaryContent1_3_3.Text = String.Format("*{0:0.00}", foodTotalSales + beverageTotalSales);  


            //Update Invoice Breakdown part2
            TextBlock_SummaryContent2_2_1.Text = String.Format("({0})", lunchCount);                //Number of LUNCH invoice
            TextBlock_SummaryContent2_2_2.Text = String.Format("({0})", dinnerCount);               //Number of DINNER invoice

            TextBlock_SummaryContent2_3_1.Text = String.Format("{0:0.00}", lunchTotalSales);        //Total sales for LUNCH invoice
            TextBlock_SummaryContent2_3_2.Text = String.Format("{0:0.00}", dinnerTotalSales);       //Total sales for DINNER invoice

            TextBlock_SummaryContent2_2_3.Text = String.Format("({0})", lunchCount + dinnerCount);
            TextBlock_SummaryContent2_3_3.Text = String.Format("{0:0.00}", lunchTotalSales + dinnerTotalSales);  

            //Update Payment Method part2
            TextBlock_SummaryContent3_2_1.Text = String.Format("({0})", cashCount);                 //Number of times CASH is used
            TextBlock_SummaryContent3_2_2.Text = String.Format("({0})", creditCount);               //Number of times CREDIT CARD is used
            TextBlock_SummaryContent3_2_3.Text = String.Format("({0})", debitCount);                //Number of times DEBIT CARD is used
            TextBlock_SummaryContent3_2_4.Text = String.Format("({0})", netsCount);                 //Number of times NETS is used

            TextBlock_SummaryContent3_3_1.Text = String.Format("{0:0.00}", cashTotalSales);         //Total sales for CASH
            TextBlock_SummaryContent3_3_2.Text = String.Format("{0:0.00}", creditTotalSales);       //Total sales for CREDIT CARD
            TextBlock_SummaryContent3_3_3.Text = String.Format("{0:0.00}", debitTotalSales);        //Total sales for DEBIT CARD
            TextBlock_SummaryContent3_3_4.Text = String.Format("{0:0.00}", netsTotalSales);         //Total sales for NETS

            TextBlock_SummaryContent3_2_5.Text = String.Format("({0})", cashCount + creditCount +
                                                                            debitCount + netsCount);
            TextBlock_SummaryContent3_3_5.Text = String.Format("{0:0.00}", cashTotalSales + creditTotalSales +
                                                                                debitTotalSales + netsTotalSales);  

        }

        private DateTime GetTodaysDate()
        {
            DateTime datePicker = new DateTime();
            datePicker = DateTime.Now;
            return datePicker;
        }

        //
        //
        // WILSON - END
        //
        //

        //
        //
        // CARLO - START
        //
        //

        private void Button_ToggleDatePicker_Click(object sender, RoutedEventArgs e)
        {
            BrushConverter brush = new BrushConverter();

            if (Grid_DatePicker.Visibility == Visibility.Hidden)
            {
                Grid_DatePicker.Visibility = Visibility.Visible;
                (sender as Button).Background = (Brush)brush.ConvertFrom("#cdcdcd");
                (sender as Button).Foreground = (Brush)brush.ConvertFrom("#363636");
            }
            else
            {
                Grid_DatePicker.Visibility = Visibility.Hidden;
                (sender as Button).Background = (Brush)brush.ConvertFrom("#518f64");
                (sender as Button).Foreground = (Brush)brush.ConvertFrom("#FFFFFF");
            }
        }

        private void Button_PageNavigation_Click(object sender, RoutedEventArgs e)
        {
            String[] page = TextBlock_PageIndicator.Text.Replace(" OF ", ",").Split(',');
            int currentPage = Int32.Parse(page[0]);
            int totalPage = Int32.Parse(page[1]);

            if ((sender as Button).Content.ToString() == "<")
            {
                currentPage--;
            }
            else if ((sender as Button).Content.ToString() == ">")
            {
                currentPage++;
            }

            int startIndex = 12 * (currentPage - 1);

            UpdateDataTable(startIndex);

        }

        //
        //
        // CARLO - END
        //
        //
    }
}
