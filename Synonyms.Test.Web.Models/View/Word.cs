using System;
using System.Collections.Generic;
using System.Text;

namespace Synonyms.Test.Web.Models.View
{
    public class Word
    {
        public string Name { get; set; }
        public string Definition { get; set; }
        public IEnumerable<string> Synonyms { get; set; }
    }
}
