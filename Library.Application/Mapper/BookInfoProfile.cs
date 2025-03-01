using AutoMapper;
using Library.Application.ViewModel;
using Library.Core.Entities;
using Library.Core.Enum;
using Library.Infrastructure.Models;

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
            CreateMap<BookInfoViewModel, BookInfo>()
                .ForMember(dest => dest.NameBook, opt => opt.MapFrom(src => src.NameBook))
                .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));
            CreateMap<Book, BookInfoViewModel>()
                .ForMember(dest => dest.NameBook, opt => opt.MapFrom(src => src.NameBook))
                .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.IdBookGenre))  // Mapear IdBookGenre para IdGenre
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));
        }
    }
}