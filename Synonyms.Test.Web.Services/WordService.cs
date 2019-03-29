using Microsoft.Extensions.Caching.Memory;
using Synonyms.Test.Web.Models.Service;
using Synonyms.Test.Web.Services.Interfaces;
using System.Collections.Generic;

namespace Synonyms.Test.Web.Services
{
    public class WordService : IWordService
    {
        #region === FIELDS ===

        private CacheHelper _cacheHelper;

        #endregion

        #region === CONSTRUCTORS ===

        public WordService(IMemoryCache memoryCache)
        {
            _cacheHelper = new CacheHelper(memoryCache);
        }

        #endregion

        #region === METHODS ===

        public void Add(string word)
        {
            _cacheHelper.AddWord(word);
        }

        public void Edit(string word, string description)
        {
            _cacheHelper.EditDescription(word, description);
        }

        public Word Get(string word)
        {
            return _cacheHelper.GetWord(word);
        }

        public IEnumerable<Word> GetAll()
        {
            return _cacheHelper.GetWords();
        }

        #endregion
    }
}
