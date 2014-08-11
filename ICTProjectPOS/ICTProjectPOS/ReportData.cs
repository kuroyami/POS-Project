using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTProjectPOS
{
    class ReportData_Invoice
    {
        string invoiceNumber;
        string timeStamp;
        string itemsOrdered;
        string amountDue;
        string paymentMethod;

        Collection<ReportData_InvoiceItem> invoiceItemCollection;

        //
        //
        // WILSON - START
        //
        //

        public ReportData_Invoice()
        {
            invoiceItemCollection = new Collection<ReportData_InvoiceItem>();
        }

        public void SetInvoiceNumber(string invoiceNumber)
        {
            this.invoiceNumber = invoiceNumber;
        }

        public void SetTimeStamp(string timeStamp)
        {
            this.timeStamp = timeStamp;
        }

        public void SetAmountDue(string amountDue)
        {
            this.amountDue = amountDue;
        }

        public void SetPaymentMethod(string paymentMethod)
        {
            this.paymentMethod = paymentMethod;
        }

        public string GetInvoiceNumber()
        {
            return this.invoiceNumber;
        }

        public string GetTimeStamp()
        {
            return this.timeStamp;
        }

        public string GetItemsOrdered()
        {
            return this.itemsOrdered;
        }

        public string GetAmountDue()
        {
            return this.amountDue;
        }

        public string GetPaymentMethod()
        {
            return this.paymentMethod;
        }

        public Collection<ReportData_InvoiceItem> GetInvoiceItemCollection()
        {
            return this.invoiceItemCollection;
        }

        public void AddInvoiceItem(ReportData_InvoiceItem invoiceItem) 
        {
            this.invoiceItemCollection.Add(invoiceItem);

            UpdateItemsOrdered();
        }

        private void UpdateItemsOrdered()
        {
            int itemsOrdered = 0;

            foreach (ReportData_InvoiceItem item in this.invoiceItemCollection)
            {
                itemsOrdered += item.GetQuantity();
            }

            this.itemsOrdered = itemsOrdered.ToString();
        }

    }

    class ReportData_InvoiceItem
    {
        String itemId;
        int quantity;
        double pricePerItem;

        public ReportData_InvoiceItem()
        {

        }

        public ReportData_InvoiceItem(string itemId, int quantity)
        {
            this.itemId = itemId;
            this.quantity = quantity;
        }

        public String GetItemId()
        {
            return this.itemId;
        }

        public int GetQuantity()
        {
            return this.quantity;
        }

        internal void SetPricePerItem(string pricePerItem)
        {
            this.pricePerItem = Double.Parse(pricePerItem);
        }

        internal Double GetPricePerItem()
        {
            return this.pricePerItem;
        }

        //
        //
        // WILSON - END
        //
        //
    }
}
