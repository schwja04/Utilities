using MySql.Data.MySqlClient;

namespace Utilities.MySql.Abstractions
{
    internal interface ISqlClientTransaction
    {
        MySqlTransaction SqlClientTransaction { get; }
    }
}

