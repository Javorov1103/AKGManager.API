using AutoService.API.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.API.Infrastructure.Services
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly AppSettings appSettings;
        public ConnectionFactory(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public SqlConnection GetCompanyConnection()
        {
            throw new NotImplementedException();
        }

        public SqlConnection GetStandardConnection()
        {
            return this.Create(this.appSettings.ConnectionString);
        }

        private SqlConnection Create(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString));
            }

            var conn = new SqlConnection(connectionString);

            conn.Open();

            return conn;
        }
    }
}
