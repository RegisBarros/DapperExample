using Dapper;
using DapperExample.Data.Models;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

                var command = $"select Id, Nome, Nascimento from Cliente where Nome = @name";

                return conn.QueryFirst<Customer>(command, new { name = name });
            }
        }

        public Customer GetById(int customerId)
        {
            using (_context = new DapperExampleContext())
            {
                var conn = _context.Connection;

                var command = $"select c.Id as CustomerId, c.Nome as Name, c.Nascimento as Born, e.Logradouro as Line " +
                    $"from Cliente as c " +
                    $"inner join Endereco as e on e.ClienteId = c.Id " + 
                    $"where c.Id = @customerId";

                //var data = conn.QueryFirst<Customer, Address>(command, (customer, address) => { customer.Address = address; return customer; }, new { customerId = customerId });

                var multi = conn.QueryMultiple(command, new { customerId = customerId });
                var customer = multi.Read<Customer>();
                var address = multi.Read<Address>();

                var result = customer.First();

                result.Address = address.First();

                return result;
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
