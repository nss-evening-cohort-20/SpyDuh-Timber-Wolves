using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public interface ISpySkillsRepository
    {
        void Add(SpySkills skills);
        void Delete(int id);
        List<SpySkills> GetAll();
        SpySkills GetBySkillId(int id);
        List<SpySkills> GetBySpyId(int id);
        void Update(SpySkills skills);
    }
}