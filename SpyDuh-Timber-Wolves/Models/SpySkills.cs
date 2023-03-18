using System;
namespace SpyDuh_Timber_Wolves.Models
{
    public class SpySkills
    {
        public int id { get; set; }
        public string skillName { get; set; }
        public int skillLevel { get; set; }
        public int spyId { get; set; }
        public Spy Spy { get; set; }

    }
}

