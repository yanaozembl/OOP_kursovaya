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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public Account()
        {
            InitializeComponent();
            using (SampleContext db = new SampleContext())
            {
                MainWindow.selectedClient = db.Client.Where(b => b.Email == MainWindow.MainForm.Email.Text && b.Password == MainWindow.MainForm.PassBox.Password).FirstOrDefault();
            }
            Name.Text = MainWindow.selectedClient.Surname + " "+MainWindow.selectedClient.Name + " "+MainWindow.selectedClient.Patronymic;
            Phone.Text = Convert.ToString(MainWindow.selectedClient.Phone_number);
            Email.Text = MainWindow.selectedClient.Email;
            PasswordSuccessText.Visibility = Visibility.Hidden;

        }

        private void Button_Name_Click(object sender, RoutedEventArgs e)
        {
            NameText1.Visibility = Visibility.Hidden;
            Name.Visibility = Visibility.Hidden;
            Button_Edit_Name.Visibility = Visibility.Hidden;

            Surname.Visibility = Visibility.Visible;
            NewName.Visibility = Visibility.Visible;
            Patr.Visibility = Visibility.Visible;
            SurnameText.Visibility = Visibility.Visible;
            NameText.Visibility = Visibility.Visible;
            PatrText.Visibility = Visibility.Visible;
            Cansel_Button.Visibility = Visibility.Visible;
            Save_Button.Visibility = Visibility.Visible;

        }
        private void Button_Cansel_Click(object sender, RoutedEventArgs e)
        {

            NameText1.Visibility = Visibility.Visible;
            Name.Visibility = Visibility.Visible;
            Button_Edit_Name.Visibility = Visibility.Visible;

            Surname.Visibility = Visibility.Hidden;
            NewName.Visibility = Visibility.Hidden;
            Patr.Visibility = Visibility.Hidden;
            SurnameText.Visibility = Visibility.Hidden;
            NameText.Visibility = Visibility.Hidden;
            PatrText.Visibility = Visibility.Hidden;
            Cansel_Button.Visibility = Visibility.Hidden;
            Save_Button.Visibility = Visibility.Hidden;
        }
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            using (SampleContext db = new SampleContext())
            {
                if (Surname.Text != null)
                    db.Database.ExecuteSqlCommand("update Client set surname='" + Surname.Text + "' where id=" + MainWindow.selectedClient.Id);
                if (NewName.Text != String.Empty)
                    db.Database.ExecuteSqlCommand("update Client set name='" + NewName.Text + "' where id=" + MainWindow.selectedClient.Id);
                if (Patr.Text != String.Empty)
                    db.Database.ExecuteSqlCommand("update Client set patronymic='" + Patr.Text + "' where id=" + MainWindow.selectedClient.Id);
                db.SaveChanges();
                MainWindow.selectedClient = db.Client.Where(b => b.Email == MainWindow.MainForm.Email.Text && b.Password == MainWindow.MainForm.PassBox.Password).FirstOrDefault();
            }
                Button_Cansel_Click(sender, e);
                Name.Text = MainWindow.selectedClient.Surname + " " + MainWindow.selectedClient.Name + " " + MainWindow.selectedClient.Patronymic;
            
        }
        private void Button_Phone_Click(object sender, RoutedEventArgs e)
        {
            PhoneText.Visibility = Visibility.Hidden;
            Phone.Visibility = Visibility.Hidden;
            Button_Edit_Phone.Visibility = Visibility.Hidden;

            NewPhone.Visibility = Visibility.Visible;
            Button_Cansel_Phone.Visibility = Visibility.Visible;
            Button_Save_Phone.Visibility = Visibility.Visible;
        }
        private void Button_Cansel_Phone_Click(object sender, RoutedEventArgs e)
        {
            PhoneText.Visibility = Visibility.Visible;
            Phone.Visibility = Visibility.Visible;
            Button_Edit_Phone.Visibility = Visibility.Visible;

            NewPhone.Visibility = Visibility.Hidden;
            Button_Cansel_Phone.Visibility = Visibility.Hidden;
            Button_Save_Phone.Visibility = Visibility.Hidden;
        }
        private void Button_Save_Phone_Click(object sender, RoutedEventArgs e)
        {
            using (SampleContext db = new SampleContext())
            {
                if (NewPhone.Text != String.Empty)
                    db.Database.ExecuteSqlCommand("update Client set phone_number=" + NewPhone.Text + " where id=" + MainWindow.selectedClient.Id);
                db.SaveChanges();

                MainWindow.selectedClient = db.Client.Where(b => b.Email == MainWindow.MainForm.Email.Text && b.Password == MainWindow.MainForm.PassBox.Password).FirstOrDefault();
            }
            Button_Cansel_Phone_Click(sender, e);

            Phone.Text = Convert.ToString(MainWindow.selectedClient.Phone_number);
        }
        private void Button_Email_Click(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Hidden;
            Email.Visibility = Visibility.Hidden;
            Button_Edit_Email.Visibility = Visibility.Hidden;

            NewEmail.Visibility = Visibility.Visible;
            Button_Cansel_Email.Visibility = Visibility.Visible;
            Button_Save_Email.Visibility = Visibility.Visible;
        }
        private void Button_Cansel_Email_Click(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Visible;
            Email.Visibility = Visibility.Visible;
            Button_Edit_Email.Visibility = Visibility.Visible;

            NewEmail.Visibility = Visibility.Hidden;
            Button_Cansel_Email.Visibility = Visibility.Hidden;
            Button_Save_Email.Visibility = Visibility.Hidden;
        }
        private void Button_Save_Email_Click(object sender, RoutedEventArgs e)
        {
            using (SampleContext db = new SampleContext())
            {
                if (NewEmail.Text != String.Empty)
                    db.Database.ExecuteSqlCommand("update Client set email='" + NewEmail.Text + "' where id=" + MainWindow.selectedClient.Id);
                db.SaveChanges();
                MainWindow.MainForm.Email.Text = NewEmail.Text;
                MainWindow.selectedClient = db.Client.Where(b => b.Email == MainWindow.MainForm.Email.Text && b.Password == MainWindow.MainForm.PassBox.Password).FirstOrDefault();
            }
            Button_Cansel_Email_Click(sender, e);

            Email.Text = MainWindow.selectedClient.Email;
        }
        private void Button_Password_Click(object sender, RoutedEventArgs e)
        {
            PasswordText.Visibility = Visibility.Hidden;
            Button_Edit_Password.Visibility = Visibility.Hidden;

            NewPasswordText.Visibility = Visibility.Visible;
            NewPassword.Visibility = Visibility.Visible;
            OldPasswordText.Visibility = Visibility.Visible;
            OldPassword.Visibility = Visibility.Visible;
            RepeatNewPasswordText.Visibility = Visibility.Visible;
            RepeatNewPassword.Visibility = Visibility.Visible;
            Button_Cansel_Password.Visibility = Visibility.Visible;
            Button_Save_Password.Visibility = Visibility.Visible;
        }
        private void Button_Cansel_Password_Click(object sender, RoutedEventArgs e)
        {
            PasswordText.Visibility = Visibility.Visible;
            Button_Edit_Password.Visibility = Visibility.Visible;

            NewPasswordText.Visibility = Visibility.Hidden;
            NewPassword.Visibility = Visibility.Hidden;
            OldPasswordText.Visibility = Visibility.Hidden;
            OldPassword.Visibility = Visibility.Hidden;
            RepeatNewPasswordText.Visibility = Visibility.Hidden;
            RepeatNewPassword.Visibility = Visibility.Hidden;
            Button_Cansel_Password.Visibility = Visibility.Hidden;
            Button_Save_Password.Visibility = Visibility.Hidden;
        }
        private void Button_Save_Password_Click(object sender, RoutedEventArgs e)
        {
            using (SampleContext db = new SampleContext())
            {
                if (OldPassword.Password==MainWindow.selectedClient.Password && NewPassword.Password != String.Empty && RepeatNewPassword.Password != String.Empty && NewPassword.Password == RepeatNewPassword.Password)
                {
                    db.Database.ExecuteSqlCommand("update Client set password='" + NewPassword.Password + "' where id=" + MainWindow.selectedClient.Id);
                    db.SaveChanges();
                    MainWindow.MainForm.PassBox.Password = NewPassword.Password;
                    MainWindow.selectedClient = db.Client.Where(b => b.Email == MainWindow.MainForm.Email.Text && b.Password == MainWindow.MainForm.PassBox.Password).FirstOrDefault();
                    OldPassword.Password = string.Empty;
                    NewPassword.Password = RepeatNewPassword.Password = string.Empty;
                    Button_Cansel_Password_Click(sender, e);
                    PasswordSuccessText.Visibility = Visibility.Visible;
                }
                else if (OldPassword.Password != MainWindow.selectedClient.Password)
                {
                    MessageBox.Show("Старый пароль введен неверно.");
                    OldPassword.Password = string.Empty;
                }
                else if (NewPassword.Password == RepeatNewPassword.Password)
                {
                    MessageBox.Show("Пароли не совпадают. Повторите попытку.");
                    NewPassword.Password = RepeatNewPassword.Password = string.Empty;
                }
                else if (NewPassword.Password == string.Empty)
                {
                    MessageBox.Show("Введите новый пароль.");
                }
                else if (RepeatNewPassword.Password == string.Empty)
                {
                    MessageBox.Show("Повторите новый пароль.");
                }
            }

        }


    }
}
