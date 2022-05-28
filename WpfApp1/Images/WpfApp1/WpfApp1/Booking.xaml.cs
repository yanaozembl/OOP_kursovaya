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
    /// <summary>
    /// Логика взаимодействия для Booking.xaml
    /// </summary>
    public partial class Booking : Window
    {

        public Booking()
        {
            InitializeComponent();
            Calendar.DisplayDateStart = DateTime.Today;
            Calendar.SelectedDate = DateTime.Today;

        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            using(SampleContext db=new SampleContext())
            {
                try
                {
                    db.Deal.Add(new Deal(Catalog.selectedFlat.Id, MainWindow.selectedClient.Id, Calendar.SelectedDate, Convert.ToInt32(Rental_period.Text), Catalog.selectedFlat.Flat_price + Catalog.selectedFlat.Deposit));
                    db.SaveChanges();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            Close();
        }
    }
}
