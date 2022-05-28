using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Command;
using System.IO;
using System.Runtime.Serialization.Json;
using WpfApp1.CreateFlat;
using System.Windows.Resources;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data.Entity;
using System.Transactions;
using System.Windows.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ShowFlats.xaml
    /// </summary>
    public partial class ShowFlats : Window
    {
        public static ShowFlats ShowForm;
        //string connectionString = "Data Source=WIN-LUDVDCOIEES;Initial Catalog=Flat_Rent;Integrated Security=True";

        public ShowFlats()
        {
            InitializeComponent();
            ShowForm = this;
            this.DataContext = new ApplicationsViewModel();

            ResourceDictionary language = new ResourceDictionary();
            string languagePath = Switcher.SwitchForm.languagePath;
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            ResourceDictionary style = new ResourceDictionary();
            string stylePath = Switcher.SwitchForm.stylePath;
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);

            StreamResourceInfo sri = Application.GetResourceStream(new Uri("Main/Normal - Techno Set.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;

            MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat);

        }

        private void DeleteFlats(object sender, RoutedEventArgs e)
        {          
            Flat selectedFlat = (Flat)(ShowFlats.ShowForm.ListView.SelectedItem);
            foreach(var item in MainWindow.MainForm.db.Flat)
            {
                if (item.Id == selectedFlat.Id)
                    MainWindow.MainForm.db.Flat.Remove(item);
            }
            MainWindow.MainForm.db.SaveChanges();
            MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat);
            //string sqlExpression = "DELETE FROM Flat WHERE id="+selectedFlat.Id;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sqlExpression, connection);
            //    int number = command.ExecuteNonQuery();
            //}
            //MainWindow.MainForm.FlatDataOutput("SELECT *, (select count(*) from Deal D where F.id=D.flat_id) FROM Flat F");

        }

        private void Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sorting.SelectedItem == PriceAsc) SortByPriceAsc();
            else
            {
                SortByPriceDesc();
            }
        }
        public void SortByPriceAsc()
        {
            MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat.OrderBy(x => x.Flat_price));

            //    MainWindow.MainForm.FlatDataOutput("SELECT *, (select count(*) from Deal D where F.id=D.flat_id) FROM Flat F ORDER BY flat_price");
        }
        void SortByPriceDesc()
        {
            MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat.OrderByDescending(x => x.Flat_price));

            //    MainWindow.MainForm.FlatDataOutput("SELECT *, (select count(*) from Deal D where F.id=D.flat_id) FROM Flat F ORDER BY flat_price DESC");
        }

        private void EditFlats(object sender, RoutedEventArgs e)
        {
            if (ShowFlats.ShowForm.ListView.SelectedItem != null)
            {
                EditFlat create = new EditFlat();
                create.Show();
            }
            else MessageBox.Show("Выберите элемент, который хотите изменить.");

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Sorting.SelectedItem == PriceAsc || Sorting.SelectedItem == null)
            MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat.Where(x => x.City_name.StartsWith(this.SearchText.Text) || x.Street_name.StartsWith(this.SearchText.Text)).OrderBy(x=>x.Flat_price));
            else
                MainWindow.MainForm.FlatDataOutput(MainWindow.MainForm.db.Flat.Where(x => x.City_name.StartsWith(this.SearchText.Text) || x.Street_name.StartsWith(this.SearchText.Text)).OrderByDescending(x => x.Flat_price));
        }

        private void MakeTransaction(object sender, RoutedEventArgs e)
        {
            //using (var context = new SampleContext())
            //{
            //    using (var transaction= context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            context.Flat.Add(new Flat("Минск", "Неманская", "3a", 123, 60, 70, 2, "sdfsf", true, "ef", "sdff", "dd", "sf", null, null));
            //            context.Flat.Add(new Flat("Минск", "Кунцевщина", "3a", 123, 60, 70, 2, "sdfsf", true, "ef", "sdff", "dd", "sf", null, null));
            //            transaction.Commit();
            //            //context.Database.ExecuteSqlCommand(@"UPDATE Flat SET flat_price = flat_price + 10 WHERE city_name = 'Минск'");
            //            //context.Database.ExecuteSqlCommand(@"UPDATE Flat SET flat_price = flat_price -10 WHERE city_name = 'Гомель'");
            //            MainWindow.MainForm.FlatDataOutput(context.Flat);
            //        }
            //        catch (Exception exp)
            //        {
            //            MessageBox.Show(exp.ToString());
            //            transaction.Rollback();
            //        }
            //    }
            //}

        }


        //private void ResultQuery(object sender, RoutedEventArgs e)
        //{
        //    MainWindow.MainForm.FlatDataOutput("SELECT *, (select count(*) from Deal D where F.id=D.flat_id) FROM Flat F where F.street_name LIKE 'Нем%' and F.flat_price>60");
        //}

        //private void ResultTransaction(object sender, RoutedEventArgs e)
        //{
        //    string sqlExpression = "begin tran delete Deal where flat_id = 36; insert Deal values(5, 39, 1, '2022-09-08', '2023-01-01') insert Deal values(6, 43, 2, '2022-09-08', '2023-01-01') commit tran;";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(sqlExpression, connection);
        //        int number = command.ExecuteNonQuery();
        //    }
        //    MainWindow.MainForm.FlatDataOutput("SELECT *, (select count(*) from Deal D where F.id=D.flat_id) FROM Flat F");

        //}
    }
}
