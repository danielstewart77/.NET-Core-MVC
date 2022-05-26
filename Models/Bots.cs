using System.ComponentModel.DataAnnotations;

namespace Quintrix_Web_App_Core_MVC.Models
{
	public class Bots
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public int BotId { get; set; }	
	}
}
