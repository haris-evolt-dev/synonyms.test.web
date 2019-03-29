using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Synonyms.Test.Web.Services.Interfaces;
using System.Collections.Generic;

namespace Synonyms.Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynonymController : ControllerBase
    {
        private ISynonymService _synonymService;
        private IMapper _mapper;

        public SynonymController(
            ISynonymService synonymService,
            IMapper mapper
        )
        {
            _synonymService = synonymService;
            _mapper = mapper;
        }

        // PUT: api/Synonym/5
        [HttpPut("{word}")]
        public void Put(string word, [FromBody] string synonym)
        {
            _synonymService.Add(word, synonym);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{word}")]
        public void Delete(string word, [FromBody] string synonym)
        {
        }
    }
}
