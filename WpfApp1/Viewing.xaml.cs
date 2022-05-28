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
    /// Логика взаимодействия для Viewing.xaml
    /// </summary>
    public partial class Viewing : Window
    {
        public Viewing()
        {
            InitializeComponent();
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            using(SampleContext db=new SampleContext())
            {
                db.Deal.Add(new Deal(Catalog.selectedFlat.Id, MainWindow.selectedClient.Id, 1, null));
                db.SaveChanges();
                Close();
            }
        }
    }
}
