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
    /// Interaction logic for ContentReservations.xaml
    /// </summary>
    public partial class ContentReservations : UserControl
    {

        public ContentReservations()
        {
            InitializeComponent();


        }

        internal void InitializeContent()
        {
            CalendarPanel.InitializeCalendar();
            AgendaPanel.InitializeAgenda();
            CalendarPanel.updateAgendaPanel += UpdateAgendaPanel;

        }

        private void UpdateAgendaPanel(DateTime startDate)
        {
            AgendaPanel.UpdatePanel(startDate);
        }

    }
}
