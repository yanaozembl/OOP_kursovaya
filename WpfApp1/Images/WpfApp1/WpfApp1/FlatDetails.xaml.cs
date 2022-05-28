using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для FlatDetails.xaml
    /// </summary>
    public partial class FlatDetails : Window
    {
        private int imageIndex;
        private string[] imageList;

        public FlatDetails()
        {
        InitializeComponent();


            //imageList = new string[] { Catalog.selectedFlat.Image_main, Catalog.selectedFlat.Image1, Catalog.selectedFlat.Image2 };
            //if (Catalog.selectedFlat.Image3 != null)
            //{
            //    imageList.Add(Catalog.selectedFlat.Image3);
            //}
            //if (Catalog.selectedFlat.Image4 != null)
            //{
            //    imageList.Add(Catalog.selectedFlat.Image4);
            //}
            //if (Catalog.selectedFlat.Image5 != null)
            //{
            //    imageList.Add(Catalog.selectedFlat.Image5);
            //}
            imageList = (string[])Directory.GetFiles(@"C:\Users\User\Desktop\бгту\ООП\WpfApp1\WpfApp1\Images\" + Catalog.selectedFlat.Id, "*.jp*g", SearchOption.AllDirectories);
            imageIndex = 0;

            Photo.Source = new BitmapImage(new Uri(imageList[imageIndex]));
            Uri uri = new Uri("https://yandex.ru/maps/?um=constructor%3A5e0ccc3cad8ef82197aa65f8e90fba60dda64ed837526a78646da582e05b8b0b&amp;source=constructorStatic");
            webView.Source = uri;
        }
        private void Button_Redo_click(object sender, EventArgs e)
        {
            imageIndex--;
            if (imageIndex < 0)
                imageIndex = imageList.Length - 1;

            Photo.Source = new BitmapImage(new Uri(imageList[imageIndex]));
        }

        private void Button_Undo_click(object sender, EventArgs e)
        {
            imageIndex++;
            if (imageIndex > imageList.Length - 1)
                imageIndex = 0;

            Photo.Source = new BitmapImage(new Uri(imageList[imageIndex]));
        }

        private void Button_Book_Click(object sender, RoutedEventArgs e)
        {
            Booking create = new Booking();
            create.Show();
        }
    }
}
