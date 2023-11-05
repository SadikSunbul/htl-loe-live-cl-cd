using Microsoft.EntityFrameworkCore;

namespace test_project;

public class Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }
    
    public class CustoemrContext:DbContext
    {
        public CustoemrContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}