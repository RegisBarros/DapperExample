using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DapperExample.Data
{
    public class DapperExampleContext : IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperExampleContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString);
                }

                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}
