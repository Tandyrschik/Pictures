

namespace Pictures.Domain.ViewModels.Picture
{
	public class PictureViewModel // модель представлений нужна для тех случаев, когда в представление не требуется 
	{                            // выводить всю информацию о сущности. 
		public int Id { get; set; }
		public string Address { get; set; }
		public string Name { get; set; }
		public int AccountId { get; set; }
	}
}
