using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Command;
using System.IO;
using System.Runtime.Serialization.Json;
using WpfApp1.CreateFlat;
using System.Windows.Resources;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data.Entity;
using System.Transactions;
using System.Windows.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ShowFlats.xaml
    /// </summary>
    public partial class ShowFlats : Window
    {
        public static ShowFlats ShowForm;
        public static Flat selectedFlat;
        public ShowFlats()
        {
            InitializeComponent();
            ShowForm = this;
            this.DataContext = new ApplicationsViewModel();

            ResourceDictionary language = new ResourceDictionary();
            string languagePath = Switcher.SwitchForm.languagePath;
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            ResourceDictionary style = new ResourceDictionary();
            string stylePath = Switcher.SwitchForm.stylePath;
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);

            StreamResourceInfo sri = Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.ToList());
            }
        }
        public static void DataOutput(List<Flat> flats)
        {
            if (ShowFlats.ShowForm.ListView != null)
            {
                ShowFlats.ShowForm.ListView.Items.Clear();
                foreach (var flat in flats)
                {
                    ShowFlats.ShowForm.ListView.Items.Add(new Flat()
                    {
                        Id = flat.Id,
                        City_name = flat.City_name,
                        Street_name = flat.Street_name,
                        House_number = flat.House_number,
                        Flat_number = flat.Flat_number,
                        Flat_price = Convert.ToDecimal(flat.Flat_price),
                        Flat_area = flat.Flat_area,
                        Room_count = flat.Room_count,
                        Flat_description = flat.Flat_description,
                        Deposit = flat.Deposit,
                        District = flat.District,
                        Subway_stat = flat.Subway_stat,
                        Publication_date = flat.Publication_date,
                        Floor = flat.Floor,
                        Max_floor = flat.Max_floor,
                        Status = flat.Status,
                        Folder_Path = flat.Folder_Path,
                        Wifi = flat.Wifi,
                        Parking = flat.Parking,
                        Conditioner = flat.Conditioner,
                        City_view = flat.City_view,
                        Tree_view = flat.Tree_view
                    });
                }
                
            }
        }
        private void DeleteFlats(object sender, RoutedEventArgs e)
        {
            using(SampleContext db=new SampleContext())
            {
                db.Database.ExecuteSqlCommand("delete flat where id=" + selectedFlat.Id);
                DataOutput(db.Flat.ToList());
            }
        }

        private void Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sorting.SelectedItem == PriceAsc) SortByPriceAsc();
            else
            {
                SortByPriceDesc();
            }
        }
        public void SortByPriceAsc()
        {
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.OrderBy(x => x.Flat_price).ToList());
            }

        }
        void SortByPriceDesc()
        {
            using (SampleContext db = new SampleContext())
            {
                DataOutput(db.Flat.OrderByDescending(x => x.Flat_price).ToList());
            }
        }

        private void EditFlats(object sender, RoutedEventArgs e)
        {
            if (ShowFlats.ShowForm.ListView.SelectedItem != null)
            {
                EditFlat create = new EditFlat();
                create.Show();
            }
            else MessageBox.Show("Выберите элемент, который хотите изменить.");

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using(SampleContext db = new SampleContext())
            {
                if (Sorting.SelectedItem == PriceAsc || Sorting.SelectedItem == null)
                    DataOutput(db.Flat.Where(x => x.City_name.StartsWith(this.SearchText.Text) || x.Street_name.StartsWith(this.SearchText.Text)).OrderBy(x => x.Flat_price).ToList());
            else
                    DataOutput(db.Flat.Where(x => x.City_name.StartsWith(this.SearchText.Text) || x.Street_name.StartsWith(this.SearchText.Text)).OrderByDescending(x => x.Flat_price).ToList());
            }
        }

        private void Button_Applicate_Click(object sender, RoutedEventArgs e)
        {
            ShowDeal create = new ShowDeal();
            create.Show();
            Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFlat = (Flat)ListView.SelectedItem;
        }
    }
}
