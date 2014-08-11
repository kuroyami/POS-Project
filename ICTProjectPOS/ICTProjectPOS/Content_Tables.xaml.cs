using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ContentTables.xaml
    /// </summary>
    public partial class Content_Tables : UserControl
    {
        object selectedTable;

        Form_TableOrder fTableOrder;
        Form_AddItem fAddItem;
        Form_Payment fPayment;

        Collection<TableOrder> tableOrderCollection;

        //
        //
        //  CARLO - START
        //
        //

        public Content_Tables()
        {
            InitializeComponent();

            tableOrderCollection = new Collection<TableOrder>();
            for (int i = 1; i <= 31; i++)
            {
                TableOrder to = new TableOrder(i);
                tableOrderCollection.Add(to);
            }

            fTableOrder = new Form_TableOrder();
            Parent_Grid.Children.Add(fTableOrder);

            fTableOrder.Visibility = Visibility.Hidden;

            fTableOrder.itemFormButtonClicked += InflateForm_AddItem;
            fTableOrder.paymentFormButtonClicked += InflateForm_Payment;
            fTableOrder.closeFormButtonClicked += CloseForm;
            fTableOrder.deleteItem += DeleteItemFromTableOrder;
            fTableOrder.disableAddItemForm += DisableFormAddItem;
            fTableOrder.enableAddItemForm += EnableFormAddItem;


            fAddItem = new Form_AddItem();
            Parent_Grid.Children.Add(fAddItem);

            fAddItem.Visibility = Visibility.Hidden;

            fAddItem.addItemButtonClicked += AddItemToTableOrder;
            fAddItem.closeFormButtonClicked += CloseForm;


            fPayment = new Form_Payment();
            Parent_Grid.Children.Add(fPayment);

            fPayment.Visibility = Visibility.Hidden;
            fPayment.confrimButtonClicked += ProcessTransaction;
            fPayment.closeFormButtonClicked += CloseForm;

        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            InflateForm_TableOrder(Int32.Parse((sender as Button).Content.ToString()));

            selectedTable = sender;

        }

        private void InflateForm_TableOrder(int tableNumber)
        {

            Canvas.SetTop(fTableOrder, 40);
            Canvas.SetLeft(fTableOrder, 150);

            fTableOrder.Visibility = Visibility.Visible;

            GreyBackdrop.Visibility = Visibility.Visible;

            TableOrder tableOrder = GetTableOrderFromTableNumber(tableNumber);
            fTableOrder.EnableForm(tableOrder);
        }

        private void InflateForm_Payment(object sender)
        {

            Canvas.SetTop(fPayment, 40);
            Canvas.SetLeft(fPayment, 1270);

            fPayment.Visibility = Visibility.Visible;

            int tableNumber = Int32.Parse(fTableOrder.TextBlock_TableNum.Text.Replace("TABLE ", ""));
            TableOrder tableOrder = GetTableOrderFromTableNumber(tableNumber);

            fPayment.EnableForm();

            fPayment.SetAmountDue(tableOrder.GetSumTaxTotal());

        }

        private void InflateForm_AddItem(object sender)
        {

            Canvas.SetTop(fAddItem, 40);
            Canvas.SetLeft(fAddItem, 710);

            fAddItem.Visibility = Visibility.Visible;

            fAddItem.EnableForm();
        }


        private void EnableFormAddItem()
        {
            fAddItem.EnableForm();
        }

        private void DisableFormAddItem()
        {
            fAddItem.DisableForm();
        }


        //
        //
        //  CARLO - END
        //
        //

        //
        //
        //  VINCENT - START
        //
        //


        private void ProcessTransaction()
        {
            TableOrder tableOrder = fTableOrder.GetTableOrder();
            tableOrder.SetPaymentMethod(fPayment.TextBlock_PaymentMethod.Text);

            System.Diagnostics.Debug.WriteLine(tableOrder.GetPaymentMethod());

            //Set fTableOrder visuals
            fTableOrder.DisableForm();

            //Save Transaction to accdb Database
            SaveTransactionToDatabase();

            //Clear TableOrder temporary database
            fTableOrder.GetTableOrder().ClearTableOrder();
            fTableOrder.GetTableOrder().SetTableStatus(TableOrder.CLOSED_BILL);
        }

        private void SaveTransactionToDatabase()
        {

            TableOrder tableOrder = fTableOrder.GetTableOrder();

            String path = Assembly.GetExecutingAssembly().Location;

            System.Diagnostics.Debug.WriteLine("PATH: " + path);

            path = path.Replace("bin\\Debug\\ICTProjectPOS.exe", "RestaurantDB.accdb");

            System.Diagnostics.Debug.WriteLine("PATH: " + path);

            String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", path);

            OleDbConnection connection = new OleDbConnection(connectionString);

            //OPEN CONNECTION
            connection.Open();
            
            String employeeID = "EMP001";

            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;

            OleDbCommand cmd = new OleDbCommand();
            
            cmd.CommandText = "SELECT COUNT(*) FROM Invoice";
            cmd.Connection = connection;

            int invoiceNumber = (int)cmd.ExecuteScalar() + 1;

            cmd.CommandText = "INSERT INTO Invoice([InvoiceNum],[TableNum],[EmployeeID], [DineDate], [DineTime], [TotalOrder], [PayMethod]) VALUES (?, ?, ?, ?, ?, ?, ?)";

            cmd.Parameters.AddWithValue("InvoiceNum", invoiceNumber);
            cmd.Parameters.AddWithValue("TableNum", tableOrder.GetTableNumber());
            cmd.Parameters.AddWithValue("EmployeeId", employeeID);
            cmd.Parameters.AddWithValue("DineDate", dateTime.Date);
            cmd.Parameters.AddWithValue("DineTime", dateTime.TimeOfDay);
            cmd.Parameters.AddWithValue("TotalOrder", tableOrder.GetAmountDue());
            cmd.Parameters.AddWithValue("PayMethod", tableOrder.GetPaymentMethod());
            
            cmd.ExecuteNonQuery();

            connection.Close();

            connection.Open();

            foreach (Item item in tableOrder.GetItemsOrdered())
            {
                OleDbCommand cmd2 = new OleDbCommand();
                cmd2.Connection = connection;

                cmd2.CommandText = "SELECT * FROM Item WHERE ItemName LIKE ?";

                cmd2.Parameters.AddWithValue("ItemName", "%" + item.GetName() + "%");

                string itemID = "";

                OleDbDataReader dr = cmd2.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        itemID = dr[0].ToString();
                    }
                }


                OleDbCommand cmd3 = new OleDbCommand();
                cmd3.Connection = connection;

                cmd3.CommandText = "INSERT INTO InvoiceItem ([InvoiceNum], [ItemID], [Quantity]) Values (?, ?, ?)";

                cmd3.Parameters.AddWithValue("@InvoiceNum", invoiceNumber);
                cmd3.Parameters.AddWithValue("@ItemID", itemID);
                cmd3.Parameters.AddWithValue("@Quantity", item.GetQuantity());
                
                cmd3.ExecuteNonQuery();

            }


            //CLOSE CONNECTION
            connection.Close();
            
        }

        //
        //
        //  VINCENT - END
        //
        //

        //
        //
        //  WILSON - START
        //
        //


        private void AddItemToTableOrder(string commaSeparatedItemDetails)
        {
            //Get tableorder from tableordercollection using table number
            int tableNumber = Int32.Parse(fTableOrder.TextBlock_TableNum.Text.Replace("TABLE ",""));

            String[] itemDetails = commaSeparatedItemDetails.Split(',');

            TableOrder tableOrder = GetTableOrderFromTableNumber(tableNumber);

            //add item to tableorder
            tableOrder.AddItemOrder(itemDetails[0],
                                        Int32.Parse(itemDetails[1]),
                                        Double.Parse(itemDetails[2]));

            //update ui element
            fTableOrder.TableTableOrder.ConvertTableOrderToTableRow(tableOrder);

            fTableOrder.UpdateSumTaxTotal(tableOrder.GetSumTaxTotal());
            fTableOrder.TogglePrintDraftButtonState();

            if (tableOrder.GetItemsOrdered().Count > 0)
            {
                tableOrder.SetTableStatus(TableOrder.OPEN_BILL);
            }

        }

        internal void DeleteItemFromTableOrder(TableRow tableRow)
        {
            //remove item from TableOrder
            int tableNumber = Int32.Parse(fTableOrder.TextBlock_TableNum.Text.Replace("TABLE ", ""));

            TableOrder tableOrder = GetTableOrderFromTableNumber(tableNumber);

            tableOrder.RemoveItemOrder(tableRow);

            //UpdateUIElements
            fTableOrder.TableTableOrder.ConvertTableOrderToTableRow(tableOrder);

            fTableOrder.UpdateSumTaxTotal(tableOrder.GetSumTaxTotal());
            fTableOrder.TogglePrintDraftButtonState();

            if (tableOrder.GetItemsOrdered().Count < 1)
            {
                tableOrder.SetTableStatus(TableOrder.TABLE_EMPTY);
            }

        }

        private TableOrder GetTableOrderFromTableNumber(int tableNumber)
        {
            foreach (TableOrder tableOrder in tableOrderCollection)
            {
                if (tableOrder.GetTableNumber() == tableNumber)
                {
                    return tableOrder;
                } 
            }

            return null;
        }

        internal void CloseForm(UserControl userControl)
        {
            //Parent_Grid.Children.Remove(userControl);

            userControl.Visibility = Visibility.Hidden;

            TableOrder tableOrder = fTableOrder.GetTableOrder();

            //Closing Form_TableOrder will also close Form_AddItem and Form_Payment (if they are inflated)
            if (userControl.Name == "FormTableOrder") 
            {

                GreyBackdrop.Visibility = Visibility.Hidden;

                try
                {
                    CloseForm(fAddItem);
                    CloseForm(fPayment);
                }
                catch
                {
                    //nothing
                }

                //Change TableOrder.Status and table color
                Button buttonTable = (selectedTable as Button);
                
                int status = tableOrder.GetTableStatus();

                if (status == TableOrder.CLOSED_BILL)
                {
                    tableOrder.SetTableStatus(TableOrder.TABLE_EMPTY);
                    status = TableOrder.TABLE_EMPTY;
                }

                ChangeTableColor(buttonTable, status);

            }

            else if (userControl.Name == "FormAddItem")
            {
                if (tableOrder.GetTableStatus() == TableOrder.PRESETTLEMENT_BILL ||
                    tableOrder.GetTableStatus() == TableOrder.CLOSED_BILL)
                {
                  // do nothing  
                }
                else 
                {
                    fTableOrder.Button_ItemForm.IsEnabled = true;
                }  
            } 
        }

        private void ChangeTableColor(Button buttonTable, int status)
        {

            if (status == TableOrder.TABLE_EMPTY)
            {
                buttonTable.Style = (Style)FindResource("EmptyTableButtonStyle");
                System.Diagnostics.Debug.WriteLine("Table Empty");
            }
            else if (status == TableOrder.OPEN_BILL || status == TableOrder.PRESETTLEMENT_BILL)
            {
                buttonTable.Style = (Style)FindResource("OccupiedTableButtonStyle");
                System.Diagnostics.Debug.WriteLine("Table Occupied");
            }
        }

        //
        // WILSON - END
        //


    }
}
