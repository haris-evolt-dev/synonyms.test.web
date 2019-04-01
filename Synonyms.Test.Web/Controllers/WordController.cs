using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Synonyms.Test.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewWord = Synonyms.Test.Web.Models.View.Word;

namespace Synonyms.Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private IWordService _wordService;
        private IMapper _mapper;

        public WordController(
            IWordService wordService,
            IMapper mapper
        )
        {
            _wordService = wordService;
            _mapper = mapper;
        }

        // GET: api/Word
        /// <summary>
        /// Returns a list of all words in the system.
        /// Supports pagination by providing limit and offset values
        /// </summary>
        /// <returns></returns>
        [HttpGet("Autocomplete")]
        public IEnumerable<string> GetAutoComplete([FromQuery]string query, [FromQuery]int? limit = null, [FromQuery]int? offset = null)
        {
            if (string.IsNullOrEmpty(query))
                return new string[0];
            if (limit != null && limit < 0)
                throw new Exception("limit must be non negative integer!");
            if (offset != null && offset < 0)
                throw new Exception("offset must be non negative integer!");

            var words = _wordService.GetAll().Where(x => x.Name?.ToLower()?.Contains(query?.ToLower()) ?? false).OrderBy(x => x.Name).Skip(offset ?? 0);
            if (limit != null)
                words = words.Take(limit.Value);
            return _mapper.Map<IEnumerable<string>>(words);
        }

        // GET: api/Word
        [HttpGet]
        public IEnumerable<string> Get([FromQuery]int? limit = null, [FromQuery]int? offset = null)
        {
            if (limit != null && limit < 0)
                throw new Exception("limit must be non negative integer!");
            if (offset != null && offset < 0)
                throw new Exception("offset must be non negative integer!");

            var words = _wordService.GetAll().OrderBy(x => x.Name).Skip(offset ?? 0);
            if (limit != null)
                words = words.Take(limit.Value);
            return _mapper.Map<IEnumerable<string>>(words);
        }


        // GET: api/Word
        [HttpGet("/details", Name = "GetDetails")]
        public IEnumerable<ViewWord> GetDetails()
        {
            return _mapper.Map<IEnumerable<ViewWord>>(_wordService.GetAll());
        }

        // GET: api/Word/5
        [HttpGet("{word}", Name = "Get")]
        public ViewWord Get(string word)
        {
            return _mapper.Map<ViewWord>(_wordService.Get(word));
        }

        // POST: api/Word
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _wordService.Add(value);
        }

        // PUT: api/Word
        [HttpPut("{word}")]
        public void Put([FromQuery]string word, [FromBody] string description)
        {
            _wordService.Edit(word, description);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
