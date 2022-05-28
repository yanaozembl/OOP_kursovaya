using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Deal
    {
        public int Id { get; set; }
        public int Flat_id { get; set; }
        public int Client_id { get; set; }
        public DateTime? Deal_start_date { get; set; }
        public int Rental_period { get; set; }
        public decimal Total_cost { get; set; }

        public Deal( int flatid, int clientid, DateTime? date, int rentalPeriod, decimal cost)
        {
            Flat_id = flatid;
            Client_id = clientid;
            Deal_start_date = date;
            Rental_period = rentalPeriod;
            Total_cost = cost;
        }
    }
}
