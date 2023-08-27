using System;
using Microsoft.EntityFrameworkCore;

namespace Finance_Tracking.Models
{
	public class ApplicationDBContext:DbContext
	{
		public ApplicationDBContext(DbContextOptions options):base(options)
		{}
		public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Category> Categorys { get; set; }
       
    }
}

