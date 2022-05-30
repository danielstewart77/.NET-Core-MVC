using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quintrix_Web_App_Core_MVC.Models
{
	/// <summary>
	/// Base player model used for players & bots
	/// </summary>
	public class Player : IdentityUser
	{
		/// <summary>
		/// players can have empty names, bots should not
		/// </summary>
		public virtual string Name { get; set; } = "";

		/// <summary>
		/// regular players start at power 1
		/// </summary>
		public virtual int Power { get; set; } = 1;

		public virtual int Level { get; set; } = 1;

		public string personality { get; set; } = "";

		//public string Email { get; set; } = "";

		public bool IsAdmin { get; set; } = false;
	}
}
