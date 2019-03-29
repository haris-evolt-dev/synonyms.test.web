using System.Collections.Generic;

namespace Synonyms.Test.Web.Models.Service
{
    /// <summary>
    /// Object representing Word that will be used in services
    /// </summary>
    public class Word
    {
        #region === Fields ===

        public string Name { get; private set; }
        public string Definition { get; set; }
        public HashSet<Word> Synonyms { get; private set; }

        #endregion

        #region === Constructors ===

        public Word()
        {
            Synonyms = new HashSet<Word>();
        }

        public Word(string name)
        {
            Name = name;
            Synonyms = new HashSet<Word>();
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
