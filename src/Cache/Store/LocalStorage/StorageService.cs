using EasySharp.Cache.Helpers;
using EasySharp.Cache.Option;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySharp.Cache.Store.LocalStorage
{
    public class StorageService : IStorage, IDisposable
    {
        private Cacheable _config { get; set; }

        //TOption _config { get; }


        /// <summary>
        /// Most current actual, in-memory state representation of the LocalDisk.
        /// </summary>
        private Dictionary<string, string> Storage { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// Gets the number of elements contained in the LocalDisk.
        /// </summary>
        public int CountAsync
        {
            get { return Storage.Count; }
            set { }
        }

        private object writeLock = new object();

        public StorageService(IOptions<Cacheable> configuration) : this(configuration.Value)
        {
            _config = configuration.Value;
        }

        public StorageService(Cacheable configuration)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));

            if (_config.LocalStorage.EnableEncryption)
            {
                if (string.IsNullOrEmpty(_config.LocalStorage.EncryptionKey)) throw new ArgumentNullException(nameof(_config.LocalStorage.EncryptionKey), "When EnableEncryption is enabled, an encryptionKey is required when initializing the LocalStorage.");
                _config.LocalStorage.EncryptionKey = "password";
            }

            if (_config.LocalStorage.AutoLoad)
                LoadAsync();
        }

        public void ClearAsync()
        {
            Storage.Clear();
        }


        public void DestroyAsync()
        {
            var filepath = FileHelpers.GetLocalStoreFilePath(_config.LocalStorage.Folder, _config.LocalStorage.Filename);
            if (File.Exists(filepath))
                File.Delete(FileHelpers.GetLocalStoreFilePath(_config.LocalStorage.Folder, _config.LocalStorage.Filename));
        }

        public void Dispose()
        {
            if (_config.LocalStorage.AutoSave)
                PersistAsync();
        }

        /// <summary>
        /// Determines whether this LocalDisk instance contains the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return Storage.ContainsKey(key: key);
        }

        /// <summary>
        /// Gets an object from the LocalDisk, without knowing its type.
        /// </summary>
        /// <param name="key">Unique key, as used when the object was stored.</param>
        public async Task<object> GetAsync(string key)
        {
            return GetAsync<object>(key);
        }

        /// <summary>
        /// Gets a strong typed object from the LocalDisk.
        /// </summary>
        /// <param name="key">Unique key, as used when the object was stored.</param>
        public async Task<T> GetAsync<T>(string key)
        {
            var succeeded = Storage.TryGetValue(key, out string raw);
            if (!succeeded) throw new ArgumentNullException($"Could not find key '{key}' in the LocalStorage.");

            if (_config.LocalStorage.EnableEncryption)
                raw = CryptographyHelpers.Decrypt(_config.LocalStorage.EncryptionKey, _config.LocalStorage.EncryptionSalt, raw);

            return JsonConvert.DeserializeObject<T>(raw);
        }

        /// <summary>
        /// Gets a collection containing all the keys in the LocalDisk.
        /// </summary>
        public async Task<IReadOnlyCollection<string>> Keys()
        {
            return Storage.Keys.OrderBy(x => x).ToList();
        }

        /// <summary>
        /// Loads the persisted state from disk into memory, overriding the current memory instance.
        /// </summary>
        /// <remarks>
        /// Simply doesn't do anything if the file is not found on disk.
        /// </remarks>
        public void LoadAsync()
        {
            if (!File.Exists(FileHelpers.GetLocalStoreFilePath(_config.LocalStorage.Folder, _config.LocalStorage.Filename))) return;

            var serializedContent = File.ReadAllText(FileHelpers.GetLocalStoreFilePath(_config.LocalStorage.Folder, _config.LocalStorage.Filename));

            if (string.IsNullOrEmpty(serializedContent)) return;

            Storage.Clear();
            Storage = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedContent);
        }

        public void PersistAsync()
        {
            var serialized = JsonConvert.SerializeObject(Storage, Formatting.Indented);

            var writemode = File.Exists(FileHelpers.GetLocalStoreFilePath(_config.LocalStorage.Folder, _config.LocalStorage.Filename))
                ? FileMode.Truncate
                : FileMode.Create;

            lock (writeLock)
            {
                using (var fileStream = new FileStream(FileHelpers.GetLocalStoreFilePath(_config.LocalStorage.Folder, _config.LocalStorage.Filename),
                    mode: writemode,
                    access: FileAccess.Write))
                {
                    using (var writer = new StreamWriter(fileStream))
                    {
                        writer.Write(serialized);
                    }
                }
            }
        }

        /// <summary>
        /// Syntax sugar that transforms the response to an IEnumerable<T>, whilst also passing along an optional WHERE-clause. 
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync<T>(string key, Func<T, bool> predicate = null)
        {
            var collection = await GetAsync<IEnumerable<T>>(key);
            return predicate == null ? collection : collection.Where(predicate);
        }

        /// <summary>
        /// Stores an object into the LocalDisk.
        /// </summary>
        /// <param name="key">Unique key, can be any string, used for retrieving it later.</param>
        /// <param name="instance"></param>
        public void StoreAsync<T>(string key, T instance)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            var value = JsonConvert.SerializeObject(instance);

            if (Storage.Keys.Contains(key))
                Storage.Remove(key);

            if (_config.LocalStorage.EnableEncryption)
                value = CryptographyHelpers.Encrypt(_config.LocalStorage.EncryptionKey, _config.LocalStorage.EncryptionSalt, value);

            Storage.Add(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetValueAsync<T>(string key, out T result)
        {
            var json = GetAsync(key);
            if (json == null)
            {
                result = default(T);
                return false;
            }

            result = JsonConvert.DeserializeObject<T>(json.ToString());
            //JsonConvert.DeserializeObject<T>(json);
            return true;
        }
    }
}
