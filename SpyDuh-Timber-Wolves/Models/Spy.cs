using System;
namespace SpyDuh_Timber_Wolves.Models
{
	public class Spy
	{
		public int id { get; set; }

		public string name { get; set; }

		public int skills { get; set; }

		public SpySkills spySkills { get; set; }

		public int services { get; set; }

		public SpyServices spyServices {get; set;}

		public string bio { get; set; }

		
	}
}

