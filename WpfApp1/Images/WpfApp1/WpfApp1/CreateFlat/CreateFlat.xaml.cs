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
using System.Windows.Shapes;
using WpfApp1.Command;
using System.Windows.Resources;
using System.Data.SqlClient;


namespace WpfApp1.CreateFlat
{
    /// <summary>
    /// Логика взаимодействия для CreateFlat.xaml
    /// </summary>
    public partial class CreateFlat : Window
    {
        public static CreateFlat CreateForm;
        public CreateFlat()
        {
            InitializeComponent();
            this.DataContext = new ApplicationsViewModel();
            CreateForm = this;

            ResourceDictionary language = new ResourceDictionary();
            string languagePath = Switcher.SwitchForm.languagePath;
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            StreamResourceInfo sri = Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;

        }

        private void AddFlat(object sender, RoutedEventArgs e)
        {

            var flats = MainWindow.MainForm.db.Flat;
            try
            {
                MainWindow.MainForm.db.Flat.Add(new Flat(this.City.Text, this.Street.Text, this.House.Text, Convert.ToInt32(this.FlatNum.Text), Convert.ToDecimal(this.Price.Text ), Convert.ToDouble(this.Square.Text), Convert.ToInt32(this.CountOfRoom.Text), "ffsf", "f", "f", "d", "d", null, null));
                MainWindow.MainForm.db.SaveChanges();
                MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat);
                this.Close();

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            //string connectionString = "Data Source=WIN-LUDVDCOIEES;Initial Catalog=Flat_Rent;Integrated Security=True";
            //string sqlExpression = $"INSERT INTO Flat ( street_name, house_number, flat_number, flat_area, room_count, flat_price,  flat_description," +
            //    "status, city_name, image_main, image1, image2, image3) VALUES ( '"+this.Street.Text+"','"+this.House.Text+"', "+Convert.ToInt32(this.FlatNum.Text)+
            //    ","+Convert.ToInt32(this.Square.Text)+", "+Convert.ToInt32(this.CountOfRoom.Text)+","+ Convert.ToInt32(this.Price.Text)+", 'fddf', 1, '"+this.City.Text+"','gvd','sf','ddde','SDvvg')";
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
