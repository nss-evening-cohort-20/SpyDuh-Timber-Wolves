using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public interface ISpyServicesRepository
    {
        void Add(SpyServices services);
        void Delete(int id);
        List<SpyServices> GetAll();
        SpyServices GetByServiceId(int id);
        List<SpyServices> GetBySpyId(int id);
        void Update(SpyServices services);
    }
}