using System.ComponentModel.DataAnnotations;

namespace TodoAPP.ViewModels
{
	public class CreateTodo
	{
		public int? Id { get; set; }

		[MinLength(5)]
		public string Name { get; set; }

		[MinLength(5)]
		public string Description { get; set; }
		public int CategoryId { get; set; }
	}
}
