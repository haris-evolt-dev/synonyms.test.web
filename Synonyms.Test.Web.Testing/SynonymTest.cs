using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synonyms.Test.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Synonyms.Test.Web.Testing
{
    [TestClass]
    public class SynonymTest
    {
        SynonymService _synonymService;
        WordService _wordService;

        [TestInitialize]
        public void Init()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            _synonymService = new SynonymService(memoryCache);
            _wordService = new WordService(memoryCache);
        }

        [TestMethod]
        public void AddSynonym()
        {
            string word = "walking",
                synonym = "strolling";

            _wordService.Add(word);
            _synonymService.Add(word, synonym);

            var words = _wordService.GetAll();

            Assert.IsNotNull(words);
            Assert.AreEqual(2, words.Count());

            var addedWord = _wordService.Get(word);
            var addedSynonym = _wordService.Get(synonym);

            Assert.IsTrue(addedWord.Synonyms.Count == 1);
            Assert.IsTrue(addedWord.Synonyms.FirstOrDefault() == addedSynonym);
            Assert.IsTrue(addedSynonym.Synonyms.FirstOrDefault() == addedWord);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddSynonymToItself()
        {
            string word = "walking",
                synonym = "Walking";

            _wordService.Add(word);
            _synonymService.Add(word, synonym);
        }

        [TestMethod]
        public void AddMultipleSynonyms()
        {
            string word = "walking",
                synonym1 = "strolling",
                synonym2 = "roaming";

            _wordService.Add(word);
            _synonymService.Add(word, synonym1);
            _synonymService.Add(word, synonym2);

            var words = _wordService.GetAll();

            Assert.IsNotNull(words);
            Assert.AreEqual(3, words.Count());

            var addedWord = _wordService.Get(word);
            var addedSynonym1 = _wordService.Get(synonym1);
            var addedSynonym2 = _wordService.Get(synonym2);

            Assert.IsTrue(addedWord.Synonyms.Count == 2);
            Assert.IsTrue(addedSynonym1.Synonyms.Count == 1);
            Assert.IsTrue(addedSynonym2.Synonyms.Count == 1);
        }

        [TestMethod]
        public void AddMultipleSynonymsAsync()
        {
            string word = "walking",
                synonym1 = "strolling",
                synonym2 = "roaming";

            _wordService.Add(word);
            Parallel.Invoke(
                () => _synonymService.Add(word, synonym1),
                () => _synonymService.Add(word, synonym2)
            );

            var words = _wordService.GetAll();

            Assert.IsNotNull(words);
            Assert.AreEqual(3, words.Count());

            var addedWord = _wordService.Get(word);
            var addedSynonym1 = _wordService.Get(synonym1);
            var addedSynonym2 = _wordService.Get(synonym2);

            Assert.IsTrue(addedWord.Synonyms.Count == 2);
            Assert.IsTrue(addedSynonym1.Synonyms.Count == 1);
            Assert.IsTrue(addedSynonym2.Synonyms.Count == 1);
        }

        [TestMethod]
        public void AddMultipleSameSynonymsAsync()
        {
            string word = "walking",
                synonym = "strolling";

            _wordService.Add(word);
            Parallel.Invoke(
                () => _synonymService.Add(word, synonym),
                () => _synonymService.Add(synonym, word)
            );

            var words = _wordService.GetAll();

            Assert.IsNotNull(words);
            Assert.AreEqual(2, words.Count());

            var addedWord = _wordService.Get(word);
            var addedSynonym = _wordService.Get(synonym);

            Assert.IsTrue(addedWord.Synonyms.Count == 1);
            Assert.IsTrue(addedSynonym.Synonyms.Count == 1);
        }
    }
}
