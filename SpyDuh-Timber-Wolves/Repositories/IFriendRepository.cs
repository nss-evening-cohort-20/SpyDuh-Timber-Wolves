using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public interface IFriendRepository
    {
        void Add(Friend friend);
        void Delete(int id);
        List<Friend> GetAll();
        List<Friend> GetByFriendId(int id);
    }
}