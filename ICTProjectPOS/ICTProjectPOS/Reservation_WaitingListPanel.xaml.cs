using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
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
    /// Interaction logic for Reservation_WaitingListPanel.xaml
    /// </summary>
    public partial class Reservation_WaitingListPanel : UserControl
    {

        public delegate void InflateReservationForm_WaitingList(string firstName, string lastName, string email, string phoneNum, string comment, string date, string time);
        public InflateReservationForm_WaitingList inflateReservationForm_WaitingList;

        List<WaitingList> waitingList;

        int selectedRecrod_ListNum;

        public Reservation_WaitingListPanel()
        {
            InitializeComponent();
        }

        internal void InitializePanel()
        {

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                UpdateWaitingList();

            });

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object o, RunWorkerCompletedEventArgs args)
            {
                TogglePageNavigationButton();
                _Table.UpdateTable(waitingList);
            });

            bw.RunWorkerAsync();

            _Table.enableCreateButton += EnableCreateButton;

        }

        private void EnableCreateButton(bool trueOrFalse)
        {
            Button_CreateNewReservation.IsEnabled = trueOrFalse;

        }

        private void UpdateWaitingList()
        {
            waitingList = new List<WaitingList>();

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
                cmd.CommandText = "SELECT * FROM WaitingList";

                cn.Open();
                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        WaitingList wl = new WaitingList(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[6].ToString());
                        waitingList.Add(wl);
                    }
                }

                cn.Close();

            }
            catch
            {
                // empty
            }
        }


        private void Button_CreateNewReservation_Click(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(_Table.GetTextBlock(_Table.highlightedRow, 0).Text);

            List<string> record = GetWaitingListRecord(index);

            inflateReservationForm_WaitingList(record[0], record[1], record[2], record[3], record[4], record[5], record[6]);

        }

        private List<string> GetWaitingListRecord(int index)
        {
            selectedRecrod_ListNum = index;

            List<string> record = new List<string>();

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
                cmd.CommandText = "SELECT * FROM WaitingList WHERE ListNum = @listNum";
                cmd.Parameters.AddWithValue("@listNum", this.selectedRecrod_ListNum);

                cn.Open();
                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        record.Add(dr[1].ToString());
                        record.Add(dr[2].ToString());
                        record.Add(dr[3].ToString());
                        record.Add(dr[4].ToString());
                        record.Add(dr[5].ToString());
                        
                        //DATE --> 31/12/2014 12:59:59 PM ---> 31/12/2014
                        string[] date = dr[6].ToString().Split(' ');
                        record.Add(date[0]);

                        string[] time = dr[7].ToString().Split(' ');
                        int hour = Int32.Parse(time[1].Split(':')[0]);
                        string modifier = time[2];

                        if ((modifier == "PM") && (hour != 12))
                        {
                            hour += 12;
                        }

                        //TIME --> 31/12/2014 12:59:59 PM ---> 23:59
                        record.Add(hour + ":00");
                    }
                }

                cn.Close();

            }
            catch
            {
                // empty
            }

            return record;
        }

        internal void DeleteWaitingList()
        {
            String path = Assembly.GetExecutingAssembly().Location;
            path = path.Replace("bin\\Debug\\ICTProjectPOS.exe", "RestaurantDB.accdb");

            String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", path);
            OleDbConnection cn = new OleDbConnection(connectionString);

            OleDbCommand cmd = new OleDbCommand("DELETE FROM WaitingList WHERE ListNum = @ListNum", cn);
            cmd.Parameters.AddWithValue("@ListNum", selectedRecrod_ListNum);

            cn.Open();

            cmd.ExecuteNonQuery();

            cn.Close();

        }


        private void Button_PageNavigation_Click(object sender, RoutedEventArgs e)
        {
            string str = TextBlock_PageIndicator.Text.Replace(" OF ","/");
            string[] page = str.Split('/');

            int currentPage = Int32.Parse(page[0]);
            int totalPage = Int32.Parse(page[1]);

            //Move page
            Button btn = (Button)sender;

            if (btn.Name == "Button_PreviousPage")
            {
                currentPage--;
                _Table.ChangePage(-1);
            }
            else
            {
                currentPage++;
                _Table.ChangePage(1);
            }

            TextBlock_PageIndicator.Text = currentPage.ToString() + " OF " + totalPage.ToString();

            TogglePageNavigationButton();

        }

        private void TogglePageNavigationButton()
        {
            string str = TextBlock_PageIndicator.Text.Replace(" OF ", "/");
            string[] page = str.Split('/');

            int currentPage = Int32.Parse(page[0]);
            int totalPage = (waitingList.Count / 8);
            if (waitingList.Count % 8 > 0) totalPage++;
            if (totalPage < 1) totalPage = 1;


            TextBlock_PageIndicator.Text = currentPage.ToString() + " OF " + totalPage.ToString();


            //Enable/Disable Button_PageNavigation
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
        }



        internal void Delegate_UpdateWaitingList()
        {
            InitializePanel();
        }
    }
}
