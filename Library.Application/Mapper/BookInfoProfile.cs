using AutoMapper;
using Library.Application.ViewModel;
using Library.Core.Entities;

namespace Library.Application.Mapper
{
    public class BookInfoProfile : Profile
    {
        public BookInfoProfile()
        {
            CreateMap<BookInfo, BookInfoViewModel>()
                .ForMember(dest => dest.NameBook, opt => opt.MapFrom(src => src.NameBook))
                .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));
        }
    }
}