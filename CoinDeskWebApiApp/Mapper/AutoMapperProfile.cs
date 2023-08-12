using AutoMapper;
using CoinDeskWebApiApp.Models;
using CoinDeskWebApiApp.Models.Dtos;

namespace CoinDeskWebApiApp.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() { 
         CreateMap<BitCoin, BitCoinDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Bpi.Eur.Rate) );
        }
    }
}
