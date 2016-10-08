using Dapper;
using DapperExample.Data.Models;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;

namespace DapperExample.Data
{
    public class CustomerRepository
    {
        private DapperExampleContext _context;


        public Customer GetByName(string name)
        {
            using (_context = new DapperExampleContext())
            {
                var conn = _context.Connection;

                var command = $"select CustomerId, Name, Born from Customer where Name = @name";

                return conn.QueryFirst<Customer>(command, new { name = name });
            }
        }

        public IEnumerable<Customer> GetAllByName(string name)
        {
            using (_context = new DapperExampleContext())
            {
                var conn = _context.Connection;

                var command = $"uspGetAllByName";

                return conn.Query<Customer>(command, new { name = name }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Add(Customer customer)
        {
            using (_context = new DapperExampleContext())
            {
                var conn = _context.Connection;

                conn.Insert(customer);
            }
        }

        public void Update(Customer customer)
        {
            using (_context = new DapperExampleContext())
            {
                var conn = _context.Connection;

                conn.Update(customer);
            }
        }
    }
}
