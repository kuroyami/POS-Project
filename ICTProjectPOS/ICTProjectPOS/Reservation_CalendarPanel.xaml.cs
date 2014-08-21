using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Reservation_CalendarPanel.xaml
    /// </summary>
    public partial class Reservation_CalendarPanel : UserControl
    {
        DateTime selectedDate;

        public delegate void UpdateAgendaPanel(DateTime startDate);
        public UpdateAgendaPanel updateAgendaPanel;

        public Reservation_CalendarPanel()
        {
            InitializeComponent();

            //TODO
            //

            // DONE Route Prev/NextMonth Button to change 'heading' only
            // DONE Route Prev/NextMonth Button to change date as well

            // DONE elapsed days have light gray background
            // Show Today have green background

            // DONE Enable click on grid, clicking grid adds an overlay of green bar
            //Output selected week's date to AgendaPanel
        }

        internal void InitializeCalendar()
        {
            selectedDate = DateTime.Today;

            UpdateDateGrid();
            InitializeIndicator();

        }


        internal TextBlock[,] GetDateGrid()
        {
            TextBlock[,] dateGrid = {
                                        {TextBlock_Date_00, TextBlock_Date_10, TextBlock_Date_20, TextBlock_Date_30, TextBlock_Date_40, TextBlock_Date_50, TextBlock_Date_60},
                                        {TextBlock_Date_01, TextBlock_Date_11, TextBlock_Date_21, TextBlock_Date_31, TextBlock_Date_41, TextBlock_Date_51, TextBlock_Date_61},
                                        {TextBlock_Date_02, TextBlock_Date_12, TextBlock_Date_22, TextBlock_Date_32, TextBlock_Date_42, TextBlock_Date_52, TextBlock_Date_62},
                                        {TextBlock_Date_03, TextBlock_Date_13, TextBlock_Date_23, TextBlock_Date_33, TextBlock_Date_43, TextBlock_Date_53, TextBlock_Date_63},
                                        {TextBlock_Date_04, TextBlock_Date_14, TextBlock_Date_24, TextBlock_Date_34, TextBlock_Date_44, TextBlock_Date_54, TextBlock_Date_64},
                                        {TextBlock_Date_05, TextBlock_Date_15, TextBlock_Date_25, TextBlock_Date_35, TextBlock_Date_45, TextBlock_Date_55, TextBlock_Date_65}
                                    };

            return dateGrid;

        }

        internal Rectangle[] GetRectangleIndicators()
        {
            Rectangle[] indicators = {Rectangle_Indicator_Row0, Rectangle_Indicator_Row1, Rectangle_Indicator_Row2, 
                                         Rectangle_Indicator_Row3, Rectangle_Indicator_Row4, Rectangle_Indicator_Row5};
            return indicators;
        }


        private void UpdateDateGrid()
        {
            String[] today = DateTime.Now.ToString("d").Split('/');

            int todayDay = Int32.Parse(today[0]);
            int todayMonth = Int32.Parse(today[1]);
            int todayYear = Int32.Parse(today[2]);

            String[] date = selectedDate.ToString("d").Split('/');


            int selectedMonth = Int32.Parse(date[1]);
            int selectedYear = Int32.Parse(date[2]);

            TextBlock[,] dateGrid = GetDateGrid();

            int dateAdjustment = GetDateAjustment(selectedYear, selectedMonth);
            int endOfMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);

            //Set TextBlock Text for current month's date
            int i = 1 - dateAdjustment;

            for (int y = 0; y < 6 ; y++)
            {
                for (int x = 0; x < 7; x++)
                {

                    if ((i > 0) && (i < endOfMonth + 1)) dateGrid[y, x].Text = i.ToString();

                    if (selectedYear < todayYear)
                    {
                        dateGrid[y, x].Style = (Style)FindResource("Date_CurrentMonth_Elapsed");
                    }
                    else if (selectedYear > todayYear)
                    {
                        dateGrid[y, x].Style = (Style)FindResource("Date_CurrentMonth");
                    }
                    else
                    {
                        //If Previous Month is shown, Update all Grid background to light gray 
                        if (selectedMonth < todayMonth) dateGrid[y, x].Style = (Style)FindResource("Date_CurrentMonth_Elapsed");

                        //Else if Next Month is shown, Update all Grid background to white 
                        else if (selectedMonth > todayMonth) dateGrid[y, x].Style = (Style)FindResource("Date_CurrentMonth");
                        //Else
                        else
                        {
                            //Update Elapsed days' Grid background to Light gray
                            if ((selectedMonth == todayMonth) && (selectedYear == todayYear) && (i < todayDay)) dateGrid[y, x].Style = (Style)FindResource("Date_CurrentMonth_Elapsed");
                            //Update Today's Grid background to Green
                            else if ((selectedMonth == todayMonth) && (selectedYear == todayYear) && (i == todayDay)) dateGrid[y, x].Style = (Style)FindResource("Date_Today");
                            //Update Future day's background to White
                            else dateGrid[y, x].Style = (Style)FindResource("Date_CurrentMonth");
                        }
                    }

                    i++;

                }
            }

            //Set TextBlock text for previous month's date
            int previousMonth = selectedMonth - 1;
            if (previousMonth < 1) previousMonth = 12;

            int endOfPreviousMonth = DateTime.DaysInMonth(selectedYear, previousMonth);

            int i2 = endOfPreviousMonth - dateAdjustment;

            for (int x2 = 0; x2 < dateAdjustment; x2++)
            {
                dateGrid[0, x2].Text = i2.ToString();

                //If selectedYear is > todayYear, or [selectedMonth > todayMonth && selectedYear == todayYear], change Grid background to White
                if ((selectedYear > todayYear) || ((selectedMonth > todayMonth) && (selectedYear == todayYear))) dateGrid[0, x2].Style = (Style)FindResource("Date_NextMonth");
                //Else, Update Elapsed days' Grid background to Light gray
                else dateGrid[0, x2].Style = (Style)FindResource("Date_PreviousMonth_Elapsed");

                i2++;
            }

            //Set TextBlock text for next month's date

            int startOfNextMonth = dateAdjustment + endOfMonth;

            int i3 = 1 - startOfNextMonth % 7;

            for (int y3 = startOfNextMonth / 7; y3 < 6; y3++)
            {
                for (int x3 = 0; x3 < 7; x3++)
                {

                    if (i3 > 0)
                    {
                        dateGrid[y3, x3].Text = i3.ToString();

                        //If selectedYear is < todayYear, or [selectedMonth < todayMonth && selectedYear == todayYear], change Grid background to Light Gray
                        if ((selectedYear < todayYear) || ((selectedMonth < todayMonth) && (selectedYear == todayYear))) dateGrid[y3, x3].Style = (Style)FindResource("Date_PreviousMonth_Elapsed");
                        //Else, Update Future day's background to White
                        else dateGrid[y3, x3].Style = (Style)FindResource("Date_NextMonth");
                    }

                    i3++;
                }
            }

            //Update Month and Year Heading
            string heading = selectedDate.ToString("MMMM") + " " + selectedDate.ToString("yyyy");
            TextBlock_MonthYear.Text = heading.ToUpper();

        }

        private int GetDateAjustment(int selectedYear, int selectedMonth)
        {
            int dateAdjustment = 0;

            DateTime dt = new DateTime(selectedYear, selectedMonth, 1);

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dateAdjustment = 0;
                    break;
                case DayOfWeek.Tuesday:
                    dateAdjustment = 1;
                    break;
                case DayOfWeek.Wednesday:
                    dateAdjustment = 2;
                    break;
                case DayOfWeek.Thursday:
                    dateAdjustment = 3;
                    break;
                case DayOfWeek.Friday:
                    dateAdjustment = 4;
                    break;
                case DayOfWeek.Saturday:
                    dateAdjustment = 5;
                    break;
                case DayOfWeek.Sunday:
                    dateAdjustment = 6;
                    break;
            }

            return dateAdjustment;
        }

        private void Button_PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            selectedDate = selectedDate.AddMonths(-1);
            UpdateDateGrid();
            UpdateIndicator();
        }

        private void Button_NextMonth_Click(object sender, RoutedEventArgs e)
        {
            selectedDate = selectedDate.AddMonths(1);
            UpdateDateGrid();
            UpdateIndicator();
        }

        private void Week_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle indicator = sender as Rectangle;
            SetSelectedWeekIndicator(indicator);

            updateAgendaPanel(GetStartDate());
        }

        private DateTime GetStartDate()
        {
            Rectangle[] rects = {Rectangle_Indicator_Row0, Rectangle_Indicator_Row1, Rectangle_Indicator_Row2, Rectangle_Indicator_Row3,
                                Rectangle_Indicator_Row4, Rectangle_Indicator_Row5};

            CultureInfo provider = CultureInfo.InvariantCulture;


            //GET MONTH & YEAR
            string monthYear = TextBlock_MonthYear.Text;

            int year = Int32.Parse(monthYear.Split(' ')[1]);

            int month = 0;

            string format = "MMMM";

            try
            {
                month = DateTime.ParseExact(monthYear.Split(' ')[0], format, provider).Month;
            }
            catch
            {

            }

            

            //GET DATE
            int row = 0;

            foreach (Rectangle rect in rects)
            {
                if (rect.Style == (Style)FindResource("Week_Selected"))
                {
                    row = Grid.GetRow(rect);
                }
            }

            string textBlock_name = String.Format("TextBlock_Date_0{0}", row.ToString()); 

            object item = _Grid.FindName(textBlock_name);

            TextBlock tb = (TextBlock)item;

            int day = Int32.Parse(tb.Text);


            //IN CASE DATE IS FOR PREVIOUS / NEXT MONTH'S
            if (row == 0)
            {
                if (day > 7)
                {
                    month--;
                }
            }
            else if (row > 3)
            {
                if (day < 9)
                {
                    month++;
                }
            }

            DateTime dt = new DateTime(year, month, day);

            return dt;
        }

        private void InitializeIndicator()
        {
            String today = DateTime.Today.ToString("d");

            int day = Int32.Parse(today.Split('/')[0]);

            int rowIndex = day / 7;

            SetSelectedWeekIndicator(GetRectangleIndicators()[rowIndex]);
        }

        private void UpdateIndicator()
        {
            Rectangle[] indicators = GetRectangleIndicators();

            int i = 0;

            foreach (Rectangle rect in indicators)
            {
                if (rect.Style == (Style)FindResource("Week_Selected")) break;
                
                i++;

            }

            SetSelectedWeekIndicator(GetRectangleIndicators()[i]);

        }

        private void SetSelectedWeekIndicator(Rectangle indicator)
        {
            Rectangle[] rects = GetRectangleIndicators();

            foreach (Rectangle rect in rects)
            {
                rect.Style = (Style)FindResource("Week_Transparent");
            }

            indicator.Style = (Style)FindResource("Week_Selected");
        }

    }
}
