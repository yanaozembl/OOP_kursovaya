using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Block
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string First_string { get; set; }
        public string District { get; set; }
        public string Image_path { get; set; }
        public string Subway_stat { get; set; }
        public double Square { get; set; }
        public string Floor { get; set; }
        public string Description { get; set; }
        public string Publication_date { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
        public Block(int id, string image, string firststring, string district, string imagepath, string subway, double square, string floor, string description, string date, decimal price)
        {
            Id = id;
            Image = image;
            First_string = firststring;
            District = district;
            Image_path = imagepath;
            Subway_stat = subway;
            Square = square;
            Floor = floor;
            Description = description;
            Publication_date = date;
            Price = price;
        }
        public Block(int id, string image, string firststring, string district, string imagepath, string subway, double square, string floor, string description, string date, decimal price, string status)
        {
            Id = id;
            Image = image;
            First_string = firststring;
            District = district;
            Image_path = imagepath;
            Subway_stat = subway;
            Square = square;
            Floor = floor;
            Description = description;
            Publication_date = date;
            Price = price;
            Status = status;
        }
    }
}