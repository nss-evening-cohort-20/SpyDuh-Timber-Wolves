using System;
namespace SpyDuh_Timber_Wolves.Models
{
	public class Enemy
	{
		public int Id { get; set; }

		public int spyId { get; set; }

		public Spy Spy { get; set; }

		public int enemyId { get; set; }

		public Spy enemy { get; set; }
	}
}

