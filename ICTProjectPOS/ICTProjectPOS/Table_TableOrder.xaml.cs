﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Table_TableOrder.xaml
    /// </summary>
    public partial class Table_TableOrder : UserControl
    {

        Collection<TableRow> table;

        internal delegate void ItemSelected(TableRow tableRow);
        internal ItemSelected itemSelected;

        //
        //
        // CARLO - START
        //
        //

        public Table_TableOrder()
        {
            InitializeComponent();
        }

        internal Border[,] GetBorder2D()
        {
            Border[,] border2D = 
            { 
                {Border_0_0, Border_0_1, Border_0_2, Border_0_3, Border_0_4}, 
                {Border_1_0, Border_1_1, Border_1_2, Border_1_3, Border_1_4},
                {Border_2_0, Border_2_1, Border_2_2, Border_2_3, Border_2_4},
                {Border_3_0, Border_3_1, Border_3_2, Border_3_3, Border_3_4},
                {Border_4_0, Border_4_1, Border_4_2, Border_4_3, Border_4_4},
                {Border_5_0, Border_5_1, Border_5_2, Border_5_3, Border_5_4},
                {Border_6_0, Border_6_1, Border_6_2, Border_6_3, Border_6_4},
                {Border_7_0, Border_7_1, Border_7_2, Border_7_3, Border_7_4},
                {Border_8_0, Border_8_1, Border_8_2, Border_8_3, Border_8_4},
                {Border_9_0, Border_9_1, Border_9_2, Border_9_3, Border_9_4},
                {Border_10_0, Border_10_1, Border_10_2, Border_10_3, Border_10_4},
                {Border_11_0, Border_11_1, Border_11_2, Border_11_3, Border_11_4},
            };

            return border2D;
        }

        internal TextBlock[,] GetTextBlock2D()
        {
            TextBlock[,] textBlock2D = 
            {
                {TextBlock_0_0, TextBlock_0_1, TextBlock_0_2, TextBlock_0_3, TextBlock_0_4},
                {TextBlock_1_0, TextBlock_1_1, TextBlock_1_2, TextBlock_1_3, TextBlock_1_4},
                {TextBlock_2_0, TextBlock_2_1, TextBlock_2_2, TextBlock_2_3, TextBlock_2_4},
                {TextBlock_3_0, TextBlock_3_1, TextBlock_3_2, TextBlock_3_3, TextBlock_3_4},
                {TextBlock_4_0, TextBlock_4_1, TextBlock_4_2, TextBlock_4_3, TextBlock_4_4},
                {TextBlock_5_0, TextBlock_5_1, TextBlock_5_2, TextBlock_5_3, TextBlock_5_4},
                {TextBlock_6_0, TextBlock_6_1, TextBlock_6_2, TextBlock_6_3, TextBlock_6_4},
                {TextBlock_7_0, TextBlock_7_1, TextBlock_7_2, TextBlock_7_3, TextBlock_7_4},
                {TextBlock_8_0, TextBlock_8_1, TextBlock_8_2, TextBlock_8_3, TextBlock_8_4},
                {TextBlock_9_0, TextBlock_9_1, TextBlock_9_2, TextBlock_9_3, TextBlock_9_4},
                {TextBlock_10_0, TextBlock_10_1, TextBlock_10_2, TextBlock_10_3, TextBlock_10_4},
                {TextBlock_11_0, TextBlock_11_1, TextBlock_11_2, TextBlock_11_3, TextBlock_11_4},
            };

            return textBlock2D;
        }

        internal TextBlock[] GetItemTextBlock()
        {
            TextBlock[] textBlockArray = 
            { TextBlock_0_1, TextBlock_1_1, TextBlock_2_1, TextBlock_3_1, TextBlock_4_1,
                TextBlock_5_1,TextBlock_6_1,TextBlock_7_1,TextBlock_8_1,
                TextBlock_9_1,TextBlock_10_1,TextBlock_11_1};

            return textBlockArray;
        }


        private void Table_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeTable();
        }

        internal void InitializeTable()
        {
            table = new Collection<TableRow>();

            //Create Row Header
            TableRow tableRowHeader = new TableRow(TableRow.TABLE_ORDER, 0);

            //Add Content / Text
            tableRowHeader.SetContent(TableRow.TABLE_ORDER, "NO,ITEM,QTY,PRICE,SUBTOTAL");

            table.Add(tableRowHeader);

            //Special case for ITEM Header & ITEM Content left justification
            TextBlock[] itemTextBlocks = GetItemTextBlock();
            foreach (TextBlock tb in itemTextBlocks)
            {
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.Padding = new Thickness(10, 0, 0, 0);
            }


            //Create 11 Row(s)
            for (int i = 1; i < 12; i++)
            {
                TableRow tableRow = new TableRow(TableRow.TABLE_ORDER, i);
                table.Add(tableRow);
            }


            foreach (TableRow tableRow in table)
            {
                int rowIndex = tableRow.GetRowIndex();
                int numberOfColumns = tableRow.GetColumnCount();

                Border[,] border2D = GetBorder2D();
                TextBlock[,] textBlock2D = GetTextBlock2D();

                for (int column = 0; column < numberOfColumns; column++)
                {
                    //Update Border (Background)
                    border2D[rowIndex, column].Style = (Style)FindResource(tableRow.GetBackgroundStyle());
                    border2D[rowIndex, column].MouseLeftButtonDown += TableRow_Click;

                    //Update TextBlock (Content)
                    textBlock2D[rowIndex, column].Style = (Style)FindResource(tableRow.GetTextStyle());
                    textBlock2D[rowIndex, column].IsHitTestVisible = false;
                    textBlock2D[rowIndex, column].Text = tableRow.GetContent(TableRow.TABLE_ORDER, column);
                }
            }

        }

        internal void UpdateUIElement()
        {
            
            foreach (TableRow tableRow in table)
            {
                int rowIndex = tableRow.GetRowIndex();
                int numberOfColumns = tableRow.GetColumnCount();

                Border[,] border2D = GetBorder2D();
                TextBlock[,] textBlock2D = GetTextBlock2D();

                for (int column = 0; column < numberOfColumns; column++)
                {
                    //Update Border (Background)
                    border2D[rowIndex, column].Style = (Style)FindResource(tableRow.GetBackgroundStyle());

                    //Update TextBlock (Content)
                    textBlock2D[rowIndex, column].Style = (Style)FindResource(tableRow.GetTextStyle());
                    textBlock2D[rowIndex, column].Text = tableRow.GetContent(TableRow.TABLE_ORDER, column);

                }
            }

        }

        public void SetTableRowText(int rowIndex, string commaSeparatedString)
        {
            String[] strs = commaSeparatedString.Split(',');

            TableRow tableRow = table.ElementAt(rowIndex);
            tableRow.SetContent(TableRow.TABLE_ORDER, commaSeparatedString);

        }

        public void ResetTable()
        {
            foreach (TableRow tableRow in table)
            {
                if (tableRow.GetBackgroundStyle().Equals("TableHeaderBackgroundStyle"))
                {
                    //don't reset
                }
                else
                {
                    tableRow.ResetContent(TableRow.TABLE_ORDER);
                    tableRow.SetTextStyle("default");
                    tableRow.SetBackgroundStyle("default");
                }

            }

            UpdateUIElement();

        }

        private void TableRow_Click(object sender, MouseButtonEventArgs e)
        {

            int rowIndex = Grid.GetRow((UIElement)sender);

            if (rowIndex > 0) //for non-header row
            {
                TableRow tableRow = table.ElementAt(rowIndex);

                if (tableRow.HasContent())
                {
                    //RESET ALL ROW STYLE
                    //ResetRowBackground();

                    foreach (TableRow tr in table)
                    {
                        if (tr.GetRowIndex() > 0) //for non-header row
                        {
                            //Re-set background style for all rows to default
                            tr.SetBackgroundStyle("default");
                            tr.SetTextStyle("default");
                        }

                        //change selected row background to green
                        if (tr.GetRowIndex() == rowIndex)
                        {
                            tr.SetBackgroundStyle("highlight");
                            tr.SetTextStyle("highlight");
                        }
                    }

                    UpdateUIElement();

                    //Enable a few buttons
                    itemSelected(tableRow);
                }
            }
        }

        internal void ConvertTableOrderToTableRow(TableOrder tableOrder)
        {
            ResetTable();

            //Each item in itemsOrdered is 1 row

            int i = 1;

            foreach (Item item in tableOrder.GetItemsOrdered())
            {
                SetTableRowText(i, (i+","+item.GetContent()));

                i++;
            }

            UpdateUIElement();
        }

        //
        //
        // CARLO - END
        //
        //

    }
}
