using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Resources;
using System.Text.RegularExpressions;
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
                Email.Background = Brushes.Red;
                MessageBox.Show("Email введен некорректно");
            }
            else if (db.Client.Where(b => b.Password == PassBox.Password).FirstOrDefault() == null)
            {
                PassBox.Background = Brushes.Red;
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
            string email = EmailReg.Text.ToLower().Trim();
            string pass = PassBoxReg.Password;
            string pass_2 = PassBoxReg_2.Password;

            EmailReg.ToolTip = "";
            EmailReg.Background = Brushes.Transparent;

            PassBoxReg.ToolTip = "";
            PassBoxReg.Background = Brushes.Transparent;

            PassBoxReg_2.ToolTip = "";
            PassBoxReg_2.Background = Brushes.Transparent;
            try
            {
                if (Surname.Text == "" || Name.Text == "" || Patronymic.Text == "" || PhoneNum.Text == "" || EmailReg.Text == "" || PassBoxReg.Password == "" || PassBoxReg_2.Password == "")
                    throw new Exception("Введите все поля.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (email.Length < 5 || email.Contains('@') == false || email.Contains('.') == false)
            {
                EmailReg.ToolTip = "Email введен некорректно";
                EmailReg.Background = Brushes.Red;
            }
            else if (pass.Length <= 5)
            {
                PassBoxReg.ToolTip = "Пароль должен быть более 5 символов";
                PassBoxReg.Background = Brushes.Red;
            }
            else if (pass != pass_2)
            {
                PassBoxReg_2.ToolTip = "Пароли не совпадают";
                PassBoxReg_2.Background = Brushes.Red;
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(PhoneNum.Text) <= 0)
                        throw new Exception("Поле не должно быть отрицательным или равным нулю");
                    Regex regexNum = new Regex(@"(2|3|9|4){2}[0-9]{7}");
                    MatchCollection matchesNum = regexNum.Matches(PhoneNum.Text);
                    Regex regexText = new Regex(@"[а-я]");
                    MatchCollection matchesSurname = regexText.Matches(Surname.Text);
                    MatchCollection matchesName = regexText.Matches(Name.Text);
                    MatchCollection matchesPatronymic = regexText.Matches(Patronymic.Text);

                    if (matchesNum.Count > 0)
                    {
                        if (Woman.IsChecked == true)
                            selectedClient = new Client(Surname.Text, Name.Text, Patronymic.Text, "ж", Convert.ToInt32(PhoneNum.Text), EmailReg.Text, Convert.ToString(PassBoxReg.Password));
                        else selectedClient = new Client(Surname.Text, Name.Text, Patronymic.Text, "м", Convert.ToInt32(PhoneNum.Text), EmailReg.Text, Convert.ToString(PassBoxReg.Password));
                        db.Client.Add(selectedClient);
                        db.SaveChanges();
                        Catalog create = new Catalog();
                        create.Show();
                        Close();
                    }
                    else
                        throw new Exception("Номер может начинаться на 29, 44, 33, а также содержать только 9 цифр.");
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Введите все поля.");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Некорректный формат данных. Проверьте значение полей.");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
    }
}

