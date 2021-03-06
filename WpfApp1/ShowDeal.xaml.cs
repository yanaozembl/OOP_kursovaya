using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

using System.Windows.Resources;

namespace WpfApp1
{
    public partial class ShowDeal : Window
    {
        public static Flat selectedFlat;
        public static ApplicateBlock selectedBlock;
        public static ShowDeal ShowDealForm;
        public string languagePath = "LanguageSelector/ru-Ru.xaml";
        public string stylePath = "StyleSelector/DarkTheme.xaml";
        public ShowDeal()
        {
            InitializeComponent();
            ShowDealForm = this;
            ResourceDictionary language = new ResourceDictionary();
            //string languagePath = Switcher.SwitchForm.languagePath;
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            System.Windows.Input.Cursor customCursor = new System.Windows.Input.Cursor(sri.Stream);
            this.Cursor = customCursor;
            Fill_flats_list(ListAppViewings, 1);
            Fill_flats_list(ListAppBookings, 3);
            Fill_flats_list(ListViews, 2);
            Fill_flats_list(ListBookings, 4);
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControl.SelectedItem == Applicate_view || TabControl.SelectedItem == Applicate_book)
                Confirm_Button.Visibility = Refuse_Button.Visibility=Visibility.Visible;
            else Confirm_Button.Visibility = Refuse_Button.Visibility = Visibility.Collapsed;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListAppViewings.SelectedItem!=null|| ListAppBookings.SelectedItem!=null)
                Confirm_Button.Background = Refuse_Button.Background= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFDD00"));
            else Confirm_Button.Background = Refuse_Button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCACACA"));

        }
        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(TabControl.SelectedItem == Applicate_view)
            {
                selectedBlock = (ApplicateBlock)ListAppViewings.SelectedItem;
                ConfirmView create = new ConfirmView();
                create.Show();
            }
            else if(TabControl.SelectedItem == Applicate_book)
            {
                selectedBlock = (ApplicateBlock)ListAppBookings.SelectedItem;
                using (SampleContext db=new SampleContext())
                {
                    db.Database.ExecuteSqlCommand("update Deal set status=4 where flat_id=@id and deal_start_date=@date", new SqlParameter("@id", selectedBlock.Id), new SqlParameter("@date", selectedBlock.Deal_start_date.Substring(3)));
                    ListAppBookings.Items.Remove(ListAppBookings.SelectedItem);
                    Fill_flats_list(ListBookings, 4);
                }
                var mailFrom = new MailAddress("yanaozembl@gmail.com", "Premium Apartments");
                var mailTo = new MailAddress("ozembl.yana375@yandex.ru", ShowDeal.selectedBlock.Name);
                var message = new MailMessage(mailFrom, mailTo);
                message.Body = $"Здравствуйте, {ShowDeal.selectedBlock.Name}! Заявка на бронирование квартиры г. {selectedBlock.City}, ул. {selectedBlock.Street}, д. {selectedBlock.House}, кв. {selectedBlock.Flat} одобрена. \n" +
                $"Дата заезда в квартиру назначена на {selectedBlock.Deal_start_date}. С Вами свяжется администратор для уточнения подробностей бронирования. \n" +
                "Спасибо, что выбрали нас!";
                message.Subject = "Заявка на бронирование квартиры одобрена!";

                var client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailFrom.Address, "vquqigsgszjlawdc");
                client.Send(message);
            }
        }
        internal static void Fill_flats_list(ListView List, int status)
        {
            List.Items.Clear();
            using (SampleContext db = new SampleContext())
            {
                var selectedFlats = from D in db.Deal
                                    join F in db.Flat on D.Flat_id equals F.Id
                                    join C in db.Client on D.Client_id equals C.Id
                                    where D.Status == status
                                    select new
                                    {
                                        Id = F.Id,
                                        City = F.City_name,
                                        Street = F.Street_name,
                                        House = F.House_number,
                                        Flat = F.Flat_number,
                                        Price = F.Flat_price,
                                        Name = C.Name,
                                        Phone = C.Phone_number,
                                        Email = C.Email,
                                        Deal_start_date = D.Deal_start_date,
                                        Rental_period = D.Rental_period,
                                        View_date = D.View_date
                                    };
                foreach (var item in selectedFlats)
                {
                    if (item.View_date != null && item.Deal_start_date!=null)
                    {   
                        DateTime date_view = (DateTime)item.View_date;
                        DateTime date_start = (DateTime)item.Deal_start_date;
                        List.Items.Add(new ApplicateBlock(item.Id, item.City, item.Street, item.House, item.Flat, item.Price, item.Name, item.Phone, item.Email, date_start.ToShortDateString(), item.Rental_period, date_view.ToShortDateString()));
                    }
                    else if (item.View_date == null && item.Deal_start_date != null)
                    {
                        DateTime date_start = (DateTime)item.Deal_start_date;
                        List.Items.Add(new ApplicateBlock(item.Id, item.City, item.Street, item.House, item.Flat, item.Price, item.Name, item.Phone, item.Email, date_start.ToShortDateString(), item.Rental_period, null));
                    }
                    else if (item.View_date != null && item.Deal_start_date == null)
                    {
                        DateTime date_view = (DateTime)item.View_date;
                        List.Items.Add(new ApplicateBlock(item.Id, item.City, item.Street, item.House, item.Flat, item.Price, item.Name, item.Phone, item.Email, null, item.Rental_period, date_view.ToShortDateString()));
                    }
                    else if (item.View_date == null && item.Deal_start_date == null)
                    {
                        List.Items.Add(new ApplicateBlock(item.Id, item.City, item.Street, item.House, item.Flat, item.Price, item.Name, item.Phone, item.Email, null, item.Rental_period, null));
                    }

                }
            }
        }
        private void Button_Refuse_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedItem == Applicate_view)
            {
                selectedBlock = (ApplicateBlock)ListAppViewings.SelectedItem;
                using (SampleContext db = new SampleContext())
                {
                    var selectedClient = db.Client.Where(c => c.Phone_number == ShowDeal.selectedBlock.Phone).FirstOrDefault();
                    var selectedFlat = db.Flat.Where(f => f.City_name == selectedBlock.City && f.Street_name == selectedBlock.Street && f.House_number == selectedBlock.House && f.Flat_number == selectedBlock.Flat).FirstOrDefault();
                    db.Database.ExecuteSqlCommand("update deal set status=5 where client_id=@client_id and flat_id=@flat_id", new SqlParameter("@client_id", selectedClient.Id), new SqlParameter("@flat_id", selectedFlat.Id));
                    ListAppViewings.Items.Remove(ListAppViewings.SelectedItem);
                }
                var mailFrom = new MailAddress("yanaozembl@gmail.com", "Premium Apartments");
                var mailTo = new MailAddress("ozembl.yana375@yandex.ru", ShowDeal.selectedBlock.Name);
                var message = new MailMessage(mailFrom, mailTo);
                message.Body = $"Здравствуйте, {ShowDeal.selectedBlock.Name}! Заявка на просмотр квартиры г. {selectedBlock.City}, ул. {selectedBlock.Street}, д. {selectedBlock.House}, кв. {selectedBlock.Flat} отклонена. \n" +
                $"С Вами свяжется администратор для уточнения причин отказа.";
                message.Subject = "Заявка на просмотр квартиры отклонена";

                var client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailFrom.Address, "vquqigsgszjlawdc");
                client.Send(message);

            }
            else if (TabControl.SelectedItem == Applicate_book)
            {

                selectedBlock = (ApplicateBlock)ListAppBookings.SelectedItem;
                using (SampleContext db = new SampleContext())
                {
                    db.Database.ExecuteSqlCommand("update Deal set status=5 where flat_id=@id and deal_start_date=@date", new SqlParameter("@id", selectedBlock.Id), new SqlParameter("@date", selectedBlock.Deal_start_date.Substring(3)));
                    ListAppBookings.Items.Remove(ListAppBookings.SelectedItem);
                }

                var mailFrom = new MailAddress("yanaozembl@gmail.com", "Premium Apartments");
                var mailTo = new MailAddress("ozembl.yana375@yandex.ru", ShowDeal.selectedBlock.Name);
                var message = new MailMessage(mailFrom, mailTo);
                message.Body = $"Здравствуйте, {ShowDeal.selectedBlock.Name}! Заявка на бронирование квартиры г. {selectedBlock.City}, ул. {selectedBlock.Street}, д. {selectedBlock.House}, кв. {selectedBlock.Flat} отклонена. \n" +
                $"С Вами свяжется администратор для уточнения причин отказа.";
                message.Subject = "Заявка на бронирование квартиры отклонена";

                var client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailFrom.Address, "vquqigsgszjlawdc");
                client.Send(message);
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            ShowFlats create = new ShowFlats();
            create.Show();
                Close();
        }
    }
}

