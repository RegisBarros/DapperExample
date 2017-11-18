using System;

namespace DapperExample.Data.Models
{
    public class Customer
    {
        protected Customer()
        {

        }
        public Customer(string name, DateTime born)
        {
            Name = name;
            Born = born;
        }

        public int CustomerId { get; private set; }

        public string Name { get; private set; }

        public DateTime Born { get; private set; }

        public Address Address { get; set; }
    }
}
