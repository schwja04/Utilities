using System;
using System.Data.Common;

namespace Utilities.Common.Sql.Abstractions
{
    public interface ISqlTransaction<TTransaction> : IDisposable
        where TTransaction : DbTransaction
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
