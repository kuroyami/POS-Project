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
using System.Data.OleDb;
using System.Data;
using System.Reflection;




namespace ICTProjectPOS
{
    /// <summary>
    /// Interaction logic for FormAddItem.xaml
    /// </summary>
    public partial class Form_AddItem : UserControl
    {

        public delegate void CloseFormButtonClicked(UserControl uc);
        public CloseFormButtonClicked closeFormButtonClicked;


        public delegate void AddItemButtonClicked(String commaSeparatedItemDetails);
        public AddItemButtonClicked addItemButtonClicked;

        TableRow selectedRow;

        public Form_AddItem()
        {
            InitializeComponent();

            TableAddItem.itemSelected += ItemSelected;

        }



        //
        //      VINCENT - START
        //      
        //      IMPORTANT! NEED TO DOWNLOAD: "Microsoft Access Database Engine 2010"
        //      GOOGLE IT AND DOWNLOAD THE x86 or x64 depending on YOUR system
        //  
        //

        private void ButtonCategory_Click(Object sender, RoutedEventArgs e){

            EnableAllCategoryButton(true); 

            //disable this button
            Button btn = (Button)sender;
            btn.IsEnabled = false;


            OleDbConnection cn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr;

            //open connection
            String path = Assembly.GetExecutingAssembly().Location;
            path = path.Replace("bin\\Debug\\ICTProjectPOS.exe", "RestaurantDB.accdb");
            String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", path);

            cn.ConnectionString = @connectionString;
            cmd.Connection = cn;

            TableAddItem.ResetTable();

            try
            {
                cmd = GenerateQueryFromSender(sender, cmd);
                cn.Open();
                dr = cmd.ExecuteReader();

                //add data to table
                if (dr.HasRows)
                {
                    int i = 1;

                    while (dr.Read())
                    {
                        //dr[0] = Item.ItemID dr[1] = Item.ItemName, dr[2] = Item.Price
                        string commaSeparatedString = String.Format("{0},{1},{2}", i, dr[1].ToString(), dr[2].ToString()).ToUpper();

                        TableAddItem.SetTableRowText(i, commaSeparatedString);

                        i++;
                    }

                    TableAddItem.UpdateUIElement();

                }

                //close connection
                dr.Close();
                cn.Close();

            }
            catch
            {
                cn.Close();
                System.Diagnostics.Debug.WriteLine("!!!ERROR IN RETREIVING DATA!!!");
            }

        }

        private OleDbCommand GenerateQueryFromSender(object sender, OleDbCommand cmd)
        {
            String categoryCode = "";
            Button btn = (Button)sender;
            String str = btn.Content.ToString();

            switch(str)
            {
                case "STARTER":
                    categoryCode = "FA";
                    TextBlock_ItemListHeader.Text = "FOOD | STARTER";
                    break;
                case "SOUP":
                    categoryCode = "FS";
                    TextBlock_ItemListHeader.Text = "FOOD | SOUP";
                    break;
                case "MAIN":
                    categoryCode = "FM";
                    TextBlock_ItemListHeader.Text = "FOOD | MAIN";
                    break;
                case "DESSERT":
                    categoryCode = "FD";
                    TextBlock_ItemListHeader.Text = "FOOD | DESSERT";
                    break;
                case "ICE CREAM":
                    categoryCode = "FI";
                    TextBlock_ItemListHeader.Text = "FOOD | ICE CREAM";
                    break;
                case "F OTHERS":
                    categoryCode = "FO";
                    TextBlock_ItemListHeader.Text = "FOOD | OTHERS";
                    break;
                case "WINE":
                    categoryCode = "BW";
                    TextBlock_ItemListHeader.Text = "BEVERAGE | WINE";
                    break;
                case "COCKTAIL":
                    categoryCode = "BC";
                    TextBlock_ItemListHeader.Text = "BEVERAGE | COCKTAIL";
                    break;
                case "JUICE":
                    categoryCode = "BJ";
                    TextBlock_ItemListHeader.Text = "BEVERAGE | JUICE";
                    break;
                case "B OTHERS":
                    categoryCode = "BO";
                    TextBlock_ItemListHeader.Text = "BEVERAGE | OTHERS";
                    break;
            }

            cmd.CommandText = "SELECT * FROM Item WHERE ItemID LIKE ?";
            cmd.Parameters.AddWithValue("ItemID", "%" + categoryCode + "%");

            return cmd;

        }

        //
        //      VINCENT - END
        //   

        //
        //      CARLO -START
        //

        internal void EnableAllCategoryButton(bool trueOrFalse)
        {
            Button[] categoryButtons = {Button_FoodCategory1,Button_FoodCategory2, Button_FoodCategory3, Button_FoodCategory4, Button_FoodCategory5, Button_FoodCategory6, 
                                               Button_BeverageCategory1, Button_BeverageCategory2, Button_BeverageCategory3, Button_BeverageCategory4 };

            foreach (Button btn in categoryButtons)
            {
                if (trueOrFalse) //if true
                {
                    btn.IsEnabled = true;
                    btn.Style = (Style)FindResource("ItemCategoryButtonStyle");
                    
                }
                else
                {
                    btn.Style = (Style)FindResource("GreenButtonStyle");
                    btn.IsEnabled = false;
                }
            }

        }

        private void ItemSelected(TableRow tableRow)
        {
            selectedRow = tableRow;
            TextBlock_ItemName.Text = tableRow.GetContent(TableRow.ADD_ITEM, TableRow.GET_ITEM_NAME);
            Button_DecreaseQty.IsEnabled = false;
            Button_IncreaseQty.IsEnabled = true;
            TextBlock_ItemQuantity.Text = "1";
            Button_AddItem.IsEnabled = true;

        }


        //
        //      CARLO - END
        //

        //
        //      WILSON - START
        //

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            closeFormButtonClicked(this);
        }

        private void Button_Quantity_Click(object sender, RoutedEventArgs e)
        {
            int quantity = Int32.Parse(TextBlock_ItemQuantity.Text);

            if ((sender as Button).Name == "Button_IncreaseQty")
            {
                quantity++;
            }
            else
            {
                quantity--;
            }

            if (quantity > 1)
            {
                Button_DecreaseQty.IsEnabled = true;
            }
            else
            {
                Button_DecreaseQty.IsEnabled = false;
            }

            TextBlock_ItemQuantity.Text = quantity.ToString();

        }

        internal void Button_AddItem_Click(object sender, RoutedEventArgs e)
        {

            String itemDetails = "";
            itemDetails += TextBlock_ItemName.Text + "," + TextBlock_ItemQuantity.Text + "," +
                selectedRow.GetContent(TableRow.ADD_ITEM, TableRow.GET_ITEM_PRICE);

            addItemButtonClicked(itemDetails); //itemName,itemQuantity,itemPrice
            
        }

        //
        //
        //      WILSON-END
        //
        //

        //
        //
        //      CARLO - START
        //
        //

        internal void DisableForm()
        {
            BrushConverter brush = new BrushConverter();
            this.BorderBrush = (Brush)brush.ConvertFrom("#cdcdcd");

            TextBlock_AddItemHeader.Opacity = 0.3;

            Rectangle1.Opacity = 0.3;
            Rectangle2.Opacity = 0.3;
            Rectangle3.Opacity = 0.3;

            TextBlock_FoodHeader.Opacity = 0.3;
            TextBlock_BeverageHeader.Opacity = 0.3;

            EnableAllCategoryButton(false);

            TextBlock_ItemListHeader.Text = "SELECT A CATEGORY";
            TextBlock_ItemListHeader.Opacity = 0.3;

            TableAddItem.ResetTable();
            TableAddItem.IsHitTestVisible = false;
            TableAddItem.Opacity = 0.3;

            TextBlock_ItemName.Text = "SELECT AN ITEM";
            TextBlock_ItemName.Opacity = 0.3;

            Button_DecreaseQty.IsEnabled = false;
            Button_IncreaseQty.IsEnabled = false;
            TextBlock_ItemQuantity.Text = "0";
            TextBlock_ItemQuantity.Opacity = 0.3;


            Button_AddItem.IsEnabled = false;

        }

        internal void EnableForm()
        {
            BrushConverter brush = new BrushConverter();
            this.BorderBrush = (Brush)brush.ConvertFrom("#518f64");

            TextBlock_AddItemHeader.Opacity = 1;

            Rectangle1.Opacity = 1;
            Rectangle2.Opacity = 1;
            Rectangle3.Opacity = 1;

            TextBlock_FoodHeader.Opacity = 1;
            TextBlock_BeverageHeader.Opacity = 1;

            EnableAllCategoryButton(true);

            TextBlock_ItemListHeader.Text = "SELECT A CATEGORY";
            TextBlock_ItemListHeader.Opacity = 1;

            TableAddItem.ResetTable();
            TableAddItem.IsHitTestVisible = true;
            TableAddItem.Opacity = 1;

            TextBlock_ItemName.Text = "SELECT AN ITEM";
            TextBlock_ItemName.Opacity = 1;

            Button_DecreaseQty.IsEnabled = false;
            Button_IncreaseQty.IsEnabled = false;
            TextBlock_ItemQuantity.Text = "0";
            TextBlock_ItemQuantity.Opacity = 1;

            Button_AddItem.IsEnabled = false;
        }

        //
        //
        //      CARLO-END
        //
        //
    }
}
