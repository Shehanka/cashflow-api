using moneyboss_transaction_service.Models;
using Microsoft.EntityFrameworkCore;

namespace moneyboss_transaction_service.DB
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options) : base(options) { }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expenses> Expense { get; set; }
    }
}