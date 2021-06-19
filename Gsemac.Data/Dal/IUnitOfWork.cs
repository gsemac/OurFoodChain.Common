using System;
using System.Threading.Tasks;

namespace Gsemac.Data.Dal {

    public interface IUnitOfWork :
        IDisposable {

        int SaveChanges();
        Task<int> SaveChangesAsync();

    }

}