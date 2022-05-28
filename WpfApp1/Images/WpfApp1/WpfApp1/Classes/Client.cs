using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Client
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public int Phone_number { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Client() { }
        public Client(string surname, string name, string patronymic, string sex, int phone, string email, string password)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Sex = sex;
            Phone_number = phone;
            Email = email;
            Password = password;
        }
    }
}
