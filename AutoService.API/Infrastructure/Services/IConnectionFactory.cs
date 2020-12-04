namespace AutoService.API.Infrastructure.Services
{
    using System.Data.SqlClient;
    public interface IConnectionFactory
    {
        /// <summary>
        /// Creates and opens a connection to the private 
        /// database of the currently logged in user's company.
        /// </summary>
        SqlConnection GetCompanyConnection();

        /// <summary>
        /// Creates and opens a connection to the standard/shared database.
        /// </summary>
        SqlConnection GetStandardConnection();
    }
}
