using System.Data.Entity;
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
    public class Flat
    {
        public int Id { get; set; }
        public string City_name { get; set; }
        public string Street_name { get; set; }
        public string House_number { get; set; }
        public int Flat_number { get; set; }
        public decimal Flat_price { get; set; }
        public double Flat_area { get; set; }
        public int Room_count { get; set; }
        public string Flat_description { get; set; }
        public string Image_main { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public decimal Deposit { get; set; }
        public string District { get; set; }
        public string Subway_stat { get; set; }
        public DateTime Publication_date { get ; set; }
        public int Floor { get; set; }
        public int Max_floor { get; set; }

        public Flat(int id, string city, string street, string house, int flatnum, int price, int square, int countofroom)
        {
            Id = id;
            City_name = city;
            Street_name = street;
            House_number = house;
            Flat_number = flatnum;
            Flat_price = price;
            Flat_area = square;
            Room_count = countofroom;
        }
        public Flat(string image_main, string city, string street)
        {
            Image_main = image_main;
            City_name = city;
            Street_name = street;
        }

        public Flat( string city, string street, string house, int flatnum, int price, int square, int countofroom)
        {
            
            City_name = city;
            Street_name = street;
            House_number = house;
            Flat_number = flatnum;
            Flat_price = price;
            Flat_area = square;
            Room_count = countofroom;
        }

        public Flat(string city, string street, string house, int flatnum, decimal price, double square, int countofroom, string descr, string imageMain, string image1, string image2, string image3, string image4, string image5)
        {
            City_name = city;
            Street_name = street;
            House_number = house;
            Flat_number = flatnum;
            Flat_price = price;
            Flat_area = square;
            Room_count = countofroom;
            Flat_description = descr;
            Image_main = imageMain;
            Image1 = image1;
            Image2 = image2;
            Image3 = image3;
            Image4 = image4;
            Image5 = image5;
        }
        public Flat() { }
    }
}
