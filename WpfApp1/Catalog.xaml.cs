using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ClientSide.xaml
    /// </summary>
    /// 
    public partial class Catalog : Window
    {
        public static Catalog CatalogForm;
        public static Flat selectedFlat;
        public static Block selectedBlock;
        private int imageIndex = 0;
        private string[] imageList;
        private string descr;
        private string imagePath = "/Images/underground.png";
        public Catalog()
        {
            InitializeComponent();
            CatalogForm = this;

            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.Where(f => f.Status == 0).ToList());
            }
        }
        public void DataOutput(List<Flat> flats)
        {
            if (FlatsList != null)
            {
                FlatsList.Items.Clear();
                using (SampleContext db = new SampleContext())
                {
                    foreach (var item in flats)
                    {
                        if (item.Flat_description.Length > 300)
                            descr = item.Flat_description.Substring(0, 300) + ("...");
                        else descr = item.Flat_description;

                        imageList = (string[])Directory.GetFiles(item.Folder_Path, "*.jp*g", SearchOption.AllDirectories);

                        if (item.City_name.Trim().ToLower() != "минск")
                        {
                            FlatsList.Items.Add(new Block(item.Id, imageList[imageIndex], item.Room_count + "-комнатная квартира, " + item.City_name + ", ул." + item.Street_name + ", д." + item.House_number, item.District + " район", null, item.Subway_stat, item.Flat_area, item.Floor + " этаж из " + item.Max_floor, descr, item.Publication_date.ToShortDateString(), item.Flat_price));
                        }
                        else
                            FlatsList.Items.Add(new Block(item.Id, imageList[imageIndex], item.Room_count + "-комнатная квартира, " + item.City_name + ", ул." + item.Street_name + ", д." + item.House_number, item.District + " район", imagePath, item.Subway_stat, item.Flat_area, item.Floor + " этаж из " + item.Max_floor, descr, item.Publication_date.ToShortDateString(), item.Flat_price));
                    }
                }
            }
        }
        private void FlatsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBlock = (Block)(FlatsList.SelectedItem);
            using (SampleContext db = new SampleContext())
            {
                selectedFlat = db.Flat.Where(f => f.Id == selectedBlock.Id).FirstOrDefault();
                FlatDetails create = new FlatDetails();
                create.Show();
                Close();
            }
        }
        private void Image_Account_Click(object sender, RoutedEventArgs e)
        {
            Account create = new Account();
            create.Show();
        }

        private void Button_Reservation_Click(object sender, RoutedEventArgs e)
        {
            Reservations create = new Reservations();
            create.Show();
        }

        private void Button_Sign_out_Click(object sender, RoutedEventArgs e)
        {
            MainWindow create = new MainWindow();
            create.Show();
            Close();
        }
        private void Frunsenskiy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Фрунзенский", "Минск");
        }
        private void Lenin_Grodno(object sender, RoutedEventArgs e)
        {
            Choose_District("Ленинский", "Гродно");
        }
        private void Okt_Grodno(object sender, RoutedEventArgs e)
        {
            Choose_District("Октябрьский", "Гродно");
        }
        private void Hotim(object sender, RoutedEventArgs e)
        {
            Choose_District("Хотимский", "Могилев");
        }
        private void Byhov(object sender, RoutedEventArgs e)
        {
            Choose_District("Быховский", "Могилев");
        }
        private void Central_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Центральный", "Минск");
        }
        private void Partizanskiy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Партизанский", "Минск");
        }
        private void Pervomayskiy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Первомайский", "Минск");
        }
        private void Sovetskiy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Советский", "Минск");
        }
        private void Leninskiy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Ленинский", "Минск");
        }
        private void Moskovskiy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Московский", "Минск");
        }
        private void Oktyabrskiy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Октябрьский", "Минск");
        }
        private void Lenin_Brest(object sender, RoutedEventArgs e)
        {
            Choose_District("Ленинский", "Брест");
        }
        private void Lenin_Moscow(object sender, RoutedEventArgs e)
        {
            Choose_District("Ленинский", "Брест");
        }
        private void Zhelezo(object sender, RoutedEventArgs e)
        {
            Choose_District("Железнодорожный", "Витебск");
        }
        private void Oktyabr(object sender, RoutedEventArgs e)
        {
            Choose_District("Октябрьский", "Витебск");
        }
        private void Pervom(object sender, RoutedEventArgs e)
        {
            Choose_District("Первомайский", "Витебск");
        }
        private void Zhelezo_Gomel(object sender, RoutedEventArgs e)
        {
            Choose_District("Железнодорожный", "Гомель");
        }
        private void Sovet_Gomel(object sender, RoutedEventArgs e)
        {
            Choose_District("Советский", "Гомель");
        }
        private void Central_Gomel(object sender, RoutedEventArgs e)
        {
            Choose_District("Центральный", "Гомель");
        }
        private void Novo_Gomel(object sender, RoutedEventArgs e)
        {
            Choose_District("Новобелицкий", "Гомель");
        }
        private void Zavodskoy_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_District("Заводской", "Минск");
        }
        private void Kamennaya_Gorka_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Каменная горка");
        }
        private void Kunc_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Кунцевщина");
        }
        private void Sport_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Спортивная");
        }
        private void Pushkin_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Пушкинская");
        }
        private void Molodezhnaya_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Молодежная");
        }
        private void Frunz_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Фрунзенская");
        }
        private void Nemiga_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Немига");
        }
        private void Kupalovskaya_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Купалоская");
        }
        private void Pervom_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Первомайская");
        }
        private void Prolet_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Пролетарская");
        }
        private void Traktor_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Тракторный завод");
        }
        private void Partizan_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Партизанская");
        }
        private void Avtozavod_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Автозаводская");
        }
        private void Mogilevskaya_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Могилевская");
        }
        private void Malina_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Малиновка");
        }
        private void Petrov_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Петровщина");
        }
        private void Mihal_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Михалово");
        }
        private void Grush_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Грушевка");
        }
        private void Institut_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Институт культуры");
        }
        private void Lenina_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Площадь Ленина");
        }
        private void Oktyabr_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Октябрьская");
        }
        private void Pobeda_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Площадь Победы");
        }
        private void Kolasa_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Площадь Якуба Коласа");
        }
        private void Akademia_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Академия наук");
        }
        private void Chelusk_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Парк Челюскинцев");
        }
        private void Moskow_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Московская");
        }
        private void Vostok_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Восток");
        }
        private void Boris_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Борисовский тракт");
        }
        private void Urushye_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Уручье");
        }
        private void Ubiley_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Юбилейная площадь");
        }
        private void Bogushevich_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Плоащдь Франтишка Богушевича");
        }
        private void Voksal_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Вокзальная");
        }
        private void Sloboda_Click(object sender, RoutedEventArgs e)
        {
            Choose_Subway("Ковальская Слобода");
        }
        public void Choose_City(string city)
        {
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.Where(f => f.Status == 0 && f.City_name == city).ToList());
            }
        }
        public void Choose_District(string district, string city)
        {
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.Where(f => f.Status == 0 && f.District == district && f.City_name == city).ToList());
            }
        }
        public void Choose_Subway(string subway)
        {
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.Where(f => f.Status == 0 && f.Subway_stat == subway).ToList());
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (Sorting.SelectedItem == PriceAsc || Sorting.SelectedItem == null)
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.Where(x => x.Status == 0 && (x.City_name.StartsWith(SearchBox.Text.Trim()) || x.Street_name.StartsWith(SearchBox.Text.Trim()))).OrderBy(x => x.Flat_price).ToList());
                if (SearchBox.Text == String.Empty)
                    DataOutput(db.Flat.Where(x => x.Status == 0).OrderBy(x => x.Flat_price).ToList());
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Room1!=null && Room2!=null&& Room3!=null&&Room4!=null&&ManyRoom!=null&& Wifi != null && Parking != null && Tree_v != null && City_v != null && Conditioner != null)
            {
                Room1.IsChecked = false;
                Room2.IsChecked = false;
                Room3.IsChecked = false;
                Room4.IsChecked = false;
                ManyRoom.IsChecked = false;
                Wifi.IsChecked = false;
                Parking.IsChecked = false;
                Tree_v.IsChecked = false;
                City_v.IsChecked = false;
                Conditioner.IsChecked = false;
            }

            using (SampleContext db = new SampleContext())
            {
                List<Flat> flats=db.Flat.Where(x => x.Status == 0).ToList();
              if (Sorting.SelectedItem == PriceAsc)
                    DataOutput(flats.OrderBy(x => x.Flat_price).ToList());
                else if (Sorting.SelectedItem == PriceDesc)
                    DataOutput(flats.OrderByDescending(x => x.Flat_price).ToList());
                else if (Sorting.SelectedItem == SquareAsc)
                    DataOutput(flats.OrderBy(x => x.Flat_area).ToList());
                else if (Sorting.SelectedItem == SquareDesc)
                    DataOutput(flats.OrderByDescending(x => x.Flat_price).ToList());            }
        }
        private void CheckBox_Comfort_Checked(object sender, RoutedEventArgs e)
        {
            Room1.IsChecked = false;
            Room2.IsChecked = false;
            Room3.IsChecked = false;
            Room4.IsChecked = false;
            ManyRoom.IsChecked = false;
            Sorting.SelectedIndex = 0;
            using (SampleContext db = new SampleContext())
            {
                List<Flat> selectedFlats = db.Flat.Where(x => x.Status == 0).ToList(); ;
                if (Wifi.IsChecked == true)
                    selectedFlats = db.Flat.Where(x => x.Wifi == true).ToList();
                if (Parking.IsChecked == true)
                    selectedFlats = selectedFlats.Where(x => x.Parking == true).ToList();
                if (Conditioner.IsChecked == true)
                    selectedFlats = selectedFlats.Where(x => x.Conditioner == true).ToList();
                if (City_v.IsChecked == true)
                    selectedFlats = selectedFlats.Where(x => x.City_view == true).ToList();
                if (Tree_v.IsChecked == true)
                    selectedFlats = selectedFlats.Where(x => x.Tree_view == true).ToList();
                DataOutput(selectedFlats);
            }
        }
        private void CheckBox_Count_Checked(object sender, RoutedEventArgs e)
        {
            Wifi.IsChecked = false;
            Parking.IsChecked = false;
            Tree_v.IsChecked = false;
            City_v.IsChecked = false;
            Conditioner.IsChecked = false;
            Sorting.SelectedIndex = 0;
            List<Flat> selectedFlats = new List<Flat>();
            using (SampleContext db = new SampleContext())
            {
                if (Room1.IsChecked == false && Room2.IsChecked == false && Room3.IsChecked == false && Room4.IsChecked == false && ManyRoom.IsChecked == false)
                    selectedFlats = db.Flat.Where(x => x.Status == 0).ToList();
                if (Room1.IsChecked == true)
                    selectedFlats.AddRange(db.Flat.Where(x => x.Status == 0 && x.Room_count == 1).ToList());
                if (Room2.IsChecked == true)
                    selectedFlats.AddRange(db.Flat.Where(x => x.Status == 0 && x.Room_count == 2).ToList());
                if (Room3.IsChecked == true)
                    selectedFlats.AddRange(db.Flat.Where(x => x.Status == 0 && x.Room_count == 3).ToList());
                if (Room4.IsChecked == true)
                    selectedFlats.AddRange(db.Flat.Where(x => x.Status == 0 && x.Room_count == 4).ToList());
                if (ManyRoom.IsChecked == true)
                    selectedFlats.AddRange(db.Flat.Where(x => x.Status == 0 && x.Room_count > 4).ToList());
                DataOutput(selectedFlats);
            }
        }

        private void Button_Minsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_City("Минск");
        }

        private void Button_Mogilev_Click(object sender, RoutedEventArgs e)
        {
            Choose_City("Могилев");
        }
        private void Mogilev(object sender, RoutedEventArgs e)
        {
            Choose_City("Могилев");
        }

        private void Button_Grodno_Click(object sender, RoutedEventArgs e)
        {
            Choose_City("Гродно");
        }
        private void Grodno(object sender, RoutedEventArgs e)
        {
            Choose_City("Гродно");
        }

        private void Button_Brest_Click(object sender, RoutedEventArgs e)
        {
            Choose_City("Брест");

        }

        private void Button_Gomel_Click(object sender, RoutedEventArgs e)
        {
            Choose_City("Гомель");

        }

        private void Button_Vitebsk_Click(object sender, RoutedEventArgs e)
        {
            Choose_City("Витебск");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.ToList());

            }

        }

    }
}
