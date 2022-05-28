using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Resources;
using WpfApp1.CreateFlat;
using WpfApp1.Command;
using System.Data.SqlClient;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainForm;
        public List<Flat> flats = new List<Flat>();
        public string languagePath = "LanguageSelector/ru-Ru.xaml";
        public string stylePath = "StyleSelector/DarkTheme.xaml";
        string connectionString = "Data Source=WIN-LUDVDCOIEES;Initial Catalog=Flat_Rent;Integrated Security=True";
        public SampleContext db = new SampleContext();
        public static Client selectedClient;

        public MainWindow()
        {
            InitializeComponent();
            MainForm = this;
            this.DataContext = new ApplicationsViewModel();

            ResourceDictionary language = new ResourceDictionary();
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            ResourceDictionary style = new ResourceDictionary();
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);

            StreamResourceInfo sri = Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;
            
           
        }

        public void SwitchLanguageRussian() // Меняем язык на русский
        {
            languagePath = "LanguageSelector/ru-RU.xaml";
            ResourceDictionary language = new ResourceDictionary();
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);
        }

        public void SwitchLanguageEng() // Меняем язык на англ
        {
            languagePath = "LanguageSelector/en-US.xaml";
            ResourceDictionary language = new ResourceDictionary();
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);
        }

        public void SetBlackStyle()
        {
            stylePath = "StyleSelector/DarkTheme.xaml";
            ResourceDictionary style = new ResourceDictionary();
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);
        }
        public void SetWhiteStyle()
        {
            stylePath = "StyleSelector/WhiteTheme.xaml";
            ResourceDictionary style = new ResourceDictionary();
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);
        }
        public void Creating()
        {
            CreateFlat.CreateFlat create = new CreateFlat.CreateFlat();
            create.Show();
            
        }

        internal void FlatDataOutput(IOrderedQueryable<Flat> dbSet)
        {
            ShowFlats.ShowForm.ListView.Items.Clear();
            foreach (var flat in dbSet)
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
                    Room_count = flat.Room_count
                });
            }
        }

        public void FlatDataOutput(System.Data.Entity.DbSet<Flat> dbSet)
        {
            ShowFlats.ShowForm.ListView.Items.Clear();
            foreach (var flat in dbSet)
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
                    Room_count = flat.Room_count
                })  ;
            }
            //string connectionString = "Data Source=WIN-LUDVDCOIEES;Initial Catalog=Flat_Rent;Integrated Security=True";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand commandOne = new SqlCommand(sqlExpressionOne, connection);
            //    SqlDataReader readerOne = commandOne.ExecuteReader();
            //    if (readerOne.HasRows) // если есть данные
            //    {
            //        while (readerOne.Read()) // построчно считываем данные
            //        {
            //            object flat_id = readerOne.GetValue(0);
            //            object city_name = readerOne.GetValue(1);
            //            object street_name = readerOne.GetValue(2);
            //            object house_number = readerOne.GetValue(3);
            //            object flat_number = readerOne.GetValue(4);
            //            object flat_area = readerOne.GetValue(5);
            //            object room_count = readerOne.GetValue(6);
            //            object flat_price = readerOne.GetValue(7);
            //            object flat_description = readerOne.GetValue(8);
            //            object status = readerOne.GetValue(9);

            //            ShowFlats.ShowForm.ListView.Items.Add(new Flat()
            //            {
            //                Id = Convert.ToInt32(flat_id),
            //                City_name = Convert.ToString(city_name),
            //                Street_name = Convert.ToString(street_name),
            //                House_number = Convert.ToString(house_number),
            //                Flat_number = Convert.ToInt32(flat_number),
            //                Flat_price = Convert.ToInt32(flat_price),
            //                Flat_area = Convert.ToInt32(flat_area),
            //                Room_count = Convert.ToInt32(room_count),
            //            }); 
            //        }
            //        readerOne.Close();                   
            //    }
            //}
        }

        public void RedoB() //вперед
        {
            if (State.oldBooks.Count >= 1)
            {
                Flat selectedFlat = new Flat();
                foreach (var item in State.oldBooks.Pop())
                {
                    selectedFlat = (Flat)(item);
                }
                
                string sqlExpression = "DELETE FROM Flat WHERE street_name='" + selectedFlat.Street_name+"' and house_number='"+selectedFlat.House_number+"' and flat_number="+selectedFlat.Flat_number;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }

                foreach (var item in State.newBooks.Peek())
                {
                    selectedFlat = (Flat)(item);
                    sqlExpression = "INSERT INTO Flat ( street_name, house_number, flat_number, flat_area, room_count, flat_price,  flat_description," +
                    "status, city_name, image_main, image1, image2, image3) VALUES ('" + item.Street_name + "','" + item.House_number + "', " + item.Flat_number +
                    "," + item.Flat_area + ", " + item.Room_count + "," + item.Flat_price + ", 'fddf', 1, '" + item.City_name + "','gvd','sf','ddde','SDvvg')";

                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
                //FlatDataOutput("SELECT * FROM Flat");
            }
        }

        public void UndoB() //назад
        {
            if (State.newBooks.Count >= 1 && State.oldBooks.Count>=1)
            {
                Flat selectedFlat = new Flat();
                foreach (var item in State.newBooks.Peek())
                {
                    selectedFlat = (Flat)(item);
                }
                string sqlExpression = "DELETE FROM Flat WHERE flat_id=" + selectedFlat.Id;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                }

                foreach (var item in State.oldBooks.Peek())
                {
                    selectedFlat = (Flat)(item);
                    sqlExpression = $"INSERT INTO Flat ( street_name, house_number, flat_number, flat_area, room_count, flat_price,  flat_description," +
                    "status, city_name, image_main, image1, image2, image3) VALUES ( '" + item.Street_name + "','" + item.House_number + "', " + item.Flat_number +
                    "," +item.Flat_area + ", " + item.Room_count + "," + item.Flat_price + ", 'fddf', 1, '" + item.City_name + "','gvd','sf','ddde','SDvvg')";

                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                }
                //FlatDataOutput("SELECT * FROM Flat");

            }
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
         
            Email.Background = Brushes.Transparent;
            PassBox.Background = Brushes.Transparent;
           
            selectedClient = db.Client.Where(b => b.Email == Email.Text && b.Password == PassBox.Password).FirstOrDefault();
            if (Email.Text.Trim().ToLower() == "admin" && PassBox.Password == "adminadmin")
            {
                ShowFlats create = new ShowFlats();
                create.Show();
                Close();
            }
            else if (selectedClient != null)
            {
                Catalog create = new Catalog();
                create.Show();
                Close();
            }
            else if (db.Client.Where(b => b.Email == Email.Text).FirstOrDefault() == null)
            {
                Email.Background = Brushes.DarkRed;
                MessageBox.Show("Email введен некорректно");
            }
            else if (db.Client.Where(b => b.Password == PassBox.Password).FirstOrDefault() == null)
            {
                PassBox.Background = Brushes.DarkRed;
                MessageBox.Show("Пароль введен некорректно");
            }
        }

        private void TabControl1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TabControl1.SelectedItem == Sign_up)
            {
                MainForm.Height = 620;
            }
            else if (TabControl1.SelectedItem == Sign_in)
            {
                MainForm.Height = 310;
            }

        }
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string email = Email.Text.ToLower().Trim();
            string pass = PassBox.Password;
            string pass_2 = PassBoxReg_2.Password;

            Email.ToolTip = "";
            Email.Background = Brushes.Transparent;

            PassBox.ToolTip = "";
            PassBox.Background = Brushes.Transparent;

            PassBoxReg_2.ToolTip = "";
            PassBoxReg_2.Background = Brushes.Transparent;

            if (email.Length < 5 || email.Contains('@') == false || email.Contains('.') == false)
            {
                Email.ToolTip = "Email введен некорректно";
                Email.Background = Brushes.DarkRed;
            }
            else if (pass.Length <= 5)
            {
                PassBox.ToolTip = "Пароль должен быть более 5 символов";
                PassBox.Background = Brushes.DarkRed;
            }
            else if (pass != pass_2)
            {
                PassBoxReg_2.ToolTip = "Пароли не совпадают";
                PassBoxReg_2.Background = Brushes.DarkRed;
            }
            else
            {
                try
                {
                    if (Woman.IsChecked == true)
                        selectedClient = new Client(Surname.Text, Name.Text, Patronymic.Text, "ж", Convert.ToInt32(PhoneNum.Text), Email.Text, Convert.ToString(PassBox.Password));
                    else selectedClient = new Client(Surname.Text, Name.Text, Patronymic.Text, "м", Convert.ToInt32(PhoneNum.Text), Email.Text, Convert.ToString(PassBox.Password));

                    db.Client.Add(selectedClient);
                    db.SaveChanges();

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Введите все поля.");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Некорректный формат данных. Проверьте значение полей.");
                }

            }
        }
    }
}

