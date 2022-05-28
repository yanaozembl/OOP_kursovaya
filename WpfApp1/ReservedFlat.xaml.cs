using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp1
{
    public partial class ReservedFlat : Window
    {
        private int imageIndex;
        private string[] imageList;
        public ReservedFlat()
        {
            InitializeComponent();
            Photo.Source = new BitmapImage(new Uri(Reservations.selectedBlock.Image));
            Address.Text = Reservations.selectedBlock.First_string;
            DateTime date = (DateTime)Reservations.selectedDeal.Deal_start_date;
            ReserveDate.Text = date.ToShortDateString();
            Rent_period.Text = Convert.ToString(Reservations.selectedDeal.Rental_period);
            Description.Text = Reservations.selectedFlat.Flat_description;
            Total_cost.Text = Convert.ToString(Reservations.selectedDeal.Total_cost);
            using (SampleContext db = new SampleContext())
            {
                imageList = (string[])Directory.GetFiles(Reservations.selectedFlat.Folder_Path, "*.jp*g", SearchOption.AllDirectories);
            }
            foreach(string img in imageList)
            {
                if (img.ToLower().Contains("map"))
                {
                    Map.Source = new BitmapImage(new Uri(img));
                }
            }

        }
    }
}
