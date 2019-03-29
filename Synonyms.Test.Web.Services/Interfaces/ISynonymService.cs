using System;
using System.Collections.Generic;
using System.Text;

namespace Synonyms.Test.Web.Services.Interfaces
{
    public interface ISynonymService
    {
        void Add(string word1, string word2);
    }
}