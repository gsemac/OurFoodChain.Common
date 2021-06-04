using System;

namespace Gsemac.Data.Dal {

    public interface IUnitOfWork :
        IDisposable {

        int SaveChanges();

    }

}