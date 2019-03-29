using Microsoft.Extensions.Caching.Memory;
using Synonyms.Test.Web.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synonyms.Test.Web.Services
{
    /// <summary>
    /// Helper for accessing words from cache and keeping changes in sync
    /// </summary>
    public class CacheHelper
    {
        #region === FIELDS ===

        private static Dictionary<string, object> _locks = new Dictionary<string, object>();

        private Dictionary<string, Word> _words;
        private IMemoryCache _cache;

        #endregion

        #region === CONSTRUCTORS ===

        /// <summary>
        /// Creates new instance of <see cref="CacheEntryHelper"/> with default memory cache
        /// </summary>
        /// <param name="memoryCache"></param>
        public CacheHelper(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        #endregion

        #region === PROPERTIES ===

        /// <summary>
        /// Gets all the words from cache
        /// </summary>
        private Dictionary<string, Word> Words
        {
            get
            {
                if (!_cache.TryGetValue("words", out _words))
                {
                    _words = new Dictionary<string, Word>();
                    _cache.Set("words", _words, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.MaxValue));
                }
                return _words;
            }
        }

        #endregion

        #region === METHODS ===

        /// <summary>
        /// Saves current 
        /// </summary>
        private void Save()
        {
            _cache.Set("words", _words, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.MaxValue));
            _cache.TryGetValue("words", out _words);
        }

        public Word GetWord(string word)
        {
            return Words.GetValueOrDefault(word?.ToLower());
        }

        internal IEnumerable<Word> GetWords()
        {
            return Words.Values;
        }

        public void AddWord(string word)
        {
            var key1 = word.ToLower();
            var lock1 = _locks.GetOrCreate(key1);

            lock (lock1)
            {
                var w1 = Words.GetValueOrDefault(key1) ?? new Word(word);
                _words[key1] = w1;
                Save();
            }
        }

        public void EditDescription(string word, string description)
        {
            var key1 = word.ToLower();
            var lock1 = _locks.GetOrCreate(key1);

            lock (lock1)
            {
                var w1 = Words.GetValueOrDefault(key1) ?? new Word(word);
                w1.Definition = description;
                _words[key1] = w1;
                Save();
            }
        }

        public void AddSynonym(string word, string synonym)
        {
            var key1 = word.ToLower();
            var key2 = synonym.ToLower();

            if (key1 == key2)
                throw new ArgumentException("Word cannot have itself as synonym");

            var words = new List<string> { key1, key2 };
            words.Sort();

            var lock1 = _locks.GetOrCreate(words.First());
            var lock2 = _locks.GetOrCreate(words.Last());

            lock (lock1)
                lock (lock2)
                {
                    var w1 = Words.GetValueOrDefault(key1) ?? new Word(word);
                    var w2 = _words.GetValueOrDefault(key2) ?? new Word(synonym);

                    w1.Synonyms.Add(w2);
                    w2.Synonyms.Add(w1);

                    _words[key1] = w1;
                    _words[key2] = w2;

                    Save();
                }
        }

        #endregion
    }

    public static class Class1
    {

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
        {
            if (!dict.TryGetValue(key, out TValue val))
            {
                val = new TValue();
                dict.Add(key, val);
            }

            return val;
        }

    }
}
