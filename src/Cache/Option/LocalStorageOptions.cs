using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Cache.Option
{
    public class LocalStorageOptions
    {
        /// <summary>
        /// Indicates if LocalStorage should be used (defaults to false).
        /// </summary>
        /// <remarks>
        /// Requires manually to Enable local storage.
        /// </remarks>
        public bool Enable { get; set; } = false;

        /// <summary>
        /// Indicates if LocalStorage should automatically load previously persisted state from disk, when it is initialized (defaults to true).
        /// </summary>
        /// <remarks>
        /// Requires manually to call Load() when disabled.
        /// </remarks>
        public bool AutoLoad { get; set; } = true;

        /// <summary>
        /// Indicates if LocalStorage should automatically persist the latest state to disk, on dispose (defaults to true).
        /// </summary>
        /// <remarks>
        /// Disabling this requires a manual call to Persist() in order to save changes to disk.
        /// </remarks>
        public bool AutoSave { get; set; } = true;

        /// <summary>
        /// Indicates if LocalStorage should encrypt its contents when persisting to disk.
        /// </summary>
        public bool EnableEncryption { get; set; } = false;

        /// <summary>
        /// [Optional] Add a custom salt to encryption, when EnableEncryption is enabled.
        /// </summary>
        public string EncryptionSalt { get; set; }

        /// <summary>
        /// Indicates Encryption Key for encrypting data value.
        /// </summary>
        public string EncryptionKey { get; set; }

        /// <summary>
        /// Folder for the persisted state on disk (defaults to "/wwwroot/storage/disk/").
        /// </summary>
        public string Folder { get; set; } = "wwwroot\\storage\\disk\\";

        /// <summary>
        /// Filename for the persisted state on disk (defaults to ".localstorage").
        /// </summary>
        public string Filename { get; set; } = ".localstorage";
    }
}
