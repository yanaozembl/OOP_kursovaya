using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Resources;
using WpfApp1.CreateFlat;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class EditFlat : Window
    {
        private bool wifi, parking, conditioner, city_v, tree_v;
        public EditFlat()
        {
            InitializeComponent();
            Flat editFlat = (Flat)ShowFlats.selectedFlat;
            City.Text = editFlat.City_name; 
            Street.Text = editFlat.Street_name;
            House.Text = editFlat.House_number;
            FlatNum.Text = Convert.ToString(editFlat.Flat_number);
            Price.Text = Convert.ToString(editFlat.Flat_price);
            Square.Text = Convert.ToString(editFlat.Flat_area);
            CountOfRoom.Text = Convert.ToString(editFlat.Room_count);
            Floor.Text = Convert.ToString(editFlat.Floor);
            MaxFloor.Text = Convert.ToString(editFlat.Max_floor);
            District.Text = editFlat.District;
            Subway_stat.Text = editFlat.Subway_stat;
            Description.Text = editFlat.Flat_description;
            Deposit.Text = Convert.ToString(editFlat.Deposit);
            Pathe.Text = editFlat.Folder_Path;
            Wifi.IsChecked = editFlat.Wifi;
            Parking.IsChecked = editFlat.Parking;
            Conditioner.IsChecked = editFlat.Conditioner;
            City_v.IsChecked = editFlat.City_view;
            Tree_v.IsChecked = editFlat.Tree_view;

            ResourceDictionary language = new ResourceDictionary();
            string languagePath = Switcher.SwitchForm.languagePath;
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            System.Windows.Input.Cursor customCursor = new System.Windows.Input.Cursor(sri.Stream);
            this.Cursor = customCursor;
        }
        private void Editing(object sender, RoutedEventArgs e)
        {
            using(SampleContext db=new SampleContext())
            {
                db.Database.ExecuteSqlCommand("update flat set city_name=@city, street_name=@street, house_number=@house, flat_number=@flat_num, flat_price=@price, flat_area=@square, room_count=@room, flat_description=@descr, deposit=@deposit, district=@district, subway_stat=@subway, floor=@floor, max_floor=@max_floor, status=@status, wifi=@wifi, parking=@parking, conditioner=@conditioner, city_view=@city_v, tree_view=@tree_v where id=@id",
                    new SqlParameter("@city", City.Text), new SqlParameter("@street", Street.Text), new SqlParameter("@house", House.Text), new SqlParameter("@flat_num", Convert.ToInt32(FlatNum.Text)), new SqlParameter("@price", Convert.ToDecimal(Price.Text)), new SqlParameter("@deposit", Convert.ToDecimal(Deposit.Text)), new SqlParameter("@square", Convert.ToDouble(Square.Text)), new SqlParameter("@status", ShowFlats.selectedFlat.Status), new SqlParameter("@floor", Convert.ToInt32(Floor.Text)), new SqlParameter("@max_floor", Convert.ToInt32(MaxFloor.Text)), new SqlParameter("@folder", Pathe.Text),
                    new SqlParameter("@wifi", wifi), new SqlParameter("@parking", parking), new SqlParameter("@conditioner", conditioner), new SqlParameter("@city_v", city_v), new SqlParameter("@tree_v", tree_v), new SqlParameter("@district", District.Text), new SqlParameter("@room", Convert.ToInt32(CountOfRoom.Text)), new SqlParameter("@descr", Description.Text), new SqlParameter("@subway", Subway_stat.Text), new SqlParameter("@id", ShowFlats.selectedFlat.Id));
                ShowFlats.DataOutput(db.Flat.ToList());
                Close();
            }
        }
        private void Button_Directory_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            Pathe.Text = FBD.SelectedPath;

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
    }
}
