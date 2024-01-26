using Tacmin.Core.Repository;

namespace Tacmin.Data.Repositories
{
    public class MainRepository<T> : GenericRepository<T> where T : class
    {
        public MainRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}