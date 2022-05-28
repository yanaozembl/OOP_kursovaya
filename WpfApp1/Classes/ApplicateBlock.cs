using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ApplicateBlock
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int Flat { get; set; }
        public decimal Price { get; set; }
        public string Deal_start_date { get; set; }
        public string View_date { get; set; }
        public int? Rental_period { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

        public ApplicateBlock(int id, string city, string street, string house, int flatnum, decimal price, string name, int phone, string email, string date, int? rent, string date_view)
        {
            Id = id;
            City = city;
            Street = street;
            House = house;
            Flat = flatnum;
            Price = price;
            Name = name;
            Phone = phone;
            Email = email;
            Deal_start_date = date;
            Rental_period = rent;
            View_date = date_view;
        }

    }
}
