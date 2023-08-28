using Pictures.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Domain.ViewModels.Picture
{
	public class PictureViewModel // модель представлений нужна для тех случаев, когда в представление не требуется 
	{                            // выводить всю информацию о сущности. 
		public string Address { get; set; }
		public string Name { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
