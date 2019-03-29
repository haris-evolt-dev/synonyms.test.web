using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Synonyms.Test.Web.Models.Service;
using Synonyms.Test.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synonyms.Test.Web.Testing
{
    [TestClass]
    public class WordTest
    {
        WordService _wordService;

        [TestInitialize]
        public void Init()
        {
            //var words = new Dictionary<string, Word>();
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            _wordService = new WordService(memoryCache);
        }

        [TestMethod]
        public void AddSingleWord()
        {
            var word = "singleWord";
            _wordService.Add(word);
            var rword = _wordService.GetAll();
            Assert.IsNotNull(rword);
            Assert.AreEqual(1, rword.Count());

            var addedWord = _wordService.Get(word);
            Assert.AreEqual(word, addedWord.Name);
            Assert.AreEqual(0, addedWord.Synonyms.Count);
        }

        [TestMethod]
        public void AddDifferentWords()
        {
            string word1 = "firstWord",
                   word2 = "secondWord";

            _wordService.Add(word1);
            _wordService.Add(word2);

            var rword = _wordService.GetAll();
            Assert.IsNotNull(rword);
            Assert.AreEqual(2, rword.Count());
        }

        [TestMethod]
        public void AddSameWords()
        {
            string word1 = "word",
                   word2 = "Word";

            _wordService.Add(word1);
            _wordService.Add(word2);

            var rword = _wordService.GetAll();
            Assert.IsNotNull(rword);
            Assert.AreEqual(1, rword.Count());
        }
    }
}
