using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Resources;
using WpfApp1.CreateFlat;
using System.Data.SqlClient;

namespace WpfApp1
{
    public partial class EditFlat : Window
    {
        public EditFlat()
        {
            InitializeComponent();
            Flat Eflat = (Flat)ShowFlats.ShowForm.ListView.SelectedItem;
            City.Text = Eflat.City_name; //error
            Street.Text = Eflat.Street_name;
            House.Text = Eflat.House_number;
            FlatNum.Text = Convert.ToString(Eflat.Flat_number);
            Price.Text = Convert.ToString(Eflat.Flat_price);
            Square.Text = Convert.ToString(Eflat.Flat_area);
            CountOfRoom.Text = Convert.ToString(Eflat.Room_count);

            ResourceDictionary language = new ResourceDictionary();
            string languagePath = Switcher.SwitchForm.languagePath;
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            StreamResourceInfo sri = Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;

        }

        private void Editing(object sender, RoutedEventArgs e)
        {
            Flat selectedFlat = (Flat)(ShowFlats.ShowForm.ListView.SelectedItem);

            //State.oldBooks.Push(new List<Flat>() { selectedFlat });
            //State.newBooks.Push(new List<Flat>() { new Flat(selectedFlat.Id, this.City.Text, this.Street.Text, this.House.Text, Convert.ToInt32(this.FlatNum.Text),
            //Convert.ToInt32(this.Price.Text), Convert.ToInt32(this.Square.Text), Convert.ToInt32(this.CountOfRoom.Text))});

            foreach (var item in MainWindow.MainForm.db.Flat)
            {
                if (item.Id == selectedFlat.Id)
                    MainWindow.MainForm.db.Flat.Remove(item);
            }
            MainWindow.MainForm.db.Flat.Add(new Flat(this.City.Text, this.Street.Text, this.House.Text, Convert.ToInt32(this.FlatNum.Text), Convert.ToDecimal(this.Price.Text), Convert.ToDouble(this.Square.Text), Convert.ToInt32(this.CountOfRoom.Text), "ffsf", "f", "f", "d", "d", null, null));
            MainWindow.MainForm.db.SaveChanges();
            MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat);
            this.Close();

            //string connectionString = "Data Source=WIN-LUDVDCOIEES;Initial Catalog=Flat_Rent;Integrated Security=True";
            //string sqlExpression = "UPDATE Flat SET street_name='"+this.Street.Text+"', house_number=" + this.House.Text + ", flat_number=" + Convert.ToInt32(this.FlatNum.Text) + ", flat_area=" + Convert.ToInt32(this.Square.Text) +
            //", room_count=" + Convert.ToInt32(this.CountOfRoom.Text) + ", flat_price=" + Convert.ToInt32(this.Price.Text) + ", city_name='"+this.City.Text+"' WHERE flat_id=" +selectedFlat.Id;

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sqlExpression, connection);
            //    int number = command.ExecuteNonQuery();
            //}

            //MainWindow.MainForm.FlatDataOutput("SELECT * FROM Flat");
        }
    }
}
