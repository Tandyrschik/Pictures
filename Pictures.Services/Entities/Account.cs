
using Pictures.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pictures.Domain.Entities
{
	public class Account
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public Role Role { get; set; }
		public ICollection<Picture> Pictures { get; set; }
	}
}
