using System.ComponentModel.DataAnnotations;

namespace Quintrix_Web_App_Core_MVC.Models
{
	public class RoleModification
	{
		[Required]
		public string RoleName { get; set; } = "";

		public string RoleId { get; set; } = "";

		public string[] AddIds { get; set; } = new string[0];

		public string[] DeleteIds { get; set; } = new string[0];
	}
}
