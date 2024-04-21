using AutoMapper;
using SSS_StoreStockSystem.DAL.Models;
using SSS_StoreStockSystem.ViewModels;

namespace SSS_StoreStockSystem.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Item, ItemViewModel>().ReverseMap();
            CreateMap<Store, StoreViewModel>().ReverseMap();
            CreateMap<StoreItem, StockViewModel>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
                .ForMember(dest => dest.ItemCode, opt => opt.MapFrom(src => src.Item.Code))
                .ForMember(dest => dest.StoreCode, opt => opt.MapFrom(src => src.Store.Code))
                .ReverseMap();
        }
    }
}
