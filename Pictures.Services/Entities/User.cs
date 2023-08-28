

using Pictures.Domain.Enums;

namespace Pictures.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public Role Role { get; set; }
		public ICollection<Picture> Pictures { get; set; } // связывание, один пользователь может иметь множество картинок.
	}
}
