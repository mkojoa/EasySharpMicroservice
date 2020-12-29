using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.Cache.Store.LocalStorage
{
    public interface IStorage
    {

        /// <summary>
        /// Gets the number of elements contained in the Local Storage.
        /// </summary>
        int CountAsync { get; set; }

        /// <summary>
        /// Clears the in-memory contents of the Local Storage, but leaves any persisted state on disk intact.
        /// </summary>
        /// <remarks>
        /// Use the Destroy method to delete the persisted file on disk.
        /// </remarks>
         void ClearAsync();

        /// <summary>
        /// Deletes the persisted file on disk, if it exists, but keeps the in-memory data intact.
        /// </summary>
        /// <remarks>
        /// Use the Clear method to clear only the in-memory contents.
        /// </remarks>
         void DestroyAsync();

        /// <summary>
        /// Determines whether this Local Storage instance contains the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
         Task<bool> ExistsAsync(string key);

        /// <summary>
        /// Gets an object from the Local Storage, without knowing its type.
        /// </summary>
        /// <param name="key">Unique key, as used when the object was stored.</param>
         Task<object> GetAsync(string key);

        /// <summary>
        /// Gets a strong typed object from the Local Storage.
        /// </summary>
        /// <param name="key">Unique key, as used when the object was stored.</param>
         Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Gets a collection containing all the keys in the Local Storage.
        /// </summary>
        Task<IReadOnlyCollection<string>> Keys();

        /// <summary>
        /// Loads the persisted state from disk into memory, overriding the current memory instance.
        /// </summary>
        /// <remarks>
        /// Simply doesn't do anything if the file is not found on disk.
        /// </remarks>
         void LoadAsync();


        /// <summary>
        /// Stores an object into the Local Storage.
        /// </summary>
        /// <param name="key">Unique key, can be any string, used for retrieving it later.</param>
        /// <param name="instance"></param>
         void StoreAsync<T>(string key, T instance);

        /// <summary>
        /// Syntax sugar that transforms the response to an IEnumerable<T>, whilst also passing along an optional WHERE-clause. 
        /// </summary>
         Task<IEnumerable<T>> QueryAsync<T>(string key, Func<T, bool> predicate = null);

        /// <summary>
        /// Persists the in-memory store to disk.
        /// </summary>
         void PersistAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
         bool TryGetValueAsync<T>(string key, out T result);
    }
}
