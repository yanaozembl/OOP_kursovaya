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
        public decimal Deposit { get; set; }
        public string District { get; set; }
        public string Subway_stat { get; set; }
        public DateTime Publication_date { get; set; }
        public int Floor { get; set; }
        public int Max_floor { get; set; }
        public int Status { get; set; }
        public string Folder_Path { get; set; }
        public bool Wifi { get; set; }
        public bool Parking { get; set; }
        public bool Conditioner { get; set; }
        public bool City_view { get; set; }
        public bool Tree_view { get; set; }

        public Flat(string city, string street, string house, int flatnum, decimal price, double square, int countofroom, string descr, decimal deposit, string district, string subway, DateTime date, int floor, int max_floor, int status, string folder, bool wifi, bool parking, bool cond, bool city_v, bool tree_v )
        {
            City_name = city;
            Street_name = street;
            House_number = house;
            Flat_number = flatnum;
            Flat_price = price;
            Flat_area = square;
            Room_count = countofroom;
            Flat_description = descr;
            Deposit = deposit;
            District = district;
            Subway_stat = subway;
            Publication_date = date;
            Floor = floor;
            Max_floor = max_floor;
            Status = status;
            Folder_Path = folder;
            Wifi = wifi;
            Parking = parking;
            Conditioner = cond;
            City_view = city_v;
            Tree_view = tree_v;
        }

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
        public Flat( string city, string street)
        {
            City_name = city;
            Street_name = street;
        }

        public Flat(string city, string street, string house, int flatnum, int price, int square, int countofroom)
        {

            City_name = city;
            Street_name = street;
            House_number = house;
            Flat_number = flatnum;
            Flat_price = price;
            Flat_area = square;
            Room_count = countofroom;
        }

        public Flat(string city, string street, string house, int flatnum, decimal price, double square, int countofroom, string descr, string folder)
        {
            City_name = city;
            Street_name = street;
            House_number = house;
            Flat_number = flatnum;
            Flat_price = price;
            Flat_area = square;
            Room_count = countofroom;
            Flat_description = descr;
            Folder_Path = folder;
        }
        public Flat() { }
    }
}
