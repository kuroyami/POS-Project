using System;
using System.Collections.Generic;
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
    /// Interaction logic for Reservation_Form.xaml
    /// </summary>
    public partial class Reservation_Form : UserControl
    {
        static int NEW_RESERVATION_NUMBER = -99;

        public delegate void CancelButtonClick();
        public CancelButtonClick cancelButtonClick;

        public delegate void UpdateAccdbRelatedUI();
        public UpdateAccdbRelatedUI updateAccdbRelatedUI;

        int resNum;

        public Reservation_Form()
        {
            InitializeComponent();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {

            cancelButtonClick();
        }

        internal void InitializeForm(int resNum)
        {
            this.resNum = resNum;

            Form_ReadState();

            UpdateForm();
            

        }

        private void Form_EditState()
        {
            TextBox[] textBoxes = GetTextBoxes();
            TextBlock[] textBlocks = GetTextBlocks();

            for (int i = 0; i < 7; i++)
            {
                textBoxes[i].Text = textBlocks[i].Text;
                textBoxes[i].Visibility = Visibility.Visible;
            }

            TextBox_TableNum.Text = TextBlock_TableNum.Text;

            TextBox_TableNum.Visibility = Visibility.Visible;

            Button_Edit.IsEnabled = false;
            Button_Save.IsEnabled = true;
        }

        private void Form_ReadState()
        {
            TextBox[] textBoxes = GetTextBoxes();

            foreach (TextBox tb in textBoxes)
            {
                tb.Visibility = Visibility.Hidden;
            }

            TextBox_TableNum.Visibility = Visibility.Hidden;

            Button_Edit.IsEnabled = true;
            Button_Save.IsEnabled = false;
        }

        public void CreateNewReservation(string tableNum, string date, string time)
        {
            CreateNewReservation();

            TextBox_TableNum.Text = tableNum;
            TextBox_Date.Text = date;
            TextBox_Time.Text = time;
        }

        private void CreateNewReservation()
        {
            Form_EditState();

            TextBox[] textBoxes = GetTextBoxes();

            for (int i = 0; i < 7; i++)
            {
                textBoxes[i].Text = "";
            }

            TextBlock_ResNum.Text = "";
            TextBlock_TableNum.Text = "";

            this.resNum = NEW_RESERVATION_NUMBER;
        }



        private void UpdateForm()
        {
            OleDbConnection cn = new OleDbConnection();    //Connection
            OleDbCommand cmd = new OleDbCommand();         //Command
            OleDbDataReader dr;

            String path = Assembly.GetExecutingAssembly().Location;
            path = path.Replace("bin\\Debug\\ICTProjectPOS.exe", "RestaurantDB.accdb");
            String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", path);

            cn.ConnectionString = @connectionString;
            cmd.Connection = cn;

            try
            {
                cmd.CommandText = "SELECT * FROM Reservation WHERE ResNum = @resNum";

                cmd.Parameters.AddWithValue("@resNum", this.resNum.ToString());

                cn.Open();
                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBlock_ResNum.Text = dr[0].ToString();

                        TextBlock_TableNum.Text = dr[6].ToString();
                        TextBlock_Date.Text = dr[7].ToString().Split(' ')[0];

                        string time = dr[8].ToString().Split(' ')[1] + dr[8].ToString().Split(' ')[2];
                        int hour = Int32.Parse(time.Substring(0, 2));
                        if (time.Contains("PM")) hour += 12;

                        TextBlock_Time.Text = hour.ToString() + ":00";

                        TextBlock_FirstName.Text = dr[1].ToString().ToUpper();
                        TextBlock_LastName.Text = dr[2].ToString().ToUpper();

                        TextBlock_Email.Text = dr[3].ToString();
                        TextBlock_Phone.Text = dr[4].ToString();

                        TextBlock_Comment.Text = dr[5].ToString();
                    }
                }

                cn.Close();

            }
            catch
            {
                // empty
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveToAccdb();
            Form_ReadState();
            UpdateForm();
            updateAccdbRelatedUI();
        }

        private void SaveToAccdb()
        {

            String path = Assembly.GetExecutingAssembly().Location;
            path = path.Replace("bin\\Debug\\ICTProjectPOS.exe", "RestaurantDB.accdb");

            String connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", path);
            OleDbConnection cn = new OleDbConnection(connectionString);




            string[] date = TextBox_Date.Text.Split('/');
            string[] time = TextBox_Time.Text.Split(':');

            DateTime dt = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]),Int32.Parse(time[0]), 0, 0);

            cn.Open();

            if(this.resNum == NEW_RESERVATION_NUMBER)    //ADD NEW RECORD
            {

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM Reservation", cn);

                int resNum_key = (int)cmd.ExecuteScalar() + 1;


                cmd = new OleDbCommand("INSERT INTO Reservation([ResNum], [FirstName], [LastName], [Email], [PhoneNum], [Comment], [TableNum], [ResDate], [ResTime]) VALUES (@ResNum, @FirstName, @LastName, @Email, @PhoneNum, @Comment, @TableNum, @ResDate, @ResTime)", cn);

                cmd.Parameters.AddWithValue("@ResNum", resNum_key);
                cmd.Parameters.AddWithValue("@FirstName", TextBox_FirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", TextBox_LastName.Text);
                cmd.Parameters.AddWithValue("@Email", TextBox_Email.Text);
                cmd.Parameters.AddWithValue("@PhoneNum", TextBox_Phone.Text);
                cmd.Parameters.AddWithValue("@Comment", TextBox_Comment.Text);
                cmd.Parameters.AddWithValue("@TableNum", Int32.Parse(TextBox_TableNum.Text));
                cmd.Parameters.AddWithValue("@ResDate", dt.Date);
                cmd.Parameters.AddWithValue("@ResTime", dt.TimeOfDay);

                cmd.ExecuteNonQuery();

                this.resNum = resNum_key;

            }
            else         // UPDATE EXISTING RECORD
            {
                int resNum_key = this.resNum;

                OleDbCommand cmd = new OleDbCommand("DELETE FROM Reservation WHERE ResNum = @ResNum", cn);
                cmd.Parameters.AddWithValue("@ResNum", this.resNum);

                cmd.ExecuteNonQuery();

                cmd = new OleDbCommand("INSERT INTO Reservation([ResNum], [FirstName], [LastName], [Email], [PhoneNum], [Comment], [TableNum], [ResDate], [ResTime]) VALUES (@ResNum, @FirstName, @LastName, @Email, @PhoneNum, @Comment, @TableNum, @ResDate, @ResTime)", cn);

                cmd.Parameters.AddWithValue("@ResNum", this.resNum);
                cmd.Parameters.AddWithValue("@FirstName", TextBox_FirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", TextBox_LastName.Text);
                cmd.Parameters.AddWithValue("@Email", TextBox_Email.Text);
                cmd.Parameters.AddWithValue("@PhoneNum", TextBox_Phone.Text);
                cmd.Parameters.AddWithValue("@Comment", TextBox_Comment.Text);
                cmd.Parameters.AddWithValue("@TableNum", Int32.Parse(TextBox_TableNum.Text));
                cmd.Parameters.AddWithValue("@ResDate", dt.Date);
                cmd.Parameters.AddWithValue("@ResTime", dt.TimeOfDay);

                cmd.ExecuteNonQuery();
            }

            cn.Close();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            Form_EditState();
        }

        private TextBox[] GetTextBoxes()
        {
            TextBox[] textBoxes = { TextBox_Date, TextBox_Time, TextBox_FirstName, TextBox_LastName, TextBox_Email, TextBox_Phone, TextBox_Comment };

            return textBoxes;
        }

        private TextBlock[] GetTextBlocks()
        {
            TextBlock[] textBlocks = { TextBlock_Date, TextBlock_Time, TextBlock_FirstName, TextBlock_LastName, TextBlock_Email, TextBlock_Phone, TextBlock_Comment };

            return textBlocks;
        }
    }
}
