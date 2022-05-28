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
using System.Windows.Forms;

namespace WpfApp1.CreateFlat
{
    /// <summary>
    /// Логика взаимодействия для CreateFlat.xaml
    /// </summary>
    public partial class CreateFlat : Window
    {
        private bool wifi = false, parking = false, conditioner = false, city_v = false, tree_v = false;
        public CreateFlat()
        {
            InitializeComponent();
            this.DataContext = new ApplicationsViewModel();

            ResourceDictionary language = new ResourceDictionary();
            string languagePath = Switcher.SwitchForm.languagePath;
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            System.Windows.Input.Cursor customCursor = new System.Windows.Input.Cursor(sri.Stream);
            this.Cursor = customCursor;

        }

        private void AddFlat(object sender, RoutedEventArgs e)
        {
            try
            {
                if(City.Text==String.Empty || Street.Text==String.Empty || FlatNum.Text == String.Empty || House.Text == String.Empty || Price.Text == String.Empty || Deposit.Text == String.Empty || Floor.Text == String.Empty || MaxFloor.Text == String.Empty || District.Text == String.Empty || Subway_stat.Text == String.Empty || Pathe.Text == String.Empty || CountOfRoom.Text == String.Empty || Description.Text == String.Empty)
                {
                    throw new Exception("Введите все поля");
                }
                else
                {
                    using (SampleContext db = new SampleContext())
                    {
                        db.Flat.Add(new Flat(City.Text, Street.Text, House.Text, Convert.ToInt32(FlatNum.Text), Convert.ToDecimal(Price.Text), Convert.ToDouble(Square.Text), Convert.ToInt32(CountOfRoom.Text), Description.Text, Convert.ToDecimal(Deposit.Text), District.Text, Subway_stat.Text, DateTime.Today, Convert.ToInt32(Floor.Text), Convert.ToInt32(MaxFloor.Text), 0, Pathe.Text, wifi, parking, conditioner, city_v, tree_v));
                        db.SaveChanges();
                        ShowFlats.DataOutput(db.Flat.ToList());
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (Wifi.IsChecked == true)
                wifi = true;
            if (Parking.IsChecked == true)
                parking = true;
            if (Conditioner.IsChecked == true)
                conditioner = true;
            if (City_v.IsChecked == true)
                city_v = true;
            if (Tree_v.IsChecked == true)
                tree_v = true;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Wifi.IsChecked == false)
                wifi = false;
            if (Parking.IsChecked == false)
                parking = false;
            if (Conditioner.IsChecked == false)
                conditioner = false;
            if (City_v.IsChecked == false)
                city_v = false;
            if (Tree_v.IsChecked == false)
                tree_v = false;
        }

        private void Button_Directory_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            Pathe.Text=FBD.SelectedPath;
        }

    }
}