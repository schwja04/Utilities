namespace Utilities.TSql.Abstractions
{
    internal interface ISqlClientTransaction
    {
        Microsoft.Data.SqlClient.SqlTransaction SqlClientTransaction { get; }
    }
}

