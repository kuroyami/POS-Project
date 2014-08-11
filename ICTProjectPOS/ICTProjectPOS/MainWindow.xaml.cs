using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ContentSummary contentPanelSummary;
        Content_Tables cTables;
        ContentReservations contentPanelReservations;
        ContentReports contentPanelReports;

        //
        //
        // CARLO - START
        //
        //

        public MainWindow()
        {
            InitializeComponent();

            contentPanelSummary = new ContentSummary();
            cTables = new Content_Tables();
            contentPanelReservations = new ContentReservations();
            contentPanelReports = new ContentReports();

            titleBar.tabSelected += InflateContentPanel;
        }

        internal void InflateContentPanel(object sender)
        {
            Button btn = (Button)sender;
            String btnContent = (String)btn.Content;

            switch (btnContent)
            {
                case "SUMMARY":
                    this.ContentPanel.Content = contentPanelSummary;
                    break;
                case "TABLES":
                    this.ContentPanel.Content = cTables;
                    break;
                case "RESERVATIONS":
                    this.ContentPanel.Content = contentPanelReservations;
                    contentPanelReservations.InitializeContent();
                    break;
                case "REPORTS":
                    this.ContentPanel.Content = contentPanelReports;
                    contentPanelReports.InitializeContent();
                    break;
            }
        }

        //
        //
        // CARLO - END
        //
        //

    }
}
