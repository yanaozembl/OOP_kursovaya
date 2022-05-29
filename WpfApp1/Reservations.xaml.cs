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
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class Reservations : Window
    {
        private int imageIndex = 0;
        private string[] imageList;
        private string imagePath = "/Images/underground.png";
        private string descr;
        public static Flat selectedFlat;
        public static Block selectedBlock;
        public static Deal selectedDeal;
        public List<Flat> reserver_flats;
        private string status;
        public Reservations()
        {
            InitializeComponent();
            if (flatsList != null)
            {
                flatsList.Items.Clear();
                using (SampleContext db = new SampleContext())
                {
                    reserver_flats = db.Database.SqlQuery<Flat>("select * from Flat f join Deal d on d.flat_id=f.id join Client c on d.client_id=c.id where c.id=" + MainWindow.selectedClient.Id).ToList();

                    foreach (var item in reserver_flats)
                    {
                        if (item.Flat_description.Length > 300)
                            descr = item.Flat_description.Substring(0, 300) + ("...");
                        else descr = item.Flat_description;

                        imageList = (string[])Directory.GetFiles(item.Folder_Path, "*.jp*g", SearchOption.AllDirectories);
                        int statusNum = db.Deal.Where(d => d.Flat_id == item.Id && d.Client_id == MainWindow.selectedClient.Id).Select(d => d.Status).FirstOrDefault();
                        switch (statusNum)
                        {
                            case 1:
                                {
                                    status = "Заявка на просмотр обрабатывается";
                                    break;
                                }
                            case 2:
                                {
                                    status = "Просмотрено";
                                    break;
                                }
                            case 3:
                                {
                                    status = "Заявка на бронирование обрабатывается";
                                    break;
                                }
                            case 4:
                                {
                                    status = "Забронировано";
                                    break;
                                }
                            case 5:
                                {
                                    status = "Отклонено";
                                    break;
                                }

                        }

                        if (item.City_name.Trim().ToLower() != "минск")
                        {
                            flatsList.Items.Add(new Block(item.Id, imageList[imageIndex], item.Room_count + "-комнатная квартира, " + item.City_name + ", ул." + item.Street_name + ", д." + item.House_number, item.District + " район", null, item.Subway_stat, item.Flat_area, item.Floor + " этаж из " + item.Max_floor, descr, item.Publication_date.ToShortDateString(), item.Flat_price, status));
                        }
                        else
                            flatsList.Items.Add(new Block(item.Id, imageList[imageIndex], item.Room_count + "-комнатная квартира, " + item.City_name + ", ул." + item.Street_name + ", д." + item.House_number, item.District + " район", imagePath, item.Subway_stat, item.Flat_area, item.Floor + " этаж из " + item.Max_floor, descr, item.Publication_date.ToShortDateString(), item.Flat_price, status));
                    }
                }
            }
        }
        private void flatsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBlock = (Block)flatsList.SelectedItem;
            selectedFlat = reserver_flats.Where(f => f.Id == selectedBlock.Id).FirstOrDefault();
            using (SampleContext db = new SampleContext())
            {
                selectedDeal = db.Deal.Where(d => d.Flat_id == selectedFlat.Id).FirstOrDefault();
            }
            ReservedFlat create = new ReservedFlat();
            create.Show();
        }
    }

}
