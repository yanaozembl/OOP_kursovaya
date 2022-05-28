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

namespace WpfApp1
{
    public partial class Booking : Window
    {
        public Booking()
        {
            InitializeComponent();
            Name.Text = Catalog.selectedBlock.First_string;
            Calendar.DisplayDateStart = DateTime.Today;
            Calendar.SelectedDate = DateTime.Today;
            Deposit.Text = Catalog.selectedFlat.Deposit + "$";
            Price.Text = Catalog.selectedFlat.Flat_price + "$";
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            using (SampleContext db=new SampleContext())
            {
                db.Deal.Add(new Deal(Catalog.selectedFlat.Id, MainWindow.selectedClient.Id, Calendar.SelectedDate, Convert.ToInt32(Rental_period.Text), Catalog.selectedFlat.Flat_price + Catalog.selectedFlat.Deposit, 3));
                db.SaveChanges();
                Close();
            }
        }
    }
}
