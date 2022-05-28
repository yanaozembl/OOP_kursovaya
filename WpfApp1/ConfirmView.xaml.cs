using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для ConfirmView.xaml
    /// </summary>
    public partial class ConfirmView : Window
    {
        public ConfirmView()
        {
            InitializeComponent();
            Calendar.DisplayDateStart = DateTime.Today;
            Calendar.SelectedDate = DateTime.Today;
        }
        private void Button_Add_Date_click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)Calendar.SelectedDate;
            using(SampleContext db=new SampleContext())
            {
                var selectedClient = db.Client.Where(c => c.Phone_number == ShowDeal.selectedBlock.Phone).FirstOrDefault();
                var selectedFlat = db.Flat.Where(f => f.City_name == ShowDeal.selectedBlock.City && f.Street_name == ShowDeal.selectedBlock.Street && f.House_number == ShowDeal.selectedBlock.House && f.Flat_number == ShowDeal.selectedBlock.Flat).FirstOrDefault();
                db.Database.ExecuteSqlCommand("update deal set status=2, view_date=@date where client_id=@client_id and flat_id=@flat_id and status=1", new SqlParameter("@date", date.Date), new SqlParameter("@client_id", selectedClient.Id), new SqlParameter("@flat_id", selectedFlat.Id));
                ShowDeal.ShowDealForm.ListAppViewings.Items.Remove(ShowDeal.selectedBlock);
                ShowDeal.Fill_flats_list(ShowDeal.ShowDealForm.ListViews, 2);
                Close();
            }
        }
    }
}
