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
    /// Interaction logic for Reservation_AgendaPanel.xaml
    /// </summary>
    public partial class Reservation_AgendaPanel : UserControl
    {
        public Reservation_AgendaPanel()
        {
            InitializeComponent();
        }

        internal void UpdatePanel(DateTime startDate)
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
