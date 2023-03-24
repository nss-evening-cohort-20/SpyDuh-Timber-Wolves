using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public interface IEnemyRepository
    {
        void Add(Enemy enemy);
        void Delete(int id);
        List<Enemy> GetAll();
        List<Enemy> GetByEnemyId(int id);
    }
}