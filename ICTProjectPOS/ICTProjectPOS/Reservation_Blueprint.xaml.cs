using System;
using System.Collections.Generic;
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
    /// Interaction logic for Reservation_Blueprint.xaml
    /// </summary>
    public partial class Reservation_Blueprint : UserControl
    {
        public delegate void CloseButtonClicked(UserControl userControl);
        public CloseButtonClicked closeButtonClicked;

        public delegate void InflateReservationForm(int reservationNum);
        public InflateReservationForm inflateReservationForm;

        public delegate void MoveBlueprintLayout();
        public MoveBlueprintLayout moveBlueprintLayout;

        public Reservation_Blueprint_CreateNewPopup popup;


        string _date;
        string _time;

        List<Reservation_Hourly> reservation_hourly;

        public Reservation_Blueprint()
        {
            InitializeComponent();
        }

        internal void InitializePanel(string date, string time)
        {
            this._date = date;
            this._time = time;

            popup = new Reservation_Blueprint_CreateNewPopup();
            popup.cancelButtonClick += ClosePopupMessage;
            popup.Margin = new Thickness(-295, -115, 0, 0);
            _Canvas.Children.Add(popup);
            popup.Visibility = Visibility.Hidden;



            UpdateHourlyReservation();
            UpdateTableButtons();
            UpdateTitleBar();

        }



        private void UpdateTitleBar()
        {
            string[] date_array = _date.Split('/');

            DateTime dt = new DateTime(Int32.Parse(date_array[2]), Int32.Parse(date_array[1]), Int32.Parse(date_array[0]));

            string str = dt.DayOfWeek.ToString().Substring(0,3) + ", " + 
                            dt.ToString("dd MMM yyyy")  + " | " 
                                + this._time;

            TextBlock_titlebar.Text = str.ToUpper();

        }

        private void UpdateHourlyReservation()
        {
            reservation_hourly = new List<Reservation_Hourly>();

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
                cmd.CommandText = "SELECT * FROM Reservation WHERE ResDate = @date AND ResTime = @time";

                cmd.Parameters.AddWithValue("@date", _date);
                cmd.Parameters.AddWithValue("@time", _time);

                cn.Open();
                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reservation_Hourly res = new Reservation_Hourly(Int32.Parse(dr[0].ToString()), Int32.Parse(dr[6].ToString()));
                        reservation_hourly.Add(res);
                    }
                }

                cn.Close();

            }
            catch
            {
                // empty
            }

        }

        private void UpdateTableButtons()
        {
            ResetTableButtons();

            List<string> reservedTables = new List<string>();

            foreach (Reservation_Hourly res in reservation_hourly)
            {
               reservedTables.Add(res.tableNum.ToString());
            }

            Button[] tableButtons = GetTableButtons();

            foreach (Button btn in tableButtons)
            {
                foreach (string tableNum in reservedTables)
                {
                    if (tableNum == btn.Content.ToString())
                    {
                        btn.Style = (Style)FindResource("ReservedTableButtonStyle");
                    }
                }
            }

            
        }

        private void ResetTableButtons()
        {
            Button[] tableButtons = GetTableButtons();

            foreach (Button btn in tableButtons)
            {
                btn.Style = (Style) FindResource("EmptyTableButtonStyle");
            }
        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            //Check whether table is empty or reserved
            bool reserved = false;

            Button table = (Button) sender;

            if (table.Style == (Style)FindResource("ReservedTableButtonStyle"))
            {
                reserved = true;
            }


            if (reserved)
            {
                int resNum = GetReservationNumber(table.Content.ToString());
                inflateReservationForm(resNum);
                moveBlueprintLayout();
            }
            else
            {
                InflateCreateReservation(table.Content.ToString());
            }

        }

        private int GetReservationNumber(string tableNumber)
        {
            int resNum = 0;

            foreach (Reservation_Hourly res in reservation_hourly)
            {
                if (res.tableNum == Int32.Parse(tableNumber))
                {
                    resNum = res.resNum;
                }
            }

            return resNum;

        }

        private void InflateCreateReservation(string tableNumber)
        {
            popup.TextBlock_Table.Text = "TABLE " + tableNumber;
            popup.TextBlock_Date.Text = this._date;
            popup.TextBlock_Time.Text = this._time;

            //_GreyBackdrop.Visibility = Visibility.Visible;
            popup.Visibility = Visibility.Visible;

        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            closeButtonClicked(this);
        }

        private void ClosePopupMessage()
        {
            _GreyBackdrop.Visibility = Visibility.Hidden;
            popup.Visibility = Visibility.Hidden;
        }

        private Button[] GetTableButtons()
        {
            Button[] tableButtons = { Table_01, Table_02, Table_03, Table_04, Table_05, Table_06, Table_07, Table_08, Table_09, Table_10, 
                                        Table_11, Table_12, Table_13, Table_14, Table_15, Table_16, Table_17, Table_18, Table_19, Table_20, 
                                        Table_21, Table_22, Table_23, Table_24, Table_25, Table_26, Table_27, Table_28, Table_29, Table_30, 
                                        Table_31};

            return tableButtons;
        }
    }
}
