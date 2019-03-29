using Synonyms.Test.Web.Models.Service;
using System.Collections.Generic;

namespace Synonyms.Test.Web.Services.Interfaces
{
    public interface IWordService
    {
        void Add(string word);
        Word Get(string word);
        IEnumerable<Word> GetAll();
        void Edit(string word, string description);
    }
}
