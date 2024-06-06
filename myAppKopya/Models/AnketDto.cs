using System.ComponentModel.DataAnnotations;

namespace myApp.Models
{
	public class AnketDto
	{
		public int id { get; set; }
		[MaxLength(100)]
		public string name { get; set; } = "";
		[MaxLength(100)]
		public string description { get; set; } = "";
		public int gc { get; set; } = 0;
		public int bc { get; set; } = 0;
		public int nc { get; set; } = 0;
		[MaxLength(20)]
		public string birinci { get; set; } = "";
		[MaxLength(20)]
		public string ikinci { get; set; } = "";
		[MaxLength(20)]
		public string ucuncu { get; set; } = "";

	}
}
