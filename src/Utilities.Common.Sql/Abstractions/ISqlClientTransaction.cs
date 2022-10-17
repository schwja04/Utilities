using System.Data.Common;

namespace Utilities.Common.Sql.Abstractions
{
    public interface ISqlClientTransaction<TTransaction>
        where TTransaction : DbTransaction
    {
        TTransaction SqlClientTransaction { get; }
    }
}

