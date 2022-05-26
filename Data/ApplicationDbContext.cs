using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quintrix_Web_App_Core_MVC.Models;

namespace Quintrix_Web_App_Core_MVC.Data
{
	public class ApplicationDbContext : IdentityDbContext<Player>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Bots>? Bots { get; set; }

	}
}