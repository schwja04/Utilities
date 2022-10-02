using Npgsql;

namespace Utilities.PSql.Abstractions
{
    internal interface ISqlClientTransaction
    {
        NpgsqlTransaction SqlClientTransaction { get; }
    }
}
