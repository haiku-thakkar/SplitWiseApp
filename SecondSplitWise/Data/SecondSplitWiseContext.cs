using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecondSplitWise.Model;

namespace SecondSplitWise.DBContext
{
    public class SecondSplitWiseContext : DbContext
    {
        public SecondSplitWiseContext (DbContextOptions<SecondSplitWiseContext> options)
            : base(options)
        {
        }

        public DbSet<user> user { get; set; }

        public DbSet<expense> expense { get; set; }

        public DbSet<expense_member> expense_member { get; set; }

        public DbSet<friend> friend { get; set; }

        public DbSet<group> group { get; set; }

        public DbSet<group_payer> group_payer{ get; set; }

        public DbSet<group_transactions> group_transaction { get; set; }

        public DbSet<members> member { get; set; }

        public DbSet<payment> payment { get; set; }

        public DbSet<single_payer> single_payer{ get; set; }

        public DbSet<user_expense> user_expense { get; set; }
    }
}
