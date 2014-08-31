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
    /// Interaction logic for Reservation_WaitingListPanel_Table.xaml
    /// </summary>
    public partial class Reservation_WaitingListPanel_Table : UserControl
    {
        List<WaitingList> waitingList;

        int startIndex = 0;

        public int highlightedRow = 0;

        internal delegate void EnableCreateButton(bool trueOrFalse);
        internal EnableCreateButton enableCreateButton;

        public Reservation_WaitingListPanel_Table()
        {
            InitializeComponent();
        }

        internal void UpdateTable(List<WaitingList> waitingList)
        {
            this.waitingList = waitingList;

            UpdateTable();
        }

        internal void UpdateTable()
        {
            ResetTableHighlights();

            TextBlock[,] table = GetTable();


            for (int i = 0; i < 8; i++)
            {
                try
                {
                    table[i, 0].Text = waitingList[this.startIndex + i].listNum;
                    table[i, 1].Text = waitingList[this.startIndex + i].firstName + " " + waitingList[this.startIndex + i].lastName;
                    table[i, 2].Text = waitingList[this.startIndex + i].resDate;
                }
                catch
                {
                    table[i, 0].Text = "";
                    table[i, 1].Text = "";
                    table[i, 2].Text = "";
                }
            }

            
        }

        TextBlock[,] GetTable()
        {
            TextBlock[,] table = {{TextBlock_10, TextBlock_11, TextBlock_12}, 
                                 {TextBlock_20, TextBlock_21, TextBlock_22},
                                 {TextBlock_30, TextBlock_31, TextBlock_32},
                                 {TextBlock_40, TextBlock_41, TextBlock_42},
                                 {TextBlock_50, TextBlock_51, TextBlock_52},
                                 {TextBlock_60, TextBlock_61, TextBlock_62},
                                 {TextBlock_70, TextBlock_71, TextBlock_72},
                                 {TextBlock_80, TextBlock_81, TextBlock_82},};
            return table;
        }

        internal void ChangePage(int nextOrPrevious)
        {
            if (nextOrPrevious == 1)
            {
                this.startIndex += 8;
            }
            else
            {
                this.startIndex -= 8;
            }

            UpdateTable();
        }

        private void Table_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResetTableHighlights();

            TextBlock textBlock = (TextBlock)sender;

            this.highlightedRow = Grid.GetRow(textBlock);

            TextBlock[,] table = GetTable();

            for (int i = 0; i < 3; i++)
            {
                table[this.highlightedRow - 1, i].Style = (Style)FindResource("Table_Style_Highlight");
            }

            enableCreateButton(true);
        }

        private void ResetTableHighlights()
        {
            TextBlock[,] table = GetTable();

            for (int i = 0; i < 8; i++)
            {
                string style = "Table_Style_1";

                if ((i % 2) == 0)
                {
                    style = "Table_Style_1";
                }
                else
                {
                    style = "Table_Style_2";
                }

                for (int j = 0; j < 3; j++)
                {

                    table[i, j].Style = (Style) FindResource(style);
                }
            }

            enableCreateButton(false);
            this.highlightedRow = 0;
        }

        internal TextBlock GetTextBlock(int row, int column)
        {
            TextBlock[,] table = GetTable();

            return table[row -1, column];
        }
    }
}
