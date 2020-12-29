using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySharp.EfCore.StoredProcedure.Interface
{
    public interface IProcBuilder
    {
        IProcBuilder AddParam<T>(string name, T val);

        IProcBuilder AddParam<T>(string name, T val, out IOutParam<T> outParam);

        IProcBuilder AddParam<T>(string name, T val, out IOutParam<T> outParam, int size = 0, byte precision = 0, byte scale = 0);

        IProcBuilder AddParam<T>(string name, out IOutParam<T> outParam);

        IProcBuilder AddParam<T>(string name, out IOutParam<T> outParam, int size = 0, byte precision = 0, byte scale = 0);

        IProcBuilder AddParam(DbParameter parameter);

        IProcBuilder ReturnValue<T>(out IOutParam<T> retParam);

        IProcBuilder SetTimeout(int timeout);

        void Exec(Action<DbDataReader> action);

        Task ExecAsync(Func<DbDataReader, Task> action);

        Task ExecAsync(Func<DbDataReader, Task> action, CancellationToken cancellationToken);

        int ExecNonQuery();

        Task<int> ExecNonQueryAsync();

        Task<int> ExecNonQueryAsync(CancellationToken cancellationToken);

        void ExecScalar<T>(out T val);

        Task ExecScalarAsync<T>(Action<T> action);

        Task ExecScalarAsync<T>(Action<T> action, CancellationToken cancellationToken);

    }
}
