using Microsoft.AspNetCore.Identity;

namespace Quintrix_Web_App_Core_MVC.Models
{
	public class RoleEdit
	{
		public IdentityRole Role { get; set; }
		public IEnumerable<Player> Members { get; set; }
		public IEnumerable<Player> NonMembers { get; set; }
	}
}
