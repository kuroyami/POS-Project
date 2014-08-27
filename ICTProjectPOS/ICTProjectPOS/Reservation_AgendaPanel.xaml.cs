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
    /// Interaction logic for Reservation_AgendaPanel.xaml
    /// </summary>
    public partial class Reservation_AgendaPanel : UserControl
    {
        List<Reservation_Weekly> reservation_weekly;

        public Reservation_AgendaPanel()
        {
            InitializeComponent();
        }

        private Button[,] GetAgendaButtons()
        {
            Button[,] agendaButtons = {{Button_Agenda_00, Button_Agenda_01, Button_Agenda_02, Button_Agenda_03, Button_Agenda_04, Button_Agenda_05, Button_Agenda_06},
                                        {Button_Agenda_10, Button_Agenda_11, Button_Agenda_12, Button_Agenda_13, Button_Agenda_14, Button_Agenda_15, Button_Agenda_16},
                                        {Button_Agenda_20, Button_Agenda_21, Button_Agenda_22, Button_Agenda_23, Button_Agenda_24, Button_Agenda_25, Button_Agenda_26},
                                        {Button_Agenda_30, Button_Agenda_31, Button_Agenda_32, Button_Agenda_33, Button_Agenda_34, Button_Agenda_35, Button_Agenda_36},
                                        {Button_Agenda_40, Button_Agenda_41, Button_Agenda_42, Button_Agenda_43, Button_Agenda_44, Button_Agenda_45, Button_Agenda_46},
                                        {Button_Agenda_50, Button_Agenda_51, Button_Agenda_52, Button_Agenda_53, Button_Agenda_54, Button_Agenda_55, Button_Agenda_56},
                                        {Button_Agenda_60, Button_Agenda_61, Button_Agenda_62, Button_Agenda_63, Button_Agenda_64, Button_Agenda_65, Button_Agenda_66},
                                        {Button_Agenda_70, Button_Agenda_71, Button_Agenda_72, Button_Agenda_73, Button_Agenda_74, Button_Agenda_75, Button_Agenda_76},
                                        {Button_Agenda_80, Button_Agenda_81, Button_Agenda_82, Button_Agenda_83, Button_Agenda_84, Button_Agenda_85, Button_Agenda_86},
                                        {Button_Agenda_90, Button_Agenda_91, Button_Agenda_92, Button_Agenda_93, Button_Agenda_94, Button_Agenda_95, Button_Agenda_96},
                                        {Button_Agenda_100, Button_Agenda_101, Button_Agenda_102, Button_Agenda_103, Button_Agenda_104, Button_Agenda_105, Button_Agenda_106},};

            return agendaButtons;

        }

        internal void UpdatePanel(DateTime startDate)
        {
            reservation_weekly = new List<Reservation_Weekly>();

            UpdateDateHeading(startDate);
            UpdateReservationContent(startDate);
            
        }

        private void UpdateDateHeading(DateTime startDate)
        {
            int day = startDate.Day;

            TextBlock[] textBlocks = {TextBlock_Header_01, TextBlock_Header_02, TextBlock_Header_03, 
                                     TextBlock_Header_04, TextBlock_Header_05, TextBlock_Header_06, TextBlock_Header_07};

            foreach (TextBlock tb in textBlocks)
            {
                string str = tb.Text;

                str = str.Split(' ')[2];
                str = day + "  " + str;

                tb.Text = str;

                day++;
            }
        }

        private void UpdateReservationContent(DateTime startDate)
        {

            string startDate_string = startDate.ToShortDateString();

            DateTime endDate = startDate.AddDays(6);
            string endDate_string = endDate.ToShortDateString();

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
                cmd.CommandText = "SELECT * FROM Reservation WHERE ResDate BETWEEN startDate AND endDate";

                cmd.Parameters.AddWithValue("startDate", startDate_string);
                cmd.Parameters.AddWithValue("endDate", endDate_string);

                cn.Open();
                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Reservation_Weekly reservation = new Reservation_Weekly(dr[7].ToString(), dr[8].ToString());
                        reservation_weekly.Add(reservation);
                    }
                }

                cn.Close();

            }
            catch 
            {
                // empty
            }

            UpdateAgendaButtons(startDate);

        }

        private void ResetAgendaButtons()
        {
            Button[,] agendaButtons = GetAgendaButtons();

            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    agendaButtons[y, x].Content = null;
                }
            }
        }

        private void UpdateAgendaButtons(DateTime startDate)
        {
            ResetAgendaButtons();

            Button[,] agendaButtons = GetAgendaButtons();

            int offset_day = startDate.Day;
            int offset_time = 11;

            foreach (Reservation_Weekly reservation in reservation_weekly)
            {
                int x = Int32.Parse(reservation.date.Split('/')[0]) - offset_day;
                int y = Int32.Parse(reservation.time.Split(':')[0]) - offset_time;


                if (agendaButtons[y, x].Content == null)
                {
                    agendaButtons[y, x].Content = "1 / 50";
                }
                else
                {
                    string s = agendaButtons[y, x].Content.ToString().Replace(" / 50", "");
                    int i = Int32.Parse(s);
                    i++;

                    agendaButtons[y, x].Content = i.ToString() + " / 50";

                    UpdateButtonStyle(agendaButtons[y, x]);

                }

            }

            System.Diagnostics.Debug.WriteLine("res" + reservation_weekly.Count);

        }

        private void UpdateButtonStyle(Button button)
        {
            string content = button.Content.ToString().Replace(" / 50", "");

            int i = Int32.Parse(content);

            if (i > 39)
            {
                button.Style = (Style)FindResource("Agenda_Blue");
            }
            else if (i > 24)
            {
                button.Style = (Style)FindResource("Agenda_Green");
            }
            else
            {
                button.Style = (Style)FindResource("Agenda_Gray");
            }
        }
        


        internal void InitializeAgenda()
        {
            DayOfWeek[] dayOfWeek = {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                                    DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday};

            //GetToday
            DateTime dt = DateTime.Today;

            //GetStartDate
            int i = 0;

            foreach (DayOfWeek nameDay in dayOfWeek)
            {
                if (dt.DayOfWeek == nameDay)
                {
                    break;
                }
                else
                {
                    i++;
                }
            }

            int day = dt.Day - i;

            DateTime dt2 = new DateTime(dt.Year, dt.Month, day);

            //UpdatePanel
            UpdatePanel(dt2);
        }
    }
}
