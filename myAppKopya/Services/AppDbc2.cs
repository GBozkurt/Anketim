using Microsoft.EntityFrameworkCore;
using myApp.Migrations;
using myApp.Models;

namespace myApp.Services
{
    public class AppDbc2 : DbContext
    {
        public AppDbc2(DbContextOptions<AppDbc2> options): base(options) 
        {
        
            
        }

        

        public DbSet<Anket> Ankets { get; set; }
		public DbSet<Veri2> Veri2 { get; set; }

	}
}
