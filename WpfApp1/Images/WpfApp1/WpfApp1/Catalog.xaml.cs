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
using System.Data.Entity;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ClientSide.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public static Catalog CatalogForm;
        public static Flat selectedFlat;

        public Catalog()
        {
            InitializeComponent();
            CatalogForm = this;
            using (SampleContext db = new SampleContext())
            {
                string imagePath = "/Images/underground.png";
                foreach (var item in db.Flat.ToList())
                {
                    if (item.City_name.Trim().ToLower() != "минск")
                    {
                        flatsList.Items.Add(new Block(item.Id, item.Image_main, item.Room_count + "-комнатная квартира, " + item.City_name + ", ул." + item.Street_name + ", д." + item.House_number, item.District + " район", null, item.Subway_stat, item.Flat_area, item.Floor + " этаж из " + item.Max_floor, item.Flat_description, item.Publication_date.ToShortDateString(), item.Flat_price));
                    }
                    else
                        flatsList.Items.Add(new Block(item.Id, item.Image_main, item.Room_count + "-комнатная квартира, " + item.City_name + ", ул." + item.Street_name + ", д." + item.House_number, item.District + " район", imagePath, item.Subway_stat, item.Flat_area, item.Floor + " этаж из " + item.Max_floor, item.Flat_description, item.Publication_date.ToShortDateString(), item.Flat_price));
                }
            }
        }
        private void flatsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Block selectedBlock = (Block)(flatsList.SelectedItem);
            using (SampleContext db = new SampleContext())
            {
                selectedFlat = db.Flat.Where(f => f.Id == selectedBlock.Id).FirstOrDefault();

                FlatDetails create = new FlatDetails();
                create.Show();
                Close();
            }
        }
        private void Image_Account_Click(object sender, MouseButtonEventArgs e)
        {
            Account create = new Account();
            create.Show();
        }
    }
}
