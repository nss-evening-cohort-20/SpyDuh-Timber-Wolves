using System;
namespace SpyDuh_Timber_Wolves.Models
{
	public class SpyServices
	{
		public int id { get; set; }
        public string serviceName { get; set; }
        public int price { get; set; }
        public int spyId { get; set; }
		public Spy Spy { get; set; }
	}
}

