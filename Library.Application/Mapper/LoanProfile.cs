using AutoMapper;
using Library.Application.ViewModel;
using Library.Core.Entities;

namespace Library.Application.Mapper
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<LoanInfo, LoanInfoViewModel>()
                .ForMember(dest => dest.NameBook, opt => opt.MapFrom(src => src.NameBook))
                .ForMember(dest => dest.NameClient, opt => opt.MapFrom(src => src.NameClient))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.LateFine, opt => opt.MapFrom(src => src.LateFine))
                .ForMember(dest => dest.LastStatusDate, opt => opt.MapFrom(src => src.LastStatusDate))
                .ForMember(dest => dest.IdStatusLoan, opt => opt.MapFrom(src => src.IdStatusLoan))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DateInitialLoan, opt => opt.MapFrom(src => src.DateInitialLoan));
        }
    }
}