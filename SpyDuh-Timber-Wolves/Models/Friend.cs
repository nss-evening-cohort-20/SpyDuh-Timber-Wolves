using System;
namespace SpyDuh_Timber_Wolves.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public int spyId { get; set; }

        public Spy Spy { get; set; }

        public int friendId { get; set; }

        public Spy friend { get; set; }
    }
}

