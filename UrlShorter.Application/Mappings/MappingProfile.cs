using AutoMapper;
using UrlShorter.Application.Dtos;
using UrlShorter.Domain.Entities;


namespace UrlShorter.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Urls, GetUrlDto>().ReverseMap();
        }
    }
}
