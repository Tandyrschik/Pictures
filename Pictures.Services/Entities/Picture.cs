﻿

namespace Pictures.Domain.Entities
{
	public class Picture // описание сущности в коде и в таблице БД, где каждое свойство равно колонке в таблице
	{
		public int Id { get; set; }
		public string Address { get; set; }
		public string Name { get; set; }
		public int UserId { get; set; }
		public User User { get; set; } // связывание, одна картинка принадлежит одному пользователю.
	}
}
