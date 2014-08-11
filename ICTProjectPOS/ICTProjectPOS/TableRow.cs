using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTProjectPOS
{
    class TableRow
    {

        public static int TABLE_ORDER = 0;
        public static int ADD_ITEM = 1;

        public static int GET_ROW_NUMBER = 0;
        public static int GET_ITEM_NAME = 1;
        public static int GET_ITEM_PRICE = 2;
        public static int GET_ITEM_QUANTITY = 3;

        string contentRowNumber;
        string contentItemName;
        string contentItemPrice;
        string contentItemQuantity;
        string contentItemSubtotal;

        int rowIndex;
        string backgroundStyle;
        string textStyle;
        int columnCount;

        //
        //
        // CARLO - START
        //
        //

        public TableRow(int callingObject, int rowIndex)
        {

            this.rowIndex = rowIndex;

            if (callingObject == TABLE_ORDER)
            {
                this.columnCount = 5;
            }
            else if (callingObject == ADD_ITEM)
            {
                this.columnCount = 3;
            }

            if (rowIndex % 2 == 0)
            {
                this.backgroundStyle = "TableRowBackgroundStyle2";
            }
            else
            {
                this.backgroundStyle = "TableRowBackgroundStyle1";
            }

            this.textStyle="TableTextNormalStyle";

            if (rowIndex == 0)
            {
                this.backgroundStyle = "TableHeaderBackgroundStyle";
                this.textStyle = "TableHeaderStyle";
            }

        }

        internal void SetBackgroundStyle(String style)
        {
            if (style.Equals("default"))
            {
                if (this.rowIndex % 2 == 0)
                {
                    this.backgroundStyle = "TableRowBackgroundStyle2";
                }

                else
                {
                    this.backgroundStyle = "TableRowBackgroundStyle1";
                }

                if (this.rowIndex == 0)
                {
                    this.backgroundStyle = "TableHeaderBackgroundStyle";
                }
            }
            
            else if (style.Equals("highlight"))
            {
                this.backgroundStyle = "TableRowBackgroundHighlightStyle";
            }

            else
            {
                this.backgroundStyle = style;
            }

        }

        internal int GetColumnCount()
        {
            return this.columnCount;
        }

        internal string GetBackgroundStyle()
        {
            return this.backgroundStyle;
        }

        internal int GetRowIndex()
        {
            return this.rowIndex;
        }

        internal void SetContent(int callingObject, string commaSeparatedString)
        {
            string[] strings = commaSeparatedString.Split(',');

            if (callingObject == TABLE_ORDER)
            {
                contentRowNumber = strings[0];
                contentItemName = strings[1];
                contentItemQuantity = strings[2];
                contentItemPrice = strings[3];
                contentItemSubtotal = strings[4];
            }
            else if (callingObject == ADD_ITEM)
            {
                contentRowNumber = strings[0];
                contentItemName = strings[1];
                contentItemPrice = strings[2];
            }

        }

        internal string GetContent(int callingObject, int columnIndex)
        {
            String str = "";

            if (callingObject == TABLE_ORDER)
            {
                switch (columnIndex)
                {
                    case 0:
                        str = this.contentRowNumber;
                        break;
                    case 1:
                        str = this.contentItemName;
                        break;
                    case 2:
                        str = this.contentItemQuantity;
                        break;
                    case 3:
                        str = this.contentItemPrice;
                        break;
                    case 4:
                        str = this.contentItemSubtotal;
                        break;
                }
            }
            else if (callingObject == ADD_ITEM)
            {
                switch (columnIndex)
                {
                    case 0:
                        str = this.contentRowNumber;
                        break;
                    case 1:
                        str = this.contentItemName;
                        break;
                    case 2:
                        str = this.contentItemPrice;
                        break;
                }

            }

            return str;
        }

        internal Boolean HasContent()
        {
            bool b = false;

            try
            {
                if (this.contentItemName.Count() > 0)
                {
                    b = true;
                }

            } catch {

            }

            return b;
        }

        internal object GetTextStyle()
        {
            return this.textStyle;
        }


        internal void SetTextStyle(String style)
        {
            if (style.Equals("default"))
            {
                this.textStyle = "TableTextNormalStyle";
            } else if (style.Equals("highlight"))
            {
                this.textStyle = "TableTextHighlightStyle";
            }
            else
            {
                this.textStyle = style;
            }
        }

        internal void ResetContent(int callingObject)
        {
            this.textStyle = "TableTextNormalStyle";

            if (callingObject == TABLE_ORDER)
            {
                this.SetContent(TABLE_ORDER, ",,,,");
            }
            else if (callingObject == ADD_ITEM)
            {
                this.SetContent(ADD_ITEM, ",,");
            }
        }

        //
        //
        // CARLO - END
        //
        //
    }
}
