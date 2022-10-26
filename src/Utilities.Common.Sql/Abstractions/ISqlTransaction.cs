using System;
using System.Data.Common;

namespace Utilities.Common.Sql.Abstractions
{
    public interface ISqlTransaction : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
