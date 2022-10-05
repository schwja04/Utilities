namespace Utilities.Sqlite.Abstractions
{
    internal interface ISqlClientTransaction
    {
        Microsoft.Data.Sqlite.SqliteTransaction SqlClientTransaction { get; }
    }
}

