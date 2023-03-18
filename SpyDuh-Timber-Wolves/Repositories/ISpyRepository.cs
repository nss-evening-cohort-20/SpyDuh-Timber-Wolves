using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public interface ISpyRepository
    {
        void Add(Spy spy);
        void Delete(int id);
        Spy GetById(int id);
        List<Spy> GetAll();
        void Update(Spy spy);
    }
}