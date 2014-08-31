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

        Reservation_Blueprint reservation_blueprint;
        Reservation_Form reservation_form;

        public ContentReservations()
        {
            InitializeComponent();


        }

        internal void InitializeContent()
        {
            CalendarPanel.InitializeCalendar();
            AgendaPanel.InitializeAgenda();
            CalendarPanel.updateAgendaPanel += UpdateAgendaPanel;
            AgendaPanel.inflateBlueprintPanel += InflateBlueprintPanel;

            reservation_blueprint = new Reservation_Blueprint();
            reservation_blueprint.Margin = new Thickness(295, 50, 0, 0);
            reservation_blueprint.Visibility = Visibility.Hidden;
            reservation_blueprint.inflateReservationForm += inflateReservationForm;

            reservation_blueprint.popup.createNewReservation += CreateNewReservation;

            reservation_form = new Reservation_Form();
            reservation_form.Margin = new Thickness(50, 120, 0, 0);
            reservation_form.cancelButtonClick += CloseReservationForm;
            reservation_form.updateAccdbRelatedUI += UpdateAccdbRelatedUI;
            reservation_form.Visibility = Visibility.Hidden;
            _Canvas.Children.Add(reservation_form);

            reservation_blueprint.closeButtonClicked += CloseUserControl;
            _Canvas.Children.Add(reservation_blueprint);


        }

        private void UpdateAccdbRelatedUI()
        {
            reservation_blueprint.UpdateBlueprint();
            AgendaPanel.UpdatePanel();
        }

        private void CreateNewReservation(string tableNum, string date, string time)
        {
            reservation_form.Visibility = Visibility.Visible;
            reservation_form.CreateNewReservation(tableNum, date, time);
            reservation_blueprint.popup.Margin = new Thickness(-520, -115, 0, 0);
            reservation_blueprint.Margin = new Thickness(520, 50, 0, 0);
        }

        private void inflateReservationForm(int reservationNum)
        {
            reservation_form.Visibility = Visibility.Visible;
            reservation_form.InitializeForm(reservationNum);
            reservation_blueprint.popup.Margin = new Thickness(-520, -115, 0, 0);
            reservation_blueprint.Margin = new Thickness(520, 50, 0, 0);
        }



        private void UpdateAgendaPanel(DateTime startDate)
        {
            AgendaPanel.UpdatePanel(startDate);
        }

        private void InflateBlueprintPanel(string date, string time)
        {
            reservation_blueprint.Visibility = Visibility.Visible;

            _GreyBackdrop.Visibility = Visibility.Visible;
           
            reservation_blueprint.InitializePanel(date, time);
        }

        private void CloseReservationForm()
        {
            reservation_form.Visibility = Visibility.Hidden;
            reservation_blueprint.Margin = new Thickness(295, 50, 0, 0);
            reservation_blueprint.popup.Margin = new Thickness(-295, -115, 0, 0);
        }

        private void CloseUserControl(UserControl userControl)
        {
            userControl.Visibility = Visibility.Hidden;
            reservation_form.Visibility = Visibility.Hidden;
            _GreyBackdrop.Visibility = Visibility.Hidden;
        }

    }
}
