using Microsoft.Extensions.Caching.Memory;
using Synonyms.Test.Web.Models.Service;
using Synonyms.Test.Web.Services.Interfaces;
using System.Collections.Generic;

namespace Synonyms.Test.Web.Services
{
    public class SynonymService : ISynonymService
    {
        #region === Fields ===

        private CacheHelper _cacheHelper;

        #endregion

        #region === Constructors ===

        public SynonymService(
            IMemoryCache memoryCache
        )
        {
            _cacheHelper = new CacheHelper(memoryCache);
        }

        #endregion

        #region === Methods ===

        /// <summary>
        /// Adds provided synonym to the provided word
        /// </summary>
        /// <param name="word"></param>
        /// <param name="synonym"></param>
        public void Add(string word, string synonym)
        {
            _cacheHelper.AddSynonym(word, synonym);
        }

        /// <summary>
        /// Gets collection of synonyms related to the provided word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public IEnumerable<Word> Get(string word)
        {
            return _cacheHelper.GetWord(word)?.Synonyms;
        }

        #endregion

    }
}
