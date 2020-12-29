using EasySharp.EfCore.StoredProcedure.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace EasySharp.EfCore.StoredProcedure
{
    public static class DbDataReaderExtension
    {
        private const string NoElementError = "Sequence contains no element";
        private const string MoreThanOneElementError = "Sequence contains more than one element";

        public static List<T> ToList<T>(this DbDataReader reader) where T : class, new()
        {
            var res = new List<T>();
            var mapper = new Mapper<T>(reader);
            mapper.Map(row => res.Add(row));
            return res;
        }

        public static Task<List<T>> ToListAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return ToListAsync<T>(reader, CancellationToken.None);
        }

        public static async Task<List<T>> ToListAsync<T>(this DbDataReader reader, CancellationToken cancellationToken) where T : class, new()
        {
            var res = new List<T>();
            var mapper = new Mapper<T>(reader);
            await mapper.MapAsync(row => res.Add(row), cancellationToken).ConfigureAwait(false);
            return res;
        }

        public static List<T> Column<T>(this DbDataReader reader) where T : IComparable
        {
            return Column<T>(reader, 0);
        }

        public static List<T> Column<T>(this DbDataReader reader, string columnName) where T : IComparable
        {
            int ordinal = columnName is null ? 0 : reader.GetOrdinal(columnName);
            return Column<T>(reader, ordinal);
        }

        public static List<T> Column<T>(this DbDataReader reader, int ordinal) where T : IComparable
        {
            var res = new List<T>();
            while (reader.Read())
            {
                T value = reader.IsDBNull(ordinal) ? default(T) : (T)reader.GetValue(ordinal);
                res.Add(value);
            }
            return res;
        }

        public static Task<List<T>> ColumnAsync<T>(this DbDataReader reader) where T : IComparable
        {
            return ColumnAsync<T>(reader, CancellationToken.None);
        }

        public static Task<List<T>> ColumnAsync<T>(this DbDataReader reader, CancellationToken cancellationToken) where T : IComparable
        {
            return ColumnAsync<T>(reader, 0, cancellationToken);
        }

        public static Task<List<T>> ColumnAsync<T>(this DbDataReader reader, string columnName) where T : IComparable
        {
            return ColumnAsync<T>(reader, columnName, CancellationToken.None);
        }

        public static Task<List<T>> ColumnAsync<T>(this DbDataReader reader, string columnName, CancellationToken cancellationToken) where T : IComparable
        {
            int ordinal = columnName is null ? 0 : reader.GetOrdinal(columnName);
            return ColumnAsync<T>(reader, ordinal, cancellationToken);
        }

        public static Task<List<T>> ColumnAsync<T>(this DbDataReader reader, int ordinal) where T : IComparable
        {
            return ColumnAsync<T>(reader, ordinal, CancellationToken.None);
        }

        public static async Task<List<T>> ColumnAsync<T>(this DbDataReader reader, int ordinal, CancellationToken cancellationToken) where T : IComparable
        {
            var res = new List<T>();
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                T value = await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? default(T) : (T)reader.GetValue(ordinal);
                res.Add(value);
            }
            return res;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, TValue>();
            var mapper = new Mapper<TValue>(reader);
            mapper.Map(val =>
            {
                TKey key = keyProjection(val);
                res[key] = val;
            });
            return res;
        }

        public static Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            return ToDictionaryAsync<TKey, TValue>(reader, keyProjection, CancellationToken.None);
        }
        
        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection, CancellationToken cancellationToken) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, TValue>();
            var mapper = new Mapper<TValue>(reader);
            await mapper.MapAsync(val =>
            {
                TKey key = keyProjection(val);
                res[key] = val;
            }, cancellationToken).ConfigureAwait(false);

            return res;
        }

        public static Dictionary<TKey, List<TValue>> ToLookup<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, List<TValue>>();
            var mapper = new Mapper<TValue>(reader);
            mapper.Map(val =>
            {
                TKey key = keyProjection(val);

                if (res.ContainsKey(key))
                {
                    res[key].Add(val);
                }
                else
                {
                    res[key] = new List<TValue> { val };
                }
            });
            return res;
        }

        public static Task<Dictionary<TKey, List<TValue>>> ToLookupAsync<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            return ToLookupAsync(reader, keyProjection, CancellationToken.None);
        }

        public static async Task<Dictionary<TKey, List<TValue>>> ToLookupAsync<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection, CancellationToken cancellationToken) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, List<TValue>>();
            var mapper = new Mapper<TValue>(reader);
            await mapper.MapAsync(val =>
            {
                TKey key = keyProjection(val);

                if (res.ContainsKey(key))
                {
                    res[key].Add(val);
                }
                else
                {
                    res[key] = new List<TValue>() { val };
                }
            }, cancellationToken).ConfigureAwait(false);

            return res;
        }

        public static HashSet<T> ToSet<T>(this DbDataReader reader) where T : IComparable
        {
            var res = new HashSet<T>();
            while (reader.Read())
            {
                T val = (T)reader.GetValue(0);
                res.Add(val);
            }
            return res;
        }

        public static Task<HashSet<T>> ToSetAsync<T>(this DbDataReader reader) where T : IComparable
        {
            return ToSetAsync<T>(reader, CancellationToken.None);
        }

        public static async Task<HashSet<T>> ToSetAsync<T>(this DbDataReader reader, CancellationToken cancellationToken) where T : IComparable
        {
            var res = new HashSet<T>();
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                T val = (T)reader.GetValue(0);
                res.Add(val);
            }
            return res;
        }

        public static T First<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, false, false);
        }
        public static T FirstOrDefault<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, true, false);
        }

        public static T Single<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, false, true);
        }

        public static T SingleOrDefault<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, true, true);
        }

        public static Task<T> FirstAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return FirstAsync<T>(reader, CancellationToken.None);
        }

        public static Task<T> FirstAsync<T>(this DbDataReader reader, CancellationToken cancellationToken) where T : class, new()
        {
            return FirstAsync<T>(reader, false, false, cancellationToken);
        }

        public static Task<T> FirstOrDefaultAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return FirstOrDefaultAsync<T>(reader, CancellationToken.None);
        }

        public static Task<T> FirstOrDefaultAsync<T>(this DbDataReader reader, CancellationToken cancellationToken) where T : class, new()
        {
            return FirstAsync<T>(reader, true, false, cancellationToken);
        }

        public static Task<T> SingleAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return SingleAsync<T>(reader, CancellationToken.None);
        }

        public static Task<T> SingleAsync<T>(this DbDataReader reader, CancellationToken cancellationToken) where T : class, new()
        {
            return FirstAsync<T>(reader, false, true, cancellationToken);
        }

        public static Task<T> SingleOrDefaultAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return SingleOrDefaultAsync<T>(reader, CancellationToken.None);
        }

        public static Task<T> SingleOrDefaultAsync<T>(this DbDataReader reader, CancellationToken cancellationToken) where T : class, new()
        {
            return FirstAsync<T>(reader, true, true, cancellationToken);
        }

        private static T First<T>(DbDataReader reader, bool orDefault, bool throwIfNotSingle) where T : class, new()
        {
            if (reader.Read())
            {
                var mapper = new Mapper<T>(reader);
                T row = mapper.MapNextRow();

                if (throwIfNotSingle && reader.Read())
                    throw new InvalidOperationException(MoreThanOneElementError);

                return row;
            }

            if (orDefault)
                return default;

            throw new InvalidOperationException(NoElementError);
        }

        private static async Task<T> FirstAsync<T>(DbDataReader reader, bool orDefault, bool throwIfNotSingle, CancellationToken cancellationToken) where T : class, new()
        {
            if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var mapper = new Mapper<T>(reader);
                T row = await mapper.MapNextRowAsync(cancellationToken).ConfigureAwait(false);

                if (throwIfNotSingle && await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                    throw new InvalidOperationException(MoreThanOneElementError);

                // Siphon out all the remaining records to allow for long running sprocs to be cancelled. If we leave
                // out of here without looping over the remaining records, a long running sproc will run to its end with
                // no chance to be cancelled. This is caused by the fact that DbDataReader.Dispose does not react to
                // cancellations and simply waits for the sproc to complete.
                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                    continue;

                return row;
            }

            if (orDefault)
                return default;

            throw new InvalidOperationException(NoElementError);
        }
    }
}
