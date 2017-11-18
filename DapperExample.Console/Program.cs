using DapperExample.Data;
using DapperExample.Data.Models;
using System;

namespace DapperExample.Console
{
    class Program
    {
        private static CustomerRepository _repository = new CustomerRepository();

        static void Main(string[] args)
        {
            //CreateNewCustomer();

            GetCustomer("Jack");

            //GetAll("Jack");

            System.Console.ReadKey();
        }

        public static void CreateNewCustomer()
        {
            var customer = new Customer("Jack", new DateTime(2015, 1, 20));

            _repository.Add(customer);
        }

        public static void GetCustomer(string name)
        {
            var customer = _repository.GetByName(name);

            var customer2 = _repository.GetById(1);

            DisplayCustomer(customer2);
        }

        public static void GetAll(string name)
        {
            var customers = _repository.GetAllByName(name);

            foreach (var customer in customers)
            {
                DisplayCustomer(customer); 
            }
        }

        public static void DisplayCustomer(Customer customer)
        {
            System.Console.WriteLine($"Id: {customer.CustomerId}");
            System.Console.WriteLine($"Name: {customer.Name}");
            System.Console.WriteLine($"Born: {customer.Born}");
            System.Console.WriteLine(string.Empty);
        }
    }
}
