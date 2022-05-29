using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            Name.Text = ShowDeal.selectedBlock.Name;
            Phone.Text = "+375" + ShowDeal.selectedBlock.Phone;
        }
        private void Button_Add_Date_click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)Calendar.SelectedDate;
            using(SampleContext db=new SampleContext())
            {
                var selectedClient = db.Client.Where(c => c.Phone_number == ShowDeal.selectedBlock.Phone).FirstOrDefault();
                var selectedFlat = db.Flat.Where(f => f.City_name == ShowDeal.selectedBlock.City && f.Street_name == ShowDeal.selectedBlock.Street && f.House_number == ShowDeal.selectedBlock.House && f.Flat_number == ShowDeal.selectedBlock.Flat).FirstOrDefault();
                db.Database.ExecuteSqlCommand("update deal set status=2, view_date=@date where client_id=@client_id and flat_id=@flat_id", new SqlParameter("@date", date.Date), new SqlParameter("@client_id", selectedClient.Id), new SqlParameter("@flat_id", selectedFlat.Id));
                ShowDeal.ShowDealForm.ListAppViewings.Items.Remove(ShowDeal.selectedBlock);
                ShowDeal.Fill_flats_list(ShowDeal.ShowDealForm.ListViews, 2);

                var mailFrom = new MailAddress("yanaozembl@gmail.com", "Premium Apartments");
                var mailTo = new MailAddress("ozembl.yana375@yandex.ru", ShowDeal.selectedBlock.Name);
                var message = new MailMessage(mailFrom, mailTo);
                message.Body = $"Здравствуйте, {ShowDeal.selectedBlock.Name}! Заявка на просмотр квартиры г. {ShowDeal.selectedBlock.City}, ул. {ShowDeal.selectedBlock.Street}, д. {ShowDeal.selectedBlock.House}, кв. {ShowDeal.selectedBlock.Flat} одобрена. \n" +
                $"Просмотр квартиры состоится {date.ToShortDateString()}. С Вами свяжется администратор для уточнения подробностей визита. \n"+
                "Спасибо, что выбрали нас!";
                message.Subject = "Заявка на просмотр квартиры одобрена!";

                var client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailFrom.Address, "vquqigsgszjlawdc");
                client.Send(message);
                Close();
            }
        }
    }
}
