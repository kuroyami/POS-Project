using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTProjectPOS
{
    class TableOrder
    {
        string paymentMethod;
        int tableNumber;
        int tableStatus = TABLE_EMPTY;
        Collection<Item> itemsOrdered;

        public static int TABLE_EMPTY = 0;
        public static int OPEN_BILL = 1;
        public static int PRESETTLEMENT_BILL = 2;
        public static int CLOSED_BILL = 3;

        //
        //
        // WILSON - START
        //
        //

        public TableOrder(int tableNumber)
        {
            this.tableNumber = tableNumber;
            itemsOrdered = new Collection<Item>();
        }

        internal int GetTableNumber()
        {
            return this.tableNumber;
        }

        internal void SetTableStatus(int tableStatus)
        {
            this.tableStatus = tableStatus;
        }

        internal int GetTableStatus()
        {
            return this.tableStatus;
        }

        public void AddItemOrder(string itemName, int itemQuantity, Double pricePerItem)
        {
            if (this.HasItem(itemName))
            {
                this.GetItem(itemName).IncreaseQuantity(itemQuantity);
            }
            else
            {
                Item item = new Item(itemName, itemQuantity, pricePerItem);
                itemsOrdered.Add(item);
            }

        }

        internal void RemoveItemOrder(TableRow tableRow)
        {
            int i = 0;

            foreach (Item item in itemsOrdered)
            {
                if (item.GetName() == tableRow.GetContent(TableRow.TABLE_ORDER, TableRow.GET_ITEM_NAME))
                {
                    break;
                }
                else
                {
                    i++;
                }
            }

            System.Diagnostics.Debug.WriteLine(itemsOrdered.Count());
            itemsOrdered.RemoveAt(i);
            System.Diagnostics.Debug.WriteLine(itemsOrdered.Count());

        }

        internal bool HasItem(string itemName)
        {
            if (this.GetItem(itemName) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal Item GetItem(String itemName)
        {

            foreach (Item item in itemsOrdered)
            {
                if (item.GetName() == itemName)
                {
                    return item;
                }
            }

            return null;

        }

        internal Collection<Item> GetItemsOrdered()
        {
            return this.itemsOrdered;
        }

        internal String GetSumTaxTotal()
        {
            
            //SUM
            double sum = 0.0;
            foreach (Item item in itemsOrdered)
            {
                sum += item.GetSubtotal();
            }

            //TAX & SERVICE --- assuming tax is 10%
            double tax = (sum * 10) / 100;

            //TOTAL
            double total = sum + tax;

            String str = String.Format("{0:0.00},{1:0.00},{2:0.00}", sum, tax, total);

            return str;
        }

        internal void ClearTableOrder()
        {
            this.itemsOrdered = new Collection<Item>();
        }

        internal string GetContent()
        {
            String str = "";

            foreach (Item item in itemsOrdered)
            {
                str += item.GetContent() + "\n";
            }

            return str;
        }

        internal string GetAmountDue()
        {
            string[] amountDue = this.GetSumTaxTotal().Split(',');

            return amountDue[2];
        }

        internal void SetPaymentMethod(string paymentMethod)
        {
            this.paymentMethod = paymentMethod;
        }

        internal string GetPaymentMethod()
        {
            return this.paymentMethod;
        }
    }

    class Item
    {
        String itemName;
        int itemQuantity;
        Double itemPrice;
        Double itemSubtotal;

        internal Item(string itemName, int itemQuantity, Double pricePerItem)
        {
            this.itemName = itemName;
            this.itemQuantity = itemQuantity;
            this.itemPrice = pricePerItem;
            this.itemSubtotal = (itemPrice * itemQuantity); 
        }

        internal String GetName()
        {
            return itemName;
        }


        internal void IncreaseQuantity(int itemQuantity)
        {
            System.Diagnostics.Debug.WriteLine("qty to add:" + itemQuantity);

            this.itemQuantity += itemQuantity;
            this.itemSubtotal = (this.itemPrice * this.itemQuantity);
        }

        internal String GetContent()
        {
            string str = itemName + "," +
                itemQuantity + "," +
                itemPrice + "," +
                itemSubtotal;

            return str;
        }


        internal double GetSubtotal()
        {
            return this.itemSubtotal;
        }

        internal int GetQuantity()
        {
            return this.itemQuantity;
        }
    }

    //
    //
    // WILSON - END
    //
    //

}
