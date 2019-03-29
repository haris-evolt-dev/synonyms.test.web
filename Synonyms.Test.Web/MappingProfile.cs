using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WordService = Synonyms.Test.Web.Models.Service.Word;
using WordView = Synonyms.Test.Web.Models.View.Word;

namespace Synonyms.Test.Web
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WordService, WordView>().ForMember(m => m.Synonyms, dest => dest.MapFrom<IEnumerable<string>>(src => src.Synonyms.ToList().Select(w => w.Name)));
            CreateMap<WordService, string>().ConstructUsing(w => w.ToString());
        }
    }
}