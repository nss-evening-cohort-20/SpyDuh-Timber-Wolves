using System;
namespace SpyDuh_Timber_Wolves.Models
{
	public class Spy
	{
		public int id { get; set; }

		public string name { get; set; }

		public string bio { get; set; }
		public List<SpySkills> spySkills { get; set; }

		public List<SpyServices> spyServices {get; set;}
	
	}
}

