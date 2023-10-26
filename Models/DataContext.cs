using Microsoft.EntityFrameworkCore;

namespace WEBAPI_ASP_NET_CORE.Models
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Movie> movie { get; set; }
    }
}
