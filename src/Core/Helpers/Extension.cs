using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.Core.Helpers
{
    public static class Extension
    {
            //dto.ForEach(n =>
            //{
                //var command = Mapping.onMap<CarDto, CreateCarCommand>(n);
            //});
        /// <summary>
        /// Partten match - Each(myList, i => Console.WriteLine(i));
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence) action(item);
        }

        ///// <summary>
        ///// Execute function. Be extra care when using this function as there is a risk for SQL injection
        ///// </summary>
        //public async Task<IEnumerable<T>> ExecuteFuntion<T>(string functionName, string parameter) where T : class
        //{
        //    return await _context.Query<T>().AsNoTracking().FromSql(string.Format("EXEC {0} {1}", functionName, parameter)).ToListAsync();
        //}
    }
}
